using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCutscence : MonoBehaviour
{
    public HpPlayer hp;
    public GameObject Edge;
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        OnCutscence();
    }
    private void OnCutscence()
    {

        if(hp.HealthPoint <= 0)
        {
            Edge.SetActive(true);
        }
    }
}
