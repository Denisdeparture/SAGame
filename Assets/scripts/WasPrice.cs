using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasPrice : MonoBehaviour
{
    public float Price;
    public GameObject[] DoSwitch;
    public void revision()
    {
        if (DataBase.playerInfo.Coins >= Price)
        {
            DoSwitch[0].SetActive(false);
            DoSwitch[1].SetActive(true);
            DataBase.playerInfo.skinpurchased = true;
        }
        else
        {
            DataBase.playerInfo.skinpurchased = false;
        }
    }
}
