using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public GameObject[] ActiveObject;
    public GameObject[] NotActiveObjects;
    // массивы должны быть одиннаковыми
    public void enumerator(bool Activator)
    {
        DataBase.playerInfo.WasLocalization = Activator;
        if (DataBase.playerInfo.WasLocalization == true)
        {
            for (int i = 0; i != ActiveObject.Length; i++)
            {
                ActiveObject[i].SetActive(Activator);
                NotActiveObjects[i].SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i != ActiveObject.Length; i++)
            {
                NotActiveObjects[i].SetActive(true);
            }
        }
    }
}
