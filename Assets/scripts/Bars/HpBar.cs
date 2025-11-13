using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : Bar
{
    public HpPlayer playerhealt;
    private void Update()
    {
        SetNow(playerhealt.HealthPoint);
    }
    private void Start()
    {
        SetBase(playerhealt.HealthPoint, playerhealt.maxHp);
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
