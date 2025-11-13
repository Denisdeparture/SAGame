using Assets.scripts;
using Assets.scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathdown : MonoBehaviour, IRespawningOnCheckPoint
{
    public float timerBeforeDeath = 1f;
    public float timerBeforeActivate = 1f;
    public float forseFriction = 0.1f;
    public int damage = 20;
    public GameObject DarkPanel;
    private float TimerBeforeRespawn = 0f;
    private float TimerBefore = 0f;
    private bool isDown = false;
    private bool isRespawn = false;
    private Rigidbody2D RbPlayer;
    private HpPlayer HpPlayer;
    private PlayerAnimatorManager ObjectManager;
    private GameObject Player;
    public PlayerAnimatorManager PlayerAnimatorManager => ObjectManager;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player" | collider.CompareTag("Player"))
        {
            isDown = true;
            HpPlayer = collider.gameObject.GetComponent<HpPlayer>();
            RbPlayer = collider.gameObject.GetComponent<Rigidbody2D>();
            ObjectManager = collider.gameObject.GetComponent<MovePlayerBeta>().playerAnimatorManager;
            Player = collider.gameObject;
        }
    }
    private void FixedUpdate()
    {
        if(isDown == true)
        {
            
            if (TimerBefore < timerBeforeDeath)
            {
                RbPlayer.AddForce(Vector3.up * forseFriction);
                TimerBefore += Time.fixedDeltaTime;
                DarkPanel.SetActive(true);
                return;
            }
            else
            {
                isRespawn = true;
            }
        }
        if (isRespawn)
        {
            Respawn();
            isDown = false;
            TimerBefore = 0f;
        }
    }
    public void Respawn()
    {
       
        if (TimerBeforeRespawn < timerBeforeActivate)
        {
            TimerBeforeRespawn += Time.fixedDeltaTime;
            RbPlayer.sleepMode = RigidbodySleepMode2D.StartAsleep;
            return;
        }
        PlayerAnimatorManager.Respawn();
        DarkPanel.SetActive(false);
        Player.transform.position = CheckPointsRegisterService.TakeLast();
        RbPlayer.sleepMode = RigidbodySleepMode2D.NeverSleep;
        HpPlayer.TakeDamage(damage);
        TimerBeforeRespawn = 0f;
        isRespawn = false;
       

    }
}
