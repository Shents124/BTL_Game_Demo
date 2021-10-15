using UnityEngine;

public class Player : MonoBehaviour
{
    #region Animaion variables
    private const string Idle = "idle";
    private const string Move = "move";
    private const string InAir = "inAir";
    private const string Land = "land";
    private const string Attack = "attack";
    private const string Push = "push";
    private const string Die_Escape = "die_escape";
    #endregion

    #region State Variables
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerAirState AirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerAttackState AttackState { get; private set; }
    public PlayerPushState PushState { get; private set; }
    public PlayerDie_EscapeState Die_EscapeState { get; private set; }
    #endregion

    #region Components
    public Animator Anim { get; private set; }
    public Rigidbody2D PlayerRigid { get; private set; }
    public PlayerInputHandle InputHandle { get; private set; }
    #endregion

    public bool isPushing;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform pushCheck;

    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection { get; private set; }
    public bool IsEscaspe { get; private set; }
    [SerializeField] private PlayerData playerData;
    private Vector2 workspace;

    private void Awake()
    {
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, playerData, Idle);
        MoveState = new PlayerMoveState(this, StateMachine, playerData, Move);
        JumpState = new PlayerJumpState(this, StateMachine, playerData, InAir);
        AirState = new PlayerAirState(this, StateMachine, playerData, InAir);
        LandState = new PlayerLandState(this, StateMachine, playerData, Land);
        AttackState = new PlayerAttackState(this, StateMachine, playerData, Attack);
        PushState = new PlayerPushState(this, StateMachine, playerData, Push);
        Die_EscapeState = new PlayerDie_EscapeState(this, StateMachine, playerData, Die_Escape);
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandle = GetComponent<PlayerInputHandle>();
        PlayerRigid = GetComponent<Rigidbody2D>();
        StateMachine.Initialize(IdleState);
        FacingDirection = 1;
        IsEscaspe = false;
    }

    private void Update()
    {
        CurrentVelocity = PlayerRigid.velocity;
        StateMachine.CurrentState.LogicUpdate();
        isPushing = CheckIfPush();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    #region Set Velocity
    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        PlayerRigid.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocityY(float jumpVelocity)
    {
        workspace.Set(CurrentVelocity.x, jumpVelocity);
        PlayerRigid.velocity = workspace;
        CurrentVelocity = workspace;
    }
    #endregion

    #region Check Method
    public bool CheckIfGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
    }

    public bool CheckIfPush()
    {
        return Physics2D.OverlapCircle(pushCheck.position, playerData.groundCheckRadius, playerData.whatIsPush);
    }

    public void CheckIfShouldFlip(float xInput)
    {
        if (FacingDirection == 1 && xInput < 0 ||
            FacingDirection == -1 && xInput > 0)
            Flip();
    }

    #endregion

    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
    private void Flip()
    {
        FacingDirection *= -1;
        Vector3 _scale = transform.localScale;
        _scale.x *= -1;
        transform.localScale = _scale;
    }

    public void Escape() => IsEscaspe = true;
}
