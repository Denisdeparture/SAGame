using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPartRampa : MonoBehaviour
{
    public PolygonCollider2D[] cr2D;
    public Animator animPlayer;
    public MovePlayerBeta player;
    public float Speedplus;
    public  AudioClip audio;
    public AudioSource source;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "player")
        {
            source.PlayOneShot(audio);
            cr2D[0].enabled = false;
            cr2D[1].enabled = true;
            animPlayer.SetBool("rampo", true);
            player.speedPlayer += Speedplus;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "player")
        {
            cr2D[0].enabled = true;
            cr2D[1].enabled = false;
            player.speedPlayer -= Speedplus;
            animPlayer.SetBool("rampo", false);
        }
    }
}
