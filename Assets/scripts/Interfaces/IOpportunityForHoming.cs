using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.scripts.Interfaces
{
    public interface IOpportunityForHoming
    {
        public GameObject Target { get; set; }
        public Vector2 NowPosition { get; set; }

        public HomingModel HomingModel { get; set; }

        public int Id { get; set; }

        public void TangentEffect();

    }
    public class HomingModel
    {
        public HomingModelDelegate Condition { get; set; }

        public event HomingModelDelegate ConditionForGet;

        public void NowConditionValueUpdate()
        {
            ConditionForGet();
        }
    }
    public delegate bool HomingModelDelegate();
}
