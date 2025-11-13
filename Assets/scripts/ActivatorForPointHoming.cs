using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActivatorForPointHoming : MonoBehaviour
{

    public GameObject Point;
    public float timer = 3f;
    private float TimerBefore = 0f;
    public UnityEvent Event;
    void Update()
    {
        if (Point.active == true) return;

        if (TimerBefore < timer)
        {
            TimerBefore += Time.fixedDeltaTime;
            return;
        }
        else
        {
            Point.SetActive(true);
            Event.Invoke();
        }


      
    }
}
