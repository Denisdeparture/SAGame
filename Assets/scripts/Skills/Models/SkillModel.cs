using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.scripts.Skills
{
    public class SkillModel : MonoBehaviour
    {
        /// <summary>
        /// Always animations use setbool default
        /// </summary>
        public Animator? animator;
       
        public AudioSource? audioSource;

        public KeyCode keyForActivated;

        public bool isMouseActivated;

        public MouseButton buttonMouse;
       

        public Sprite spriteOnGround; // Оно здесь на случай если игрок захочет выкинуть текущий скилл

        public Sprite spriteInSideBar;
    }
   
}
