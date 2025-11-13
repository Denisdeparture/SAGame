using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaBar : Bar
{
    public ShotBullet playerBulletController;

    private void Update()
    {
        SetNow(playerBulletController.NowBullets);
    }
    private void Start()
    {
        SetBase(playerBulletController.NowBullets, playerBulletController.MaxBullets);
    }
    protected override void SetBase(float val, float max)
    {
        slider.maxValue = max;

        slider.value = val;
    }

    protected override void SetNow(float val)
    {
        slider.value = val;
    }
}
