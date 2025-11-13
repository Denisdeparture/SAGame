using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace Assets.scripts.Skills
{
    public class PickUpAbility : MonoBehaviour
    {
        public KeyCode KeyForPickUp;

        public Ability Model;

        public float timerForMovementToSideBar = 3f;

        public float YPositionOnMove = 1f;
        public float XPositionOnMove = -1f;

        public float speedMoveToSideBar;

        private float TimerBeforeSidebar;

        private bool isExit;

        public Hint hint; // Do another script

        public MovePlayerBeta Player;


        public GameObject DimLightPanel;

        public SideBarOpen SideBar;

        private void Start()
        {
            hint.KeyCodeBase = KeyForPickUp;
        }
        private void FixedUpdate()
        {
            InnerExit();
        }
        // Outer api
        public void ChnageKey(KeyCode key)
        {
            hint.KeyCodeBase = key;
            KeyForPickUp = key;
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.name == "player" | collision.CompareTag("Player"))
            {
                hint.gameObject.SetActive(true);

                if (Input.GetKey(KeyForPickUp))
                {
                    Player.NoMovement = true;

                    SideBar.isConcentrate = true;

                    SideBar.OuterModel = Model;

                    DimLightPanel.SetActive(true);

                    SideBar.OnExit += Exit;
                }
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.name == "player" | collision.CompareTag("Player"))
            {
                hint.gameObject.SetActive(false);
            }
        }
        private void Exit(object sender, EventArgs e) => isExit = true;
        private void InnerExit()
        {
      

            if (!isExit) return;

            if (TimerBeforeSidebar < timerForMovementToSideBar)
            {
                TimerBeforeSidebar += Time.fixedDeltaTime;
                DimLightPanel.SetActive(false);
                Player.NoMovement = false;
                Move();
                return;
            }

            SideBar.isConcentrate = false;

            SideBar.isOpen = false;

            isExit = false;

            TimerBeforeSidebar = 0;

            var spriteRenderer = this.gameObject.GetComponentInChildren<SpriteRenderer>();

            spriteRenderer.sprite = null;

        }
        private void Move()
        {
            transform.Translate(new Vector2(XPositionOnMove, YPositionOnMove) * Time.fixedDeltaTime * speedMoveToSideBar, Space.World);
        }
    }
}




