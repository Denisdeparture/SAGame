using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.scripts
{
    public class CombatManager : MonoBehaviour
    {
        public float timerBetweenAttack = 3f;

        private float TimerBefore = 0f;

        public List<Punch> Punches = new List<Punch>();

        private void FixedUpdate()
        {
            DoPunch();
        }
        private void DoPunch()
        {
            if (TimerBefore < timerBetweenAttack)
            {
                TimerBefore += Time.fixedDeltaTime;
                return;
            }
            foreach(var p in Punches)
            {
                p.PunchModel.NowConditionValueUpdate();
                if (p.PunchModel.ConditionalInvoke())
                {
                    p.Invoke();
                }
            }
            TimerBefore = 0f;
        }

        public static void CreatePunch(Punch punch, Vector3 startposition, Quaternion quaternion)
        {
            Instantiate(punch, startposition, quaternion);
        }

    }
}
