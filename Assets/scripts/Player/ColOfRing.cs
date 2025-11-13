using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ColOfRing : MonoBehaviour
{
    public TextMeshProUGUI text;
    private void Start()
    {
        int numtext = int.Parse(text.text);
        numtext += DataBase.playerInfo.Coins;
        text.text = $"{numtext}";
    }
}
