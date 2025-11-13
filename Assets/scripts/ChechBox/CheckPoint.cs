using Assets.scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CheckPoint : MonoBehaviour
{
    private bool IsStay = false;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player" | collision.gameObject.name != "player") return;

        IsStay = true;
    }
    void Update()
    {
        if (IsStay)
        {
            CheckPointsRegisterService.Register(this.transform.position);
            IsStay = false;
        }
    }
}
