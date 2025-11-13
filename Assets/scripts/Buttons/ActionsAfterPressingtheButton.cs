using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsAfterPressingtheButton : MonoBehaviour
{
    [NonSerialized]
    public int Elective;
    public void ActiveButton(GameObject[] NotActiveButton)
    {
        for(int i = 0; i < NotActiveButton.Length; i++)
        {
            if (i == Elective)
            {
                NotActiveButton[i].SetActive(true);
            }
            else
            {
                NotActiveButton[i].gameObject.SetActive(false);
            }
        }
    }
   
}
