using UnityEngine;

public class GroundedState : PlayerState
{
    protected Vector2 input;
    protected bool isPushing;
    private bool isJumping;
    private bool isGrounded;  
    private bool isAttacking;

    public GroundedState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = player.CheckIfGround();
        isPushing = player.CheckIfPush();
    }

    public override void Enter()
    {
        base.Enter();
        player.JumpState.ResetAmountOfJumpLeft();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        input = player.InputHandle.GetMove();
        isJumping = player.InputHandle.IsJumping();
        isAttacking = player.InputHandle.IsAttacking();

        if (isJumping && player.JumpState.CanJump())
        {
            playerStateMachine.ChangeState(player.JumpState);
        }
        else if (isAttacking == true && Mathf.Abs(input.x) <= 0.1f)
        {
            playerStateMachine.ChangeState(player.AttackState);
        }
        else if (isGrounded == false)
        {
            playerStateMachine.ChangeState(player.AirState);
        }
        else if(player.IsEscaspe == true)
        {
            playerStateMachine.ChangeState(player.Die_EscapeState);
        }
    }
}
