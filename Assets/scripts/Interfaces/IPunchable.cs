using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scripts.Interfaces
{
    public interface IPunchable
    {
        public void HitFromPunch(DamageModel OuterDamageModel);
        public DamageModel DamageModel { get; set; }
    }
}
