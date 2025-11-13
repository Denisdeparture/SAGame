using Assets.scripts;
using Assets.scripts.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Target : MonoBehaviour
{
    [NonSerialized]
    public List<GameObject> Targets = new List<GameObject>();
    [NonSerialized]
    public bool IsActivate = false;
    [NonSerialized]
    public IOpportunityForHoming TargetObjectInfo;
    private void OnTriggerStay2D(Collider2D collision)
    {
        var target = collision.gameObject.GetComponent<IOpportunityForHoming>();

        if (target is null)
        {
            return;
        }

        bool redflag = true;

        foreach (var t in Targets)
        {
            if (t.gameObject.GetComponent<IOpportunityForHoming>().Id == target.Id)
            {
                redflag = false; break;
            }
        }

        if (redflag)
        {
            var tar = collision.gameObject.GetComponentInChildren<TargetPoint>();

            if(tar is not null)
            {
                Debug.Log("Is not null");
                tar.gameObject.SetActive(true);
            }

            Targets.Add(collision.gameObject);
        }

        Targets.Add(collision.gameObject);
        Triggering(collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        foreach(var target in Targets)
        {
            var tar = target.GetComponentInChildren<TargetPoint>();

            if (tar is null) continue;

            tar.gameObject.SetActive(false);

            TargetObjectInfo = null;
        }
    }
    private void Triggering( Collider2D collision)
    {

        if (Targets.Count == 0) return;

        var nearestGameObjPos = Targets[0].transform.position;
        IOpportunityForHoming homPoint = null;
        foreach (GameObject target in Targets)
        {
            if (target.name == collision.gameObject.name | target.tag == collision.gameObject.tag)
            {

                IsActivate = true;
                
                var hominger = collision.gameObject.GetComponent<IOpportunityForHoming>();

                if (hominger is null)
                {
                    break;
                }

                var pos = collision.gameObject.transform.position;

                hominger.Target.SetActive(true);

                if (pos.x > nearestGameObjPos.x | pos.y > nearestGameObjPos.y) continue;
                else
                {
                    nearestGameObjPos = pos;
                    homPoint = hominger;
                }
            }
        }
        if(homPoint is not null)
        {
            homPoint.Target.SetActive(true);
            TargetObjectInfo = homPoint;
        }
    
    }
    private void Update()
    {
        Targets.RemoveAll(x => x == null || x.active == false);
    }
}
