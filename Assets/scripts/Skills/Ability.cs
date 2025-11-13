using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.scripts.Skills
{
    public abstract class Ability : MonoBehaviour
    {
        private SkillModel Skill {  get; set; }
        public AnimationClip Animation {  get; private set; }
        public AudioClip Audioclip { get; private set; }

        public bool NeedEffects;

        [NonSerialized]
        public bool Activate = false;

        private bool WasConfig {  get; set; } = false;
        public abstract void OtherFunction();
        public abstract void OtherConfiguration();
        public abstract bool Condition();

        public SkillModel GetSkill()
        {
            return Skill;
        }
        public void ConfigureAbility(AnimationClip animation, SkillModel skillModel, AudioClip clip)
        {
            Skill = skillModel;
            if (NeedEffects)
            {
                Animation = animation;
                Audioclip = clip;
            }
        }
        public void Invoke()
        {
            if (!Activate) return;
            if(!WasConfig)
            {
                OtherConfiguration();
                WasConfig = true;
            }
            if (Condition())
            {
                if (NeedEffects)
                {
                    if (Skill.animator is not null & Animation is not null)
                    {
                        Skill.animator.Play(Animation.name);
                    }
                    if (Skill.audioSource is not null & Audioclip is not null)
                    {
                        Skill.audioSource.PlayOneShot(Audioclip);
                    }
                }
                OtherFunction();
            }
        }
    }
}
