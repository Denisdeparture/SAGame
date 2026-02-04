using NaughtyAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.scripts
{
    // Kind of Punch only for player
    public class PunchOnKey : Punch
    {
        public KeyCode KeyForInvoke = KeyCode.F;

        [ShowIf(nameof(KeyForInvoke), KeyCode.None)]
        public MouseButton mouseButton;
        public override void Configurate()
        {
            PunchModel.ConditionalInvoke = Condtional;
            PunchModel.ConditionForGet += Condtional;
            base.Configurate();
        }
        public bool Condtional()
        {
            return KeyForInvoke == KeyCode.None ? Input.GetMouseButton((int)mouseButton) : Input.GetKey(KeyForInvoke);
        }
    }
}
