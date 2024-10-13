using PolyPerfect;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [Header("PlayerInput")]
    [SerializeField] PlayerInput playerInput;
    [SerializeField] Rigidbody rigid;
    [SerializeField] public float movePower;
    [SerializeField] public float jumpPower;
    public float sprintSpeed = 3;
    public float currentSpeed = 0;
    public bool rolling;
    public bool isRunning;

    public InputAction playerControls;
    public InputAction playerMove;
    public InputAction playerRun;
    public InputAction playerRoll;
    public InputAction playerAttack;

    private Vector2 movement = Vector2.zero;
    private Vector3 direction = Vector3.zero;



    [Header("AnimationController")]
    [SerializeField] Animator animator;

    private static int curAniHash;
    private static int idleHash = Animator.StringToHash("Idle_ver_B");
    // Walk FBLR
    private static int walkFHash = Animator.StringToHash("Walk_ver_B_Front");
    private static int walkBHash = Animator.StringToHash("Walk_ver_B_Back");
    private static int walkLHash = Animator.StringToHash("Walk_ver_B_Front_L90");
    private static int walkRHash = Animator.StringToHash("Walk_ver_B_Front_R90");
    // run FBLR
    private static int runFHash = Animator.StringToHash("Jogging_8Way_verB_F");
    private static int runBHash = Animator.StringToHash("Jogging_8Way_verB_BL45");
    private static int runLHash = Animator.StringToHash("Jogging_8Way_verB_L90");
    private static int runRHash = Animator.StringToHash("Jogging_8Way_verB_R90");
    // Roll FBLR
    private static int rollFHash = Animator.StringToHash("Dodge_Front");
    private static int rollBHash = Animator.StringToHash("Dodge_Back");
    private static int rollLHash = Animator.StringToHash("Dodge_Right");
    private static int rollRHash = Animator.StringToHash("Dodge_Right");




    public enum State { Idle, WalkF, WalkB, WalkL, WalkR, RunF, RunB, RunL, RunR,
        RollF, RollB, RollL, RollR, Attack1, Attack2, Attack3, Size }
    [SerializeField] State currentState;
    private BaseState[] state = new BaseState[(int)State.Size];




    #region STATE
    private class PlayerState : BaseState
    {
        public InputController player;
        public PlayerState(InputController player)
        {
            this.player = player;
        }
    }
    public void ChangeState(State nextState)
    {
        state[(int)currentState].Exit();
        currentState = nextState;
        state[(int)currentState].Enter();
    }

    private class IdleState : PlayerState
    {
        public IdleState(InputController player) : base(player)
        {
        }
        public override void Update()
        {
            player.AnimatorPlay();
        }
    }
    #region WalkSTATE
    private class WalkFState : PlayerState
    {
        public WalkFState(InputController player) : base(player)
        {
        }
        public override void Update()
        {
            player.AnimatorPlay();
            //player.rigid.velocity = new Vector2(player.movePower, player.rigid.velocity.y);
            //if (player.rigid.velocity.sqrMagnitude < 0.01f)
            //{
            //    player.ChangeState(State.Idle);
            //}

        }
    }
    private class WalkBState : PlayerState
    {
        public WalkBState(InputController player) : base(player)
        {
        }
        public override void Update()
        {
            player.AnimatorPlay();
        }
    }
    private class WalkLState : PlayerState
    {
        public WalkLState(InputController player) : base(player)
        {
        }
        public override void Update()
        {
            player.AnimatorPlay();
        }
    }
    private class WalkRState : PlayerState
    {
        public WalkRState(InputController player) : base(player)
        {
        }
        public override void Update()
        {
            player.AnimatorPlay();
        }
    }
    #endregion

    #region RunSTATE
    private class RunFState : PlayerState
    {
        public RunFState(InputController player) : base(player)
        {
        }
        public override void Update()
        {
            player.AnimatorPlay();
        }
    }
    private class RunBState : PlayerState
    {
        public RunBState(InputController player) : base(player)
        {
        }
        public override void Update()
        {
            player.AnimatorPlay();
        }
    }
    private class RunLState : PlayerState
    {
        public RunLState(InputController player) : base(player)
        {
        }
        public override void Update()
        {
            player.AnimatorPlay();
        }
    }
    private class RunRState : PlayerState
    {
        public RunRState(InputController player) : base(player)
        {
        }
        public override void Update()
        {
            player.AnimatorPlay();
        }
    }
    #endregion


    #endregion


    #region Animation
    private void AnimatorPlay()
    {
        int checkAniHash;
        // Walk Animation
        if (rigid.velocity.x > 0.01f && rigid.velocity.x < 1.01f) 
        { checkAniHash = walkRHash; }
        else if (rigid.velocity.x < -0.01f && rigid.velocity.x > -1.01f)
        { checkAniHash = walkLHash; }
        else if (rigid.velocity.z > 0.05f && rigid.velocity.z < 1.05f)
        { checkAniHash = walkFHash; }
        else if (rigid.velocity.z < -0.05f && rigid.velocity.z > -1.01f)
        { checkAniHash = walkBHash; }
        // Idle Animation
        else if (rigid.velocity.sqrMagnitude < 0.05f)
        { checkAniHash = idleHash; }
        // Run Animation
        else if (rigid.velocity.x > 1.3f && isRunning)
        { checkAniHash = runRHash; }
        else if (rigid.velocity.x < -1.3f && isRunning)
        { checkAniHash = runLHash; }
        else if (rigid.velocity.z > 1.3f && isRunning)
        { checkAniHash = runFHash; }
        else if (rigid.velocity.z < -1.3f && isRunning)
        { checkAniHash = runBHash; }

        else
        {
            checkAniHash = idleHash;
        }
        if (curAniHash != checkAniHash)
        {
            curAniHash = checkAniHash;
            animator.Play(curAniHash);
        }
    }
    #endregion

    private void OnEnable()
    {
        playerMove = playerInput.Player.Move;
        playerMove.Enable();

        playerAttack = playerInput.Player.Fire;
        playerAttack.Enable();
        playerAttack.performed += Attack;

        playerRun = playerInput.Player.Run;
        playerRun.Enable();
       
    }
    private void OnDisable()
    {
        playerMove.Disable();
        playerAttack.Disable();
        playerRun.Disable();
    }



    private void Awake()
    {
        playerInput = new PlayerInput();
        // Idle Hash
        state[(int)State.Idle] = new IdleState(this);
        // Walk Hash
        state[(int)State.WalkF] = new WalkFState(this);
        state[(int)State.WalkB] = new WalkBState(this);
        state[(int)State.WalkL] = new WalkLState(this);
        state[(int)State.WalkR] = new WalkRState(this);
        // Run Hash
        state[(int)State.RunF] = new RunFState(this);
        state[(int)State.RunB] = new RunBState(this);
        state[(int)State.RunL] = new RunLState(this);
        state[(int)State.RunR] = new RunRState(this);
    }

    private void Update()
    {
        // Move
        //movement = input.actions["Move"].ReadValue<Vector2>();
        movement = playerMove.ReadValue<Vector2>();
        //currentSpeed = movePower;
        if (isRunning)
        {
            currentSpeed = sprintSpeed;
        }
        else
        {
            currentSpeed = movePower;
            isRunning = false;
        }
            
        direction = new Vector3(movement.x, 0, movement.y);
        rigid.velocity = direction * currentSpeed + Vector3.up * rigid.velocity.y;
        //currentSpeed = movement.magnitude;

        if(movement == Vector2.zero)
        {
            currentSpeed = 0;
        }

        
    }
    private void FixedUpdate()
    {
        state[(int)currentState].Update();
    }
    public void Run()
    {
        isRunning = true;
        if (playerRun == playerInput.Player.Run )
        {
            playerRun = playerInput.Player.Run;
            playerRun.Enable();
            currentSpeed = sprintSpeed;
            Debug.Log($"{rigid.velocity} Run Test {currentSpeed}");
            Debug.Log($"{isRunning}");
        }


    }
    public void Walk()
    {
        isRunning = false;
        Debug.Log($"{rigid.velocity} WALK {currentSpeed}");
        currentSpeed = movePower;
        movement = playerMove.ReadValue<Vector2>();
        direction = new Vector3(movement.x, 0, movement.y);
        rigid.velocity = direction * currentSpeed + Vector3.up * rigid.velocity.y;
    }
    public void Roll()
    {
        //movement = playerInput.actions["Move"].ReadValue<Vector2>();
        movement = playerMove.ReadValue<Vector2>();
        direction = new Vector3(movement.x, 0, movement.y);
        rigid.AddForce(direction * jumpPower, ForceMode.Impulse);
        Debug.Log("±¸¸£±â");
    }
    public void Attack(InputAction.CallbackContext context)
    {
        Debug.Log("Attack Test");
    }
}
