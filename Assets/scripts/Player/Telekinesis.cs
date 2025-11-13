using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Telekinesis : MonoBehaviour
{
    Animator animatoObj;
    Rigidbody2D rigidbody2Dobj;
    public GameObject transformObj;
    private Rigidbody2D playerRightbody2d;
    public int ForceGravity;
    private bool redFlag;
    public SwitchTelekinesProperty Swither;
    public Sprite[] sprite;
    public Image MySprite;
    public float ForceBouns = 5f;
    public Animator playerAnimator;
    public MovePlayerBeta eventReact;
    public float efectBouns = 1000;

    private void Start()
    {
        playerRightbody2d = transformObj.GetComponent<Rigidbody2D>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision != null & collision.gameObject.CompareTag("notResisGravity"))
        {
                GameObject obj = collision.gameObject;
                animatoObj = obj.GetComponent<Animator>();
                rigidbody2Dobj = obj.GetComponent<Rigidbody2D>();
                
                if (Swither.valueSwitch == true)
                {
                    animatoObj.SetBool("Trig", true);
                    rigidbody2Dobj.AddForce(transformObj.transform.position * Time.fixedDeltaTime * ForceGravity);
                    redFlag = true;
                }
                else 
                {
                  
                  animatoObj.SetBool("Trig", false);
                  if(redFlag == true)
                  {
                    rigidbody2Dobj.AddForce(transformObj.transform.position * -1 * Time.fixedDeltaTime * ForceGravity); redFlag = false; 
                  }
                } 
        }
    }
    public void Bouns(float joysticVert)
    {
        if (Swither.valueSwitch)
        {

            MySprite.sprite = sprite[0];
            if (joysticVert <= -0.9 & eventReact.OnGround != true)
            {
                playerAnimator.Play("Homing");
                eventReact.NonWork = false;
                playerRightbody2d.AddForce(Vector2.down * Time.deltaTime * ForceBouns * efectBouns);

            }
            else 
            { 
            eventReact.NonWork = true;
            playerAnimator.SetBool("hom", false);
            }
        }
        else
        {
            MySprite.sprite = sprite[1];
        }
    }
}
