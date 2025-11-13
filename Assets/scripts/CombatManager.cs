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
        public static void CreatePunch(Punch punch, Vector3 startposition, Quaternion quaternion)
        {
            Instantiate(punch, startposition, quaternion);
        }
    }
}
