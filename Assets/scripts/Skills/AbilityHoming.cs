using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets.scripts.Skills
{
    public class AbilityHoming : Ability
    {

        public float DistantionForSimpleHoming = 10f;

        public float SpeedHoming = 2f;


        public float YOffset = 1f;

        public float timerBeforeTangentEffect = 3f;

        public float timerBetweenSimpleHoming = 3f;

        private float TimerBefore = 0f;

        private SkillMovingModel Model;

        public AnimationClip AnimationModel;

        public AudioClip BaseAudioClip;

        private Target TargetArea;

        public GameObject Target;

        public int ManaDamage = 2;

        public ShotBullet ManaManager;

        public ShockWaveEffect shockWave;

        private bool WasHoming = false;
        private bool Cond 
        {  
            get 
            {
                bool isMouse = Model.isMouseActivated == true;

                return  isMouse && Input.GetMouseButton((int)Model.buttonMouse) | Input.GetKey(Model.keyForActivated);
            } 
        }

        private void Start()
        {

            Model = GetComponent<SkillMovingModel>();

            TargetArea = Target.GetComponent<Target>();

            ConfigureAbility(AnimationModel, Model, BaseAudioClip);
        }
        private void FixedUpdate()
        {
            if (!Activate) return;
            if (Cond & !WasHoming)
            {
                SimpleHoming();
                WasHoming = true;
                
            }
            else
            {
                WasHoming = false;
            }
           
        }
        public override bool Condition()
        {
            return Cond;
        }

        public override void OtherConfiguration()
        {
            return;
        }

        public override void OtherFunction()
        {
            Homing();
        }
        private void Homing()
        {

            var infoAbove = TargetArea.TargetObjectInfo;

            if (infoAbove is null)
            {
                return;
            }
            infoAbove.HomingModel.NowConditionValueUpdate();
            if (infoAbove.HomingModel.Condition())
            {
                if (WasHoming) return;
                EnableEffects();
            }
            if (TargetArea.Targets.Count == 0) return;

            var player = Model.playerMovement.gameObject;

            if (TimerBefore < timerBeforeTangentEffect)
            {
                TimerBefore += Time.fixedDeltaTime;
                player.transform.position = Vector2.Lerp(player.transform.position, infoAbove.NowPosition, SpeedHoming * Time.fixedDeltaTime);
                return;
            }
            else
            {
                TimerBefore = 0f;
                infoAbove.TangentEffect();
            }
       
        }
        private void SimpleHoming()
        {
            if (WasHoming) return;

            if(ManaManager.NowBullets <= 0)
            {
                return;
            }

            EnableEffects();

            ManaManager.CounterBullets += ManaDamage;

            var movement = Model.playerMovement;

            var player = movement.gameObject;

            var newPos = new Vector2(movement.Faceleft ? player.transform.position.x + DistantionForSimpleHoming : player.transform.position.x - DistantionForSimpleHoming, player.transform.position.y + YOffset);

            player.transform.position = Vector2.Lerp(player.transform.position, newPos, SpeedHoming * Time.fixedDeltaTime);
           
        }
        private void EnableEffects()
        {
            if (WasHoming) return;

            if (shockWave is not null) { shockWave.CallEffect(); }

            Model.playerMovement.NeeedTrail = true;

            var animator = Model.playerMovement.playerAnimatorManager;

            Model.playerMovement.playerAnimatorManager.SetOneAnimation(nameof(animator.Homing), true);

            Model.playerMovement.playerAnimatorManager.Homing(true);

            StartCoroutine(DisableEffects());

        }
        IEnumerator DisableEffects()
        {
            yield return new WaitForSeconds(timerBetweenSimpleHoming);

            var animator = Model.playerMovement.playerAnimatorManager;

            Model.playerMovement.playerAnimatorManager.SetOneAnimation(nameof(animator.Homing), false);

            Model.playerMovement.playerAnimatorManager.Homing(false);

            Model.playerMovement.NeeedTrail = false;
        }
    }
}
