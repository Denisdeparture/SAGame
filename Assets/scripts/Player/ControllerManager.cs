using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    public Mode Mode;
    //KeyBoard
    public KeyCode buttonForJump;
    public KeyCode buttonForUp;
    public KeyCode buttonForDash;
    public KeyCode buttonForDown;
    // Phone
    public Joystick Stick;


    public GameObject ButtonDash;

    public GameObject ButtonShot;

    public GameObject ButtonTelekinesis;

    public GameObject ButtonJump;

    public GameObject[] OtherObjects;

    void Update()
    {
        if (Mode != Mode.Keyboard)
        {
            bool value = true;
            ButtonDash.SetActive(value);
            Stick.gameObject.SetActive(value);
            ButtonTelekinesis.SetActive(value);
            ButtonShot.SetActive(value);
            ButtonJump.SetActive(value);
            foreach(var obj in OtherObjects)
            {
                obj.SetActive(value);
            }
        }
    }
}
public enum Mode
{
    Keyboard,
    Phone
}