using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatscenceStart : MonoBehaviour
{
    public GameObject triggerScence;
    public GameObject[] OnObject;
    private int countCutscence = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (countCutscence == 0)
        {
            if (collision.gameObject.name == "player")
            {

                triggerScence.SetActive(true);
                countCutscence += 1;
            }
            
        }
        else
        {
            foreach (GameObject item in OnObject)
            {
                item.SetActive(true);
            }
            gameObject.SetActive(false);
        }
    }
    
}
