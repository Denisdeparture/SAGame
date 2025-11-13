using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCutscence : MonoBehaviour
{
    public HpPlayer MephHp;
    public GameObject cutscence;
    private void Update()
    {
       if(MephHp.HealthPoint <= 0)
        {
            cutscence.SetActive(true);
        } 
    }
}
