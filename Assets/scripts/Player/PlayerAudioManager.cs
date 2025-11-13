using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    public AudioClip jumpA;

    public AudioClip RunA;

    public AudioClip FlyA;

    public AudioClip ShotA;

    private AudioSource Source;

    void Awake()
    {
        Source = this.GetComponent<AudioSource>();
    }

    public void Stop()
    {
        Source.Stop();
    }
    public void Shot()
    {
        Source.PlayOneShot(ShotA);
    }
    public void Run()
    {
        Source.PlayOneShot(RunA);
    }
    public void Fly()
    {
        Source.PlayOneShot(FlyA, 0.5f);
    }
    public void Jump()
    {
        Source.PlayOneShot(jumpA);
    }
}
