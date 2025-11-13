using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartMenu : MonoBehaviour
{
  public void Go()
    {
        SceneManager.LoadScene(DataBase.playerInfo.Level + 1);
    }
}
