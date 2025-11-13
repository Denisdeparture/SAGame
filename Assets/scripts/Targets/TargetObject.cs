using Assets.scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetObject : MonoBehaviour, IOpportunityForHoming
{
    public GameObject target;
    public GameObject Target { get; set; }
    public Vector2 NowPosition { get; set; }
    public HomingModel HomingModel { get; set; }

    public float timeBeforeRespawn = 3f;

    private float TimerBefore = 0f;
    public int id;
    public int Id { get; set; }

    private bool isHoming = false;

    private bool isSpawning = false;

    private void Start()
    {
        Id = id;
        NowPosition = gameObject.transform.position;
        Target = target;
        HomingModel = new HomingModel();
        HomingModel.Condition = HomingModel_ConditionForGet;
        HomingModel.ConditionForGet += HomingModel_ConditionForGet;
    }
    private bool HomingModel_ConditionForGet()
    {
        return isHoming;
    }
    public void TangentEffect()
    {
        this.gameObject.SetActive(false);
        isHoming = true;
    }
}
