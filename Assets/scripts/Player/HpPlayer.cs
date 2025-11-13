using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpPlayer : MonoBehaviour
{
    public float HealthPoint;
    public float maxHp;
    public GameObject player;
    public bool Activision;
   
    public void Death()
    {
        HealthPoint = 0;
    }
    public void TakeDamage(float damage)
    {
        HealthPoint -= damage;
    }
    void DeadFromEnemy()
    {
        if(HealthPoint <= 0)
        {
            Activision = false;
            player.SetActive(Activision);
           
        }
    }
    private void Update()
    {
        DeadFromEnemy();
    }
    private void Start()
    {
        HealthPoint = maxHp / 3;
    }
}
