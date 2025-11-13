using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWaveEffect : MonoBehaviour
{
    public float shockWaveTimer = 0.75f;

    public float startPos = -0.1f;

    public float endPos = 1f;

    private float TimerBefore = 0f;

    private Material Material;

    private static int waveDistant = Shader.PropertyToID("_WaveDistant");

    private bool activate = false;

    private float lerpAmount = 0f;
    public void CallEffect()
    {
        activate = true;
    }
    private void Effect()
    {
        
        if(TimerBefore < shockWaveTimer)
        {

            TimerBefore += Time.fixedDeltaTime;

            lerpAmount = Mathf.Lerp(startPos, endPos, (TimerBefore / shockWaveTimer));

            Material.SetFloat(waveDistant, lerpAmount);

            return;
        }
        Disactivate();
    }
    private void Disactivate()
    {
        Material.SetFloat(waveDistant, startPos);
        TimerBefore = 0f;
        lerpAmount = 0f;
        activate = false;
    }
    private void Start()
    {
        Material = GetComponent<SpriteRenderer>().material;   
    }

    private void FixedUpdate()
    {
        if (!activate) return;

        Effect();
    }
}
