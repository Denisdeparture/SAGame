using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackFunction : MonoBehaviour
{
    public GameObject[] buttonReturn;
    public void backFunc()
    {
        foreach (var button in buttonReturn)
        {
            button.SetActive(true);
        }
    }
}
