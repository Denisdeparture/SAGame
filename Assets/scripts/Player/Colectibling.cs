using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colectibling : MonoBehaviour
{
    public AudioClip clip;

    public AudioSource source;

    public float timeBeforeDes = 3f;
    private float TimerBefore = 0f;
    private bool trigger = false;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
           trigger = true;

           source.PlayOneShot(clip);

           animator.SetTrigger("IsCollect");

        }
    }
    void Collect()
    {
        Destroy(gameObject);

        DataBase.playerInfo.Coins += 1;
    }
    private void FixedUpdate()
    {
        if (trigger)
        {
            TimerBefore += Time.fixedDeltaTime;
        }
        if(timeBeforeDes <= TimerBefore)
        {
            Collect();
        }
    }

}
