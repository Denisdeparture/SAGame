using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scripts.Interfaces
{
    public interface IRespawningOnCheckPoint
    {
        public PlayerAnimatorManager PlayerAnimatorManager { get; }
        public void Respawn();
    }
}
