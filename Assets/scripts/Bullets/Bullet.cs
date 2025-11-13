using Assets.scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;

    public float lifeFromBullet;

    public float distance;

    public float damage;

    public LayerMask bullmasks;

    public BulletFor Kind;

    private int iterator = 1;

    bool redflag = false;

    public bool IPrototype = false;
    private void Update()
    {
        HitBullet();
    }
    public void HitBullet()
    {
        if (Kind == BulletFor.Player)
        {
            RaycastHit2D hitinfo = Physics2D.Raycast(transform.position, transform.up, distance, bullmasks);
            if (hitinfo.collider != null)
            {
                var comp = hitinfo.collider.GetComponent<ISusceptibleToBullets>();
                if (comp is not null)
                {
                    comp.DamageModel = new Assets.DamageModel();
                    comp.DamageModel.Damage = damage;
                    comp.Hit();
                }
                Destroy(gameObject);
            }
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        if(Kind == BulletFor.Enemy)
        {
            redflag = true;
            transform.Translate(new Vector2(1 * iterator, 0) * speed * Time.deltaTime);
            iterator *= -1;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" & redflag == true)
        {
            var HpManager = collision.gameObject.GetComponent<HpPlayer>();

            if (HpManager == null) { return; }

            HpManager.HealthPoint -= damage;
        }
    }
}
public enum BulletFor
{
    Enemy,
    Player
}
