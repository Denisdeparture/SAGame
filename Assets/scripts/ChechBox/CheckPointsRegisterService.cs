using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.scripts
{
    [System.Serializable]
    public class CheckPointsRegisterService : MonoBehaviour
    {
        private static List<Vector2> CheckPoints = new List<Vector2>();

        public static void Register(Vector2 vector)
        {
            var point = CheckPoints.Where(pos => pos.x == vector.x & vector.y == pos.y).ToList();
            if (point.Count != 0 && (point[0].x == 0 & point[0].y == 0))
            {
                Debug.Log("Some point already exists");
                return;
            }
            CheckPoints.Add(vector);
        }
        public static Vector2 TakeLast() => CheckPoints.LastOrDefault();
        public static void DeleteAll() => CheckPoints.Clear();
        public static IList<Vector2> GetAll() => CheckPoints;
    }
}
