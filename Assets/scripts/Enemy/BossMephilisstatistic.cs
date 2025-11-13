using Assets;
using Assets.scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
[RequireComponent(typeof(Animator), typeof(AudioSource))]
public class BossMephilisstatistic : MonoBehaviour,  IEnemy
{
    public float damage;
    public float destenyPunch;
    public float speed = 5f;
    public float jumpflyForce = 3f;
    public Transform triggerFromAtack;
    public MovePlayerBeta PLAYER;
    public Transform AtackPoint;
    private Rigidbody2D physicsBoss;
    public RadiusAtackTrigger tr;
    private Animator anim;
    public HpPlayer hpEnemy;
    public int countFromSeconds;
    public HpPlayer hp;
    public GameObject scencePast;

    public GameObject bulletAttackFromAir;


    public int DamageForBoss = 3;

    public HpPlayer HpManager { get; set; }
    [SerializeField]
    public MovePlayerBeta Target { get; set; }

    public Animator Animator { get; set; }
    public AudioSource AudioSource { get; set; }
    public KindOfAttackEnemy AttackEnemy { get; set; }
    public DamageModel DamageModel { get; set; }
    public SpriteRenderer SpriteRenderer { get;set; }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        physicsBoss = GetComponent<Rigidbody2D>();
       
    }
    private void Update()
    {
        Move();
        Attack();
        JumpOrFly();
        OnePunch();
        AttackFromAir();
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.name == "Gran1" | collision.gameObject.name == "Gran2")
        {
           
             anim.SetBool("run", false);
             transform.localScale *= new Vector2(-1, 1);

        }
        
        
    }
    private void OnePunch()
    {
        if (hp.HealthPoint < 20)
        {
            speed += 20;
            anim.Play("atack5");
            damage += 10;
        }
    }
    private void Move()
    {
        anim.SetBool("pastpunch", false);
       physicsBoss.AddForce(new Vector2(transform.localScale.x, 0) * speed * Time.deltaTime);
       anim.SetBool("run", true);
       
    }
    private void JumpOrFly()
    {
        anim.SetBool("pastpunch", false);
        if (PLAYER.OnGround == true)
        {
            anim.SetBool("jump", false);
        }
        else
        {
            anim.SetBool("jump", true);
            physicsBoss.AddForce(new Vector2(0, PLAYER.transform.position.y) * jumpflyForce * Time.deltaTime);
        }

    }
    private void AttackFromAir()
    {
        if(PLAYER.OnGround == false)
        {
            transform.Translate(new Vector2(PLAYER.gameObject.transform.position.x, (PLAYER.gameObject.transform.position.y * -1)) * Time.deltaTime);
            
        }
    }
    private void Attack() {
        anim.SetBool("pastpunch", false);
        float NowHp = hpEnemy.HealthPoint;
        if (tr.Trigger == true & PLAYER.OnGround == true)
        {
            anim.StopPlayback();
            anim.Play("atack1");
            hpEnemy.HealthPoint -= (int)damage;
            if(hpEnemy.HealthPoint < NowHp)
            {
                anim.SetBool("needsecondatack", true);
                hpEnemy.HealthPoint -= (int)damage/2;

            }
            else
            {
                anim.SetBool("needsecondatack", false);
            }
        }
    
    }
    public void Hit()
    {
        TakeDamage(DamageForBoss);
    }
    public void TakeDamage(int damage)
    {
        float NowSpeed = speed, NowJump = jumpflyForce;
        if (hp.HealthPoint > 0)
        {

            hp.HealthPoint -= damage;
            anim.SetBool("pastpunch", true);
            speed = NowSpeed;
            jumpflyForce = NowJump;

        }
    }

    public void DoDamage()
    {
        throw new NotImplementedException();
    }
}
