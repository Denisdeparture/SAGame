using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace Assets.scripts
{
    public interface IEnemy : ISusceptibleToBullets
    {
        public void DoDamage();
       
        public HpPlayer HpManager { get; set; }
      
        public MovePlayerBeta Target { get; set; }

        public Animator Animator { get; set; }

        public AudioSource AudioSource { get; set; }

        public KindOfAttackEnemy AttackEnemy { get; set; }

        public SpriteRenderer SpriteRenderer { get; set; }

    }
    public enum KindOfAttackEnemy
    {
        Near,
        Distant
    }
}
