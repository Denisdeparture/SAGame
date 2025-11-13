using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Bar : MonoBehaviour
{
    public Slider slider;
    protected abstract void SetBase(float val, float max);

    protected abstract void SetNow(float val);


}
