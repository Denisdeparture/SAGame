using Assets.scripts;
using Assets.scripts.Player;
using Assets.scripts.Skills;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SideBarOpen : MonoBehaviour
{
    public Animator animator;

    public Button button;

    public KeyCode keyForInvoke;

    public KeyCode keyForChoose;

    public KeyCode keyForAccept;

    [NonSerialized]
    public bool isOpen = false;

    private bool isStart = true;

    public bool isConcentrate = false;

    public float timerBeforeSwipe = 3f;

    private int counter = 0;

    private float TimerBeforeSwipe = 0f;

    public List<SkillUIObject> SpritesAbilities = new List<SkillUIObject>();

    public AbilitiesManager PlayerAbilityManager;

    [NonSerialized]

    public Ability OuterModel;

    public event EventHandler OnExit;

    private void Start()
    {
        counter = SpritesAbilities.Count - 1;

        button.navigation = new Navigation { mode = Navigation.Mode.None };
    }
    private void Update()
    {
        if (!isConcentrate)
        {
            InvokeOnKey();
        }
        
    }
    private void FixedUpdate()
    {
        if (counter < 0) counter = SpritesAbilities.Count - 1;

        if (isConcentrate)
        {
            AcceptSwitch(counter);

            isOpen = true;

            animator.SetBool("IsOpen", isOpen);

            if (isStart)
            {
                SetActiveChild(counter, true);
            }

            if (Input.GetKey(keyForChoose))
            {
                isStart = false;
                if (TimerBeforeSwipe < timerBeforeSwipe)
                {
                    TimerBeforeSwipe += Time.fixedDeltaTime;
                    return;
                }
                else
                {
                    counter -= 1;
                    if (counter < 0) counter = SpritesAbilities.Count - 1;
                    SetActiveChild(counter, true);
                    for (int i = 0; i < SpritesAbilities.Count; i++)
                    {
                        if (i == counter) continue;

                        SetActiveChild(i, false);
                    }
                    TimerBeforeSwipe = 0f;
                }
                
            }
            return;
        }
    }
    private void AcceptSwitch(int id)
    {
        if (Input.GetKey(keyForAccept))
        {

            if (OuterModel is null) return;

            var spriteRenderer = SpritesAbilities[id].Window.GetComponent<Image>();

            if (OuterModel is null)
            {
                Debug.Log("Outer Model is null"); 
            }
            if (spriteRenderer is null)
            {
                Debug.Log("Sprite renderer is null");
            }
            spriteRenderer.sprite = OuterModel.GetSkill().spriteInSideBar;

            PlayerAbilityManager.Abilities.Add(OuterModel);

            SetActiveChild(id, false);

            Exit();
        }
    }
    private void SetActiveChild(int id, bool condition)
    {
        var ui = SpritesAbilities[id];
   
        var model = ui.Window;

        (ui.gameObject.GetComponent<Animator>() as Animator).SetBool("IsChosed", condition);

        ui.Arrow.SetActive(condition);
    }
    private void Exit()
    {
        OuterModel = null;
        isOpen = false;
        isConcentrate = false;
        OnExit.Invoke(this, null);
    }
   
    public void ClickOnSideBar()
    {
        isOpen = !isOpen;
        animator.SetBool("IsOpen", isOpen);
    }
    private void OnMouseEnter()
    {
        InvokeOnMouse();
    }

    private void InvokeOnKey()
    {
        if (Input.GetKeyDown(keyForInvoke))
        {
            button.onClick.Invoke();
            ClickOnSideBar();
        }
    }
    private void InvokeOnMouse()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                button.onClick.Invoke();
                ClickOnSideBar();
            }
        }
    }

}
