using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scripts
{
    public interface ISusceptibleToBullets
    {
        public void Hit();
        public DamageModel DamageModel { get; set; }
    }
}
