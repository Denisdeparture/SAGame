using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.scripts
{
    [System.Serializable]
    public class DataBullets : MonoBehaviour
    {
        private static List<BulletModel>  Bullets = new List<BulletModel>();
        
        public static void Add(BulletModel bullet)
        {
            Debug.Log("Add bullet: " + bullet.Object.name + " with id: " + bullet.Id);
            Bullets.Add(bullet);
        }
        public static void Delete(int id)
        {
            Bullets.RemoveAll(b => b.Id == id);
        }
        public static BulletModel Get(int id)
        {
            return Bullets.Where(x => x.Id == id).SingleOrDefault();
        }
        public static List<BulletModel> GetAll() => Bullets;
        public static void UpdateConditionFor(int id, BulletModelDelegate condition)
        {
            var bullet = Get(id);

            bullet.ConditionForGet += condition;
        }
        
    }
}
