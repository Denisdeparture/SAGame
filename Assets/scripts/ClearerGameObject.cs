using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.scripts
{
    public class ClearerGameObject : MonoBehaviour
    {
        private bool NowClear = false;
        private float timer = 0;
        private float innerTimer = 0;
       
        private void FixedUpdate()
        {
            ClearInner();
        }
        public void Clear(float timebeforeClean)
        {
            NowClear = true;

            timer = timebeforeClean;

        }
        private void ClearInner()
        {
            if (!NowClear) return;

            if (innerTimer >= timer)
            {
                Destroy(gameObject);
            }
            else
            {
                innerTimer += Time.fixedDeltaTime;
            }
        }
    }
}
