using Assets.scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NaughtyAttributes;
using Assets;
using System;
using Assets.scripts.Interfaces;
[RequireComponent(typeof(NavMeshAgent), typeof(Animator), typeof(AudioSource))]
public class Enemy : MonoBehaviour, IEnemy, IPunchable
{
    public float speed = 4f;

    public float distance = 2f;

    public float mindistance = 1f;
    [HideIf(nameof(AttackEnemy), KindOfAttackEnemy.Distant)]

    public string NameOfAttackAnimation;

    public string NameOfDamageAnimation;

    [HideIf(nameof(AttackEnemy), KindOfAttackEnemy.Near)]

    public string NameOfShotAnimation;

    public float health;

    public float damageForPlayer = 1;

    public float timerBetweenAttack = 3f;

    public float timerAfterHit = 3f;

    private float TimerAfterHit = 0f;
    private float TimerBefore = 0f;

    private float OldPlayerHp;

    public GameObject Player;
    public HpPlayer HpManager { get; set; }

    public Animator Animator { get ; set ; }
    public AudioSource AudioSource { get; set; }
    public MovePlayerBeta Target { get; set; }

    [field: SerializeField]
    public KindOfAttackEnemy AttackEnemy { get; set; }
    public DamageModel DamageModel { get; set; }
    public SpriteRenderer SpriteRenderer { get; set; }

    [HideIf(nameof(AttackEnemy), KindOfAttackEnemy.Near)]
    public Bullet bullet;
    [HideIf(nameof(AttackEnemy), KindOfAttackEnemy.Distant)]
    public Punch punch;
    private NavMeshAgent agent;

    private bool PlayerIsHere = false;
    private bool NoAttack = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        Animator = GetComponent<Animator>();
        AudioSource = GetComponent<AudioSource>();
        HpManager = Player.GetComponent<HpPlayer>();
        Target = Player.GetComponent<MovePlayerBeta>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        OldPlayerHp = HpManager.HealthPoint;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        var hp = collision.gameObject.GetComponent<HpPlayer>();

        if (hp is null) return;

        HpManager = hp;

        PlayerIsHere = true;
    }
    
    private void FixedUpdate()
    {
        if (health <= 0) Death();
        Moving();
        DoDamage();
        if (WasDamage())
        {
            if(TimerAfterHit < timerAfterHit)
            {
                TimerAfterHit += Time.fixedDeltaTime;
                NoAttack = true;
            }
            else
            {
                NoAttack = false;
                TimerAfterHit = 0;
            }
        }
    }
    public void DoDamage()
    {
        var length = Vector2.Distance(Player.transform.position,this.transform.position);
        OldPlayerHp = HpManager.HealthPoint;
        if(length >= distance)
        {
            Attack(length);
        }
    }
    public bool WasDamage()
    {
        return OldPlayerHp == HpManager.HealthPoint;
    }
    private void Attack(float distance)
    {
        if (NoAttack) return;
        if(AttackEnemy == KindOfAttackEnemy.Distant)
        {
            PlayerIsHere = true;
            // or create firePoint like a player
            bullet.damage = damageForPlayer;

            AttackPart(() => ShotBullet.SimpleShotBullet(bullet, this.transform.position, this.transform.rotation));
           
        }
        if(AttackEnemy == KindOfAttackEnemy.Near)
        {
            if(distance <= mindistance)
            {
                AttackPart(() => CombatManager.CreatePunch(punch, this.transform.position, this.transform.rotation));
            }
        }

    }

    private void AttackPart(Action action)
    {
        if (TimerBefore < timerBetweenAttack)
        {
            TimerBefore += Time.fixedDeltaTime;
            return;
        }
        else
        {
            action();
            // Add Aninmation and sources
            TimerBefore = 0;
            PlayerIsHere = false;
        }
    }
    private void Moving()
    {
        Flip();

        if (PlayerIsHere) return;
        
        agent.SetDestination(Target.transform.position);
    }
    public void Flip()
    {
        if (Target.Faceleft)
        {
            SpriteRenderer.flipX = false;
        }
        else
        {
            SpriteRenderer.flipX = true;
        }
    }
    public void Hit()
    {
        health -= DamageModel.Damage;
    }
    private void Death()
    {
        Destroy(this.gameObject);
    }

    public void HitFromPunch(DamageModel OuterDamageModel)
    {
        if (OuterDamageModel.From == this.gameObject) return;
        health -= OuterDamageModel.Damage;
    }
}
