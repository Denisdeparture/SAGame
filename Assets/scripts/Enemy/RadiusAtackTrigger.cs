using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiusAtackTrigger : MonoBehaviour
{
    public bool Trigger;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.name == "player")
        {
            Trigger = true;
        }
        else
        {
            Trigger = false;
        }
    }
    
}
