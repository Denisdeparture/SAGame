using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remover : MonoBehaviour
{
    public string Name = "WeaponEnemy";
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == Name)
        {
            Destroy(collision.gameObject);
        }
    }
}
