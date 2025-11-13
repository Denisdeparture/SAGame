using Assets.scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.scripts
{
    [RequireComponent(typeof(AudioSource))]
    public class Punch : MonoBehaviour
    {
        public PunchModel PunchModel;

        private AudioSource AudioSource;

        private void Start()
        {
            AudioSource = GetComponent<AudioSource>();
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            var gobject = collision.gameObject.GetComponent<IPunchable>();

            if (gobject is null) return;

            gobject.DamageModel = new DamageModel();

            gobject.DamageModel.From = PunchModel.Initiator;

            gobject.DamageModel.Damage = PunchModel.damage;

            PunchModel.ObjectAnimator.Play(PunchModel.NameOfAnim);

            if (PunchModel.AudioClip is null) return;
            
            AudioSource.PlayOneShot(PunchModel.AudioClip);

            gobject.HitFromPunch(gobject.DamageModel);
        }
    }
    [Serializable]
    public class PunchModel
    {
        public Animator ObjectAnimator;

        public string NameOfAnim;

        public float damage;

        public AudioClip AudioClip;

        public GameObject VFXEffect;

        public GameObject Initiator;
    }
}
