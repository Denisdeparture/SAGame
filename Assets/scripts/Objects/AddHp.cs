using Assets;
using Assets.scripts;
using Assets.scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHp : MonoBehaviour, ISusceptibleToBullets
{
    public HpPlayer hp;
    public int hpAdd;
    public Animator anim;
    public float timeBeforeDes = 1f;
    public GameObject obj;
    public AudioClip clip;

    private bool healthNow = false;
    private float TimerBefore = 0f;
    private AudioSource audioSource;
    private bool audioIsPlay = true;

    public GameObject target;

    public DamageModel DamageModel { get; set; }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void AddHealthPoint()
    {
        healthNow = true;
    }
    public void Health()
    {
        if (healthNow)
        {
            TimerBefore += Time.fixedDeltaTime;

            anim.SetTrigger("Burst");

            if (audioIsPlay)
            {
                audioSource.PlayOneShot(clip);
                audioIsPlay = false;
            }

            if (TimerBefore < timeBeforeDes) return;

            hp.HealthPoint += hpAdd;

            Destroy(obj);
        }
    }
    public void Hit()
    {
        AddHealthPoint();
    }
    private void FixedUpdate()
    {
        Health();
    }

    public void TangentEffect()
    {
        AddHealthPoint();
    }

}
