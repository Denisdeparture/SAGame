using Assets.scripts.Skills;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityCut : Ability
{
    private SkillGearModel Model;

    public AnimationClip AnimationModel;

    public AudioClip BaseAudioClip;

    public MovePlayerBeta Player;
    void Start()
    {
        Model = GetComponent<SkillGearModel>();

        ConfigureAbility(AnimationModel, Model, BaseAudioClip);
    }

    public override void OtherFunction()
    {
        return;
    }
    public override void OtherConfiguration()
    {
        Model.bulletSystem.AddBullet(Condition, Model.bullet);
    }
    public override bool Condition()
    {
        return Player.IsFly == true;
    }
   
}
