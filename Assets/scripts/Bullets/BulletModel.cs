using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scripts
{
    public class BulletModel
    {
        public int Id {  get; set; }

        public BulletModelDelegate Condition { get; set; }

        public event BulletModelDelegate ConditionForGet;

        public Bullet Object { get; set; }

        public void NowConditionValueUpdate()
        {
            ConditionForGet();
        }
    }
    public delegate bool BulletModelDelegate();
}
