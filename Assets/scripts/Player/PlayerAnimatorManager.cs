using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorManager : MonoBehaviour
{
    public Animator Workanim;
    private string OneAnimation = string.Empty;



    private void Update()
    {
        Workanim.StartPlayback();
    }
    public void SetOneAnimation(string oneAnim, bool cond)
    {
        if(cond)
        {
            OneAnimation = oneAnim;
        }
        else
        {
            OneAnimation = string.Empty;
        }
    }
    public void Stop()
    {
        Workanim.StopPlayback();
    }
    public void Respawn()
    {
        if (OneAnimation != string.Empty && OneAnimation != nameof(Respawn)) return;
        Workanim.SetTrigger("Respawn");
    }
    public void Run(string kindOfRun, bool condition)
    {
        if (OneAnimation != string.Empty && OneAnimation != nameof(Run)) return;

        Workanim.SetBool(kindOfRun, condition);
    }
    public void RampaForm(bool cond)
    {
        if (OneAnimation != string.Empty && OneAnimation != nameof(RampaForm)) return;


        Workanim.SetBool("rampo", cond);
    }
    public void Jump()
    {
        if (OneAnimation != string.Empty && OneAnimation != nameof(Jump)) return;

        Workanim.StopPlayback();
        Workanim.Play("Jump_Silver_Jump");
    }
    public void Move(string kindofmove, float value)
    {
        if (OneAnimation != string.Empty && OneAnimation != nameof(Move)) return;

        Workanim.SetFloat(kindofmove, value);
    }
    public void Stay(bool stay)
    {
        if (OneAnimation != string.Empty && OneAnimation != nameof(Stay)) return;


        Workanim.SetBool("NoMovment", stay);
    }
    public void Snap(bool snap)
    {
        if (OneAnimation != string.Empty && OneAnimation != nameof(Snap)) return;


        Workanim.SetBool("GoSnap", snap);
    }
    public void Drag(bool needDrag)
    {
        if (OneAnimation != string.Empty && OneAnimation != nameof(Drag)) return;


        Workanim.SetBool("IsDragging",needDrag);
    }
    public void Shot()
    {
        if (OneAnimation != string.Empty && OneAnimation != nameof(Shot)) return;


        Workanim.StopPlayback();
        Workanim.Play("Fire1");
    }
    public void Fire()
    {
        if (OneAnimation != string.Empty && OneAnimation != nameof(Fire)) return;

        Workanim.StopPlayback();
        Workanim.Play("Fire2");
    }
    public void Falling()
    {
        if (OneAnimation != string.Empty && OneAnimation != nameof(Falling)) return;


        Workanim.SetBool("gofly", false);
        Workanim.StopPlayback();
    }
    public void MoveFly(bool cond)
    {
        if (OneAnimation != string.Empty && OneAnimation != nameof(MoveFly)) return;

        Workanim.SetBool("gofly", cond);
    }
    public void Fly()
    {
        if (OneAnimation != string.Empty && OneAnimation != nameof(Fly)) return;

        Workanim.SetBool("gofly", true);
    }
    public void Dash(bool now)
    {
        if (OneAnimation != string.Empty && OneAnimation != nameof(Dash)) return;


        Workanim.SetBool("dash", now);
    }
    public void DoubleJump()
    {
        if (OneAnimation != string.Empty && OneAnimation != nameof(DoubleJump)) return;


        Workanim.StopPlayback();
        Workanim.Play("doublejump");
    }
    public void Homing(bool cond)
    {
        if (OneAnimation != string.Empty && OneAnimation != nameof(Homing)) return;


        Workanim.SetBool("hom", cond);
    }
    public void OnGround(bool onGround)
    {
        if (OneAnimation != string.Empty && OneAnimation != nameof(OnGround)) return;


        Workanim.SetBool("onGround", onGround);
    }

}