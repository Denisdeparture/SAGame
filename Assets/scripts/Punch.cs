using Assets.scripts.Interfaces;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.scripts
{
    [RequireComponent(typeof(AudioSource))]
    public class Punch : MonoBehaviour
    {
        public PunchModel PunchModel;

        private AudioSource AudioSource;

        private bool StartVFXNow = false;

        private void Start()
        {
            Configurate();
        }
        private void FixedUpdate()
        {
           FixedUpdateWork();   
        }
        public virtual void Configurate()
        {
            AudioSource = GetComponent<AudioSource>();
        }
        public virtual void FixedUpdateWork()
        {
            if(StartVFXNow)
            {
                VFXWwork(true);
            }
        }
        public void Invoke()
        {

            RaycastHit2D hitinfo = Physics2D.Raycast(transform.position, Vector2.up, PunchModel.distance, PunchModel.bullmasks);

            var collision = hitinfo.collider;

            if (collision is null)
            {
                Debug.Log("Here");
                return;
            }

            Debug.Log("2");
            var gobject = collision.gameObject.GetComponent<IPunchable>();

            if (gobject is null) return;

            if (!PunchModel.ConditionalInvoke()) return;

            gobject.DamageModel = new DamageModel();

            gobject.DamageModel.From = PunchModel.Initiator;

            gobject.DamageModel.Damage = PunchModel.damage;

            PunchModel.ObjectAnimator.Play(PunchModel.NameOfAnim);

            StartVFXNow = true;

            if (PunchModel.AudioClip is null) return;
            
            AudioSource.PlayOneShot(PunchModel.AudioClip);

            gobject.HitFromPunch(gobject.DamageModel);
        }

        private void OnDrawGizmosSelected()
        {
            Debug.Log("test");
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.up * PunchModel.distance);
        }
        private void VFXWwork(bool isFirst)
        {

            if (isFirst)
            {
                VFXWwork(false);

                PunchModel.VFXEffect.transform.position = PunchModel.Initiator.transform.position;

                PunchModel.VFXEffect.SetActive(true);
            }

            if (PunchModel.TimerBefore < PunchModel.timerVfx)
            {

                PunchModel.TimerBefore += Time.fixedDeltaTime;

                PunchModel.VFXAnimator.Play(PunchModel.AnimationForVfxName);

                return;
            }

            PunchModel.VFXEffect.SetActive(false);
            PunchModel.TimerBefore = 0;
            StartVFXNow = false;
        }
    }
    [Serializable]
    public class PunchModel
    {

        [Header("Stat")]
        public float damage;
        public LayerMask bullmasks;
        public float distance;
        [Space]
        [Header("Initiator")]
        public string NameOfAnim;
        public Animator ObjectAnimator;
        public AudioClip AudioClip;
        public GameObject Initiator;
        [Space]
        [Header("VFX")]
        public GameObject VFXEffect;
        public Animator VFXAnimator => VFXEffect.GetComponent<Animator>();
        public string AnimationForVfxName;
        public float timerVfx;
        // Maybe you need offset x, y
        [NonSerialized]
        public float TimerBefore;
        public Func<bool> ConditionalInvoke { get; set; }

        public event Func<bool> ConditionForGet;

        public void NowConditionValueUpdate()
        {
            ConditionForGet();
        }
    }
}
