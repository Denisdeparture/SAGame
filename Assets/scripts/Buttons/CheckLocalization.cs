using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckLocalization : MonoBehaviour
{
    public GameObject loc;
    public GameObject notLoc;
    private void Update()
    {
        cheeck();
    }
    public void cheeck()
    {
        if(DataBase.playerInfo.WasLocalization == true)
        {
                loc.SetActive(true);
                notLoc.SetActive(false);
        }
        else if(DataBase.playerInfo.WasLocalization == false)
        {
                loc.SetActive(false);
                notLoc.SetActive(true);
        }
    }
}
