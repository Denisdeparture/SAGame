using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class ActiveButton : MonoBehaviour
{
    public GameObject[] NotActiveButtons;
    public ActionsAfterPressingtheButton ActiveButtons;

    public void re()
    {
        ActiveButtons.Elective = NotActiveButtons.Length - 1;
        ActiveButtons.ActiveButton(NotActiveButtons);
    }
     
}
