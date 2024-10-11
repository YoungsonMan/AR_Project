using PolyPerfect;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [Header("PlayerInput")]
    [SerializeField] PlayerInput input;
    [SerializeField] Rigidbody rigid;
    [SerializeField] public float movePower;
    [SerializeField] public float jumpPower;
    public float currentSpeed;
    public bool rolling;

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
                        RollF, RollB, RollL, RollR, Attack1, Attack2, Attack3, Size}
    [SerializeField] State currentState;
    private BaseState[] state = new BaseState[(int)State.Size];




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
    private void AnimatorPlay()
    {
        int checkAniHash;
        if (rigid.velocity.x > 0.01f)
        {
            checkAniHash = walkRHash;
        }
        else if (rigid.velocity.x < -0.01f)
        {
            checkAniHash = walkLHash;
        }
        else if (rigid.velocity.z < 0.05f)
        {
            checkAniHash = walkFHash;
        }
        else if (rigid.velocity.z < -0.05f)
        {
            checkAniHash = walkBHash;
        }
        else if (rigid.velocity.sqrMagnitude < 0.05f)
        {
            checkAniHash = idleHash;
        }
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

    private void Awake()
    {
        state[(int)State.Idle] = new IdleState(this);
        state[(int)State.WalkF] = new WalkFState(this);
        state[(int)State.WalkB] = new WalkBState(this);
        state[(int)State.WalkL] = new WalkLState(this);
        state[(int)State.WalkR] = new WalkRState(this);
    }

    private void Update()
    {
        // Move
        movement = input.actions["Move"].ReadValue<Vector2>();
        direction = new Vector3(movement.x, 0, movement.y);
        rigid.velocity = direction * movePower + Vector3.up * rigid.velocity.y;
        currentSpeed = movement.magnitude;

        

        bool roll = input.actions["Roll"].WasPressedThisFrame();
        if (roll)
        {
            Debug.Log($"±¸¸£±â! {currentSpeed}");
            rigid.AddForce(direction * jumpPower , ForceMode.Impulse);
            rolling = roll;
        }
        else
        {
            rolling = false;
        }

        Debug.Log($"{rigid.velocity} mag = {currentSpeed}");
    }
    private void FixedUpdate()
    {
        state[(int)currentState].Update();
    }

    public void Roll()
    {
        Vector2 move = input.actions["Move"].ReadValue<Vector2>();
        Vector3 dir = new Vector3(move.x, 0, move.y);
        rigid.AddForce(dir * jumpPower, ForceMode.Impulse);
    }
}
