using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ButtonClickPlay : MonoBehaviour
{
    public Vector3 pozition;
    public VectorValue playercord;
    public int levelToload;
  public void clickOnButton()
    {
        playercord.playerVector = pozition;
        SceneManager.LoadScene(levelToload);
        
    }
}
