using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Resources;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using System;
using Debug = UnityEngine.Debug;
using Assets.scripts.Player;
using Assets.scripts;
using Assets.scripts.Interfaces;
using Assets;
public class MovePlayerBeta : MonoBehaviour, IPunchable
{
    #region Movement
    [Header("Movement")]
    public float speedPlayer = 5f;

    public int jumpValue = 60;

    public float playerJumpForce = 1f;

    public int maxjumpvalue = 2;

    public float couldownFromUltimate = 5f;

    public float jumpFireFomUp = 2f;

    public int maxdoubleFireAtack = 1;


    public float  timeForBeginFly = 3f;

    public float timeBeforeMaxDrag = 3f;

    public float timeBeforeStay = 10f;

    public float snapAnimationMultiplier = 2f;

    public float forceDragging = 1f;

    public int Fastspeed = 10;

    [Space]

    #endregion
    #region Unity_depends
    [Header("Depends")]
    private Vector2 playerTransform;

    [NonSerialized]
    public Rigidbody2D rigidbodyPlayer;

    public ParticleSystem DustAfterRun;

    private TrailRenderer Trail;

    [NonSerialized]

    public ShotBullet BulletSystem;

    [NonSerialized]

    public HpPlayer HpSystem;
    #endregion
    #region Checkers
    [Header("Checkers")]
    public bool OnGround = true;

    public Transform GroundCheck;

    public float checkRadius = 0.5f;

    private Vector3 previousPosition;

    private float realspeed;

    private bool WasSnap = false;

    public bool IsFly;
    [NonSerialized]

    public bool NeeedTrail = false;
    [Space]
    #endregion
    #region Managers
    [Header("Managers")]
    public ControllerManager controllerManager;
    public PlayerAudioManager playerAudioManager;
    public PlayerAnimatorManager playerAnimatorManager;
    public AbilitiesManager playerAbilitiesManager;
    #endregion
    #region Client_settings
    // it is test func
    public Telekinesis objTelekinezis;
    [Space]
    #endregion
    #region Dash
    [Header("Dash")]
    public SwitchDash SwitchDash;
    private DashScript dashObj;
    #endregion
    #region Other
    public LayerMask masks;

    public Transform SpawnPoint;

    public VectorValue pos;

    public Transform FirePoint;

    public bool NoMovement;

    public float TimerOnSecond = 0f;

    private bool jumpiControl;

    private int jumpIteration = 0;

    private int jumpCount = 0;

    private int countWorkanim = 0;

    [NonReorderable]
    public bool Faceleft = true;

    private bool IsStart = true;
    // make a public
    [Space]
    #endregion
    [NonSerialized]
    public bool NonWork = true;
    #region Timers
    [Header("Timers")]
    private float TimerBeforeFly;
    private float TimerBeforeStay;
    private float TimerIncreaseBrakes;

    public DamageModel DamageModel { get; set; }
    #endregion

    private void Awake()
    {
        rigidbodyPlayer = GetComponent<Rigidbody2D>();
        transform.position = pos.playerVector;
        dashObj = GetComponent<DashScript>();
        previousPosition = transform.position;
        BulletSystem = GetComponent<ShotBullet>();
        Trail = GetComponent<TrailRenderer>();
        Trail.enabled = false;
        HpSystem = GetComponent<HpPlayer>();
    }
    private void Start()
    {
        transform.position = SpawnPoint.position;
        CheckPointsRegisterService.Register(this.gameObject.transform.position);
    }
    private void Update()
    {
        if (NeeedTrail) Trail.enabled = true;
        else
        {
            Trail.enabled = false;
        }

        if (NoMovement)
        {
            return;
        }

        foreach(var ability in playerAbilitiesManager.Abilities)
        {
            ability.Activate = true;
            ability.Invoke();
        }

        if (!OnGround)
        {
            TimerBeforeFly += Time.deltaTime;
        }
        else
        {
            TimerBeforeFly = 0f;
            playerAnimatorManager.MoveFly(false);
        }
        if (!NonWork)
        {
           
                Dash();

                Telekineze();
            return;
        }

        if (controllerManager.Mode == Mode.Keyboard )
        {
            if (Input.GetKeyDown(controllerManager.buttonForJump))
            {
                Jump();
               
            }
            Ultimate(Input.GetKey(controllerManager.buttonForUp));

            realspeed = Input.GetAxis("Horizontal") * speedPlayer;
        }
        else
        {
            Ultimate(controllerManager.Stick.Vertical >= 0.3f);
            realspeed = controllerManager.Stick.Horizontal * speedPlayer;

        }
        if (rigidbodyPlayer.gravityScale >= 1f)
        {
            Move(ref playerTransform, ref rigidbodyPlayer, "moveanimation", realspeed);
            Run("run");

        }
        if (rigidbodyPlayer.gravityScale <= 0f)
        {
            Move(ref playerTransform, ref rigidbodyPlayer, "movefly", realspeed);
            Run("fly");
        }
        Flip(playerTransform, ref Faceleft, ref FirePoint);

        Drag();

        CheckInGround();

        TimerOnSecond -= Time.deltaTime;
        
    }
    private void FixedUpdate()
    {
        Drag();
        Stay();
    }
    private void Telekineze() => objTelekinezis.Bouns(controllerManager.Stick.Vertical);
    private void Dash()
    {
        SwitchDash.valueSwitch = false;
        const float MaxVerticalValue = -0.9f;
        if(OnGround)
        {

            if(controllerManager.Mode == Mode.Phone & controllerManager.Stick.Vertical <= MaxVerticalValue)
            {
                NonWork = false;

                controllerManager.ButtonDash.SetActive(true);

                playerAnimatorManager.Dash(true); 

                dashObj.Dash(transform.localScale.x * -1 * transform.position.x, controllerManager.ButtonDash);
            }
            else if(controllerManager.Mode == Mode.Keyboard & Input.GetKeyDown(controllerManager.buttonForDown) & Input.GetKey(controllerManager.buttonForDash))
            {
                NonWork = false;

                playerAnimatorManager.Dash(true);

                dashObj.Dash(transform.localScale.x * -1 * transform.position.x, controllerManager.ButtonDash);
            }
           
        }
        else
        {
            NonWork = true;
            playerAnimatorManager.Dash(false);
            if (controllerManager.Mode == Mode.Phone)
            {
                controllerManager.ButtonDash.SetActive(false);
            }
        }
    }
    public void Move(ref Vector2 vsPlayer, ref Rigidbody2D rb2d, string textFromAnimation, float speed)
    {

        if(controllerManager.Mode == Mode.Phone)
        {
            vsPlayer.x = controllerManager.Stick.Horizontal;
        }
        else
        {
            vsPlayer.x = Input.GetAxis("Horizontal");
        }

        rb2d.AddForce(new Vector2(speed / 10, vsPlayer.y ));

        playerAnimatorManager.Move(textFromAnimation, Mathf.Abs(vsPlayer.x));           
    }
    public void Drag()
    {
        float playerMove = 0;
        if(controllerManager.Mode == Mode.Keyboard)
        {
             playerMove = Input.GetAxis("Horizontal");
        }
        else
        {
            playerMove = controllerManager.Stick.Horizontal;
        }

        if(previousPosition.x != transform.position.x & playerMove == 0 & OnGround)
        {
            TimerIncreaseBrakes += Time.fixedDeltaTime;

            playerAnimatorManager.Drag(true);

            CreateDust();

            if(TimerIncreaseBrakes >= timeBeforeMaxDrag)
            {
                TimerIncreaseBrakes = 0;
                rigidbodyPlayer.velocity = Vector2.zero;
            }
            else
            {
                rigidbodyPlayer.AddForce(new Vector2(previousPosition.x - transform.position.x, 0).normalized * forceDragging);
            }

        }
        else
        {
            playerAnimatorManager.Drag(false);
        }
        previousPosition = transform.position;

    }

    public void Flip(Vector2 vsPlayer,ref bool FaceFlip,ref Transform PointFromObject)
    {
        if(vsPlayer.x > 0 && !FaceFlip  || vsPlayer.x < 0 && FaceFlip)
        {
            transform.localScale *= new Vector2(-1, 1);
            FaceFlip = !FaceFlip;
            PointFromObject.Rotate(0f, 180f, 0f);
           
        }
       
    }
   
    public void Jump()
    {
      
        if (rigidbodyPlayer.gravityScale != 0f) {
            if (OnGround)
            {
                playerAnimatorManager.Jump();
                jumpiControl = true;
                CreateDust();

                rigidbodyPlayer.AddForce(Vector2.up * playerJumpForce);
            }
            else if(++jumpCount < maxjumpvalue) {

                playerAnimatorManager.DoubleJump();

                playerAudioManager.Jump(); 

                rigidbodyPlayer.velocity = new Vector2(0, 10);
                
            }
        }
        else
        {
            jumpiControl = false;
        }
        if (OnGround) jumpCount = 0;
        
        
    }
    void CheckInGround()
    {
        OnGround = Physics2D.OverlapCircle(GroundCheck.position, checkRadius, masks);

        playerAnimatorManager.OnGround(OnGround);

       

    }
    void Run(string IndexAnimation)
    {
        float change = 0;
        float comparer = -1f; // can be 1 or -1
        if(controllerManager.Mode == Mode.Keyboard)
        {
            change = Input.GetAxis("Horizontal");
        }
        else
        {
            change = controllerManager.Stick.Horizontal;
        }
        if(Faceleft)
        {
            comparer = 1f;
        }
        if (playerTransform.x <= comparer)
        {
            playerAnimatorManager.Run(IndexAnimation, true);    
            realspeed = Fastspeed * change;
        }
        else
        {
            playerAnimatorManager.Run(IndexAnimation, false);
            realspeed = speedPlayer * change;
            if (OnGround == true) playerAudioManager.Run();

        }
        
    }
    void Ultimate(bool flagStart)
    {
        if (flagStart)
        {
            IsFly = true;
            
            if (countWorkanim < maxdoubleFireAtack)
            {
                playerAnimatorManager.Fly();
                countWorkanim += 1;
            }

            Fly();
        }
        
        if (TimerOnSecond > 0)
        {
            TimerOnSecond -= Time.deltaTime;
        }
        if (TimerOnSecond <= 0 | !flagStart)
        {
            Falling();

            TimerOnSecond = couldownFromUltimate;
        }
    }
    void Stay()
    {
        if(TimerBeforeStay >= float.MaxValue)
        {
            TimerBeforeStay = 0;
        }
        if(realspeed <= 0 & !BulletSystem.WasShot)
        {
            TimerBeforeStay += Time.fixedDeltaTime;
            if (TimerBeforeStay >= (timeBeforeStay * snapAnimationMultiplier) & !WasSnap )
            {
                playerAnimatorManager.Snap(true);
                WasSnap = true;
                return;
            }
            if (TimerBeforeStay >= timeBeforeStay)
            {
                playerAnimatorManager.Stay(true);
                playerAnimatorManager.Snap(false);
            }
        }
        
        else
        {
            playerAnimatorManager.Stay(false);
            playerAnimatorManager.Snap(false);
            TimerBeforeStay = 0;
            WasSnap = false;
        }


    }
    void Fly()
    {

        rigidbodyPlayer.AddForce(new Vector2(0, 1) * (playerJumpForce + jumpFireFomUp) * Time.deltaTime);

        rigidbodyPlayer.gravityScale = 0f;

        playerAnimatorManager.Fly();

        playerAudioManager.Fly();

    }
    void Falling()
    {
          playerAudioManager.Stop();
          rigidbodyPlayer.gravityScale = 1f;
          playerAnimatorManager.Falling();
          TimerBeforeFly = 0f;
          IsFly = false;
    }
    void CreateDust()
    {
        DustAfterRun.Play();
    }

    public void HitFromPunch(DamageModel OuterDamageModel)
    {
        if (OuterDamageModel.From == this.gameObject) return; 
        HpSystem.HealthPoint -= OuterDamageModel.Damage;


    }
}
