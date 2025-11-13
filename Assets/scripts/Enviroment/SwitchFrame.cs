using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchFrame : MonoBehaviour
{
    public GameObject NowFrame;
    public GameObject PastFrame;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            NowFrame.SetActive(true);
            PastFrame.SetActive(false);
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            NowFrame.SetActive(false);
            PastFrame.SetActive(true);
        }
    }
}
