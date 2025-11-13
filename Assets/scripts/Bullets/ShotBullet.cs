using Assets.scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class ShotBullet : MonoBehaviour
{
    public Transform startingPoint;

    public float liveBulletTime = 3f;

    public int MaxBullets = 5;

    public MouseButton mouseButton;

    public float timeRecharge = 4f;

    public int RateOfFire = 1;

    public float offsettingTheFiringPoint = 0.2f;

    public float IntervalBetweenShot = 1f;

    private float TimerBetweenShot = 0;

    private float TimerLiveBullet = 0f;

    public float CounterBullets;

    private float TimerBeforeRecharge = 0f;

    [NonSerialized]

    public bool WasShot = false;

    public List<Bullet> BaseBullets;

    public MovePlayerBeta Player;

    public PlayerAudioManager audioManager;

    public ControllerManager controllerManager;

    public PlayerAnimatorManager animatorManager;


    public float Recharge { get => timeRecharge - TimerBeforeRecharge; }
    public float NowBullets => MaxBullets - CounterBullets; 
    private void Start()
    {
        var bullet = new BulletModel()
        {
            Id = 0,

            Object = BaseBullets[0],

            Condition = Bullet_ConditionForGet
        };
        bullet.ConditionForGet += Bullet_ConditionForGet;

        DataBullets.Add(bullet);

    }

    private bool Bullet_ConditionForGet()
    {
        return Player.IsFly == false;
    }

    // This Methor created for outer ability
    #region ForAbilites
    public void AddBullet(BulletModelDelegate condition, Bullet bullet)
    {
        var model = new BulletModel
        {
            Id = DataBullets.GetAll().Count,

            Object = bullet,

            Condition = condition
        };

        model.ConditionForGet += condition;

        DataBullets.Add(model);
    }

    public void DeleteBullet(int id)
    {
        DataBullets.Delete(id);
    }
    #endregion
    private void DynamicUpdateService()
    {
        var model = DataBullets.GetAll();

        int length = DataBullets.GetAll().Count;

        for (int counter = 0; counter < length; counter++) 
        {
            model[counter].NowConditionValueUpdate();

            DataBullets.UpdateConditionFor(counter, model[counter].Condition);
        }
    }
    private void FixedUpdate()
    {
        DynamicUpdateService();

        float liveBulletTimeLocal = liveBulletTime / 10;

        WorksWithTimer();

        ClearBulletsOnScene(ref liveBulletTimeLocal);

        ControlShot();
    }
    public void Shotting()
    {
        if (CounterBullets < MaxBullets)
        {
            if (TimerBetweenShot >= IntervalBetweenShot)
            {
                foreach (var bulletModel in DataBullets.GetAll())
                {
                    bulletModel.NowConditionValueUpdate();

                    if (bulletModel.Condition())
                    {
                        audioManager.Shot();

                        animatorManager.Shot();

                        bulletModel.Object.IPrototype = false;

                        SimpleShotBullet(bulletModel.Object, startingPoint.position, startingPoint.rotation);

                        CounterBullets += 1;

                        WasShot = true;
                    }


                }
            }
            
        }
    }
    private void ClearBulletsOnScene(ref float liveBulletTimer)
    {
        if (TimerLiveBullet >= liveBulletTimer)
        {
            TimerLiveBullet = 0f;

            var bullets = FindObjectsByType<Bullet>(sortMode: FindObjectsSortMode.InstanceID).Where(c => c.IPrototype == false);
            foreach (var bullet in bullets)
            {
                bullet.gameObject.SetActive(false);
                Destroy(bullet.gameObject);
            }
        }
    }
    private void WorksWithTimer()
    {
        if (TimerBetweenShot < IntervalBetweenShot)
        {
            TimerBetweenShot += Time.fixedDeltaTime;
            return;
        }

        if (CounterBullets >= MaxBullets)
        {
            TimerBeforeRecharge += Time.fixedDeltaTime;
        }
        if (TimerBeforeRecharge >= timeRecharge)
        {
            CounterBullets = 0;
            TimerBeforeRecharge = 0;
        }

        if (WasShot)
        {
            TimerLiveBullet += Time.fixedDeltaTime;
            WasShot = false;
        }
    }
    private void ControlShot()
    {
        if (controllerManager.Mode == Mode.Keyboard & Input.GetMouseButton(0))
        {
            int symbol = -1;
            foreach (var i in Enumerable.Range(0, RateOfFire))
            {
                Shotting();
                startingPoint.position = new Vector3(this.transform.position.x + symbol + offsettingTheFiringPoint, this.transform.position.y, 0);
                symbol *= -1;
            }
            TimerBetweenShot = 0f;
        }
    }
    public static void SimpleShotBullet(Bullet bullet, Vector3 startPosition, Quaternion startRotation)
    {
        Instantiate(bullet, startPosition, startRotation);
    }

}
