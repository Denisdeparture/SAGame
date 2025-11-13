using Assets.scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelChange : MonoBehaviour
{
    private Animator anim;
    public int levelToload;
    public Vector3 pozition;
    public VectorValue playercord;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void FadeToLevel(bool ActiveAnim)
    {
        anim.SetBool("End", ActiveAnim);
    }
    public void OnFadeComplete()
    {
        playercord.playerVector = pozition;
        DataBase.playerInfo.Level = levelToload;
        SceneManager.LoadScene(levelToload);
        /*
#if UNITY_WEBGL
        YandexDataBase.Instance.Save();
#endif
        */
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "player")
        {
            FadeToLevel(true);
            CheckPointsRegisterService.DeleteAll();
            OnFadeComplete();
        }
        else
        {
            FadeToLevel(false);
        }
    }
}
