using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataBase : MonoBehaviour
{
    public static PlayerInfo playerInfo = new PlayerInfo();
}
public class PlayerInfo
{
    [NonSerialized]
    public int quantityRings;
    [NonSerialized]
    public bool skinpurchased = false;
    [NonSerialized]
    public bool WasLocalization = false;
    [NonSerialized]
    public int Level;
    [NonSerialized]
    public int Coins;
}
