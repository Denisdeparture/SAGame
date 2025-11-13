using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

class DashScript : MonoBehaviour
{
    private Rigidbody2D playerRg2;
    private Animator animator;
    public float SpeedDash;
    public SwitchDash buttonContol;
    public GameObject AuraDashAttack;
    public ControllerManager Manager;
    private void Start()
    {
        playerRg2 = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        animator.SetBool("dashAttack", false);
    }
    public void Dash(float xPos,GameObject button)
    {
        if(Manager.Mode == Mode.Phone ? buttonContol.valueSwitch == true : Input.GetKey(Manager.buttonForDash))
        {
            AuraDashAttack.SetActive(true);
            animator.SetBool("dashAttack", true);
            playerRg2.AddForce(new Vector2(xPos, transform.position.y) * Time.deltaTime * SpeedDash);
            button.SetActive(false);
        }
        AuraDashAttack.SetActive(false);

    }
}
