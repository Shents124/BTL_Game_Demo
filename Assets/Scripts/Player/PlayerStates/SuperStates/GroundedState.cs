using UnityEngine;

public class GroundedState : PlayerState
{
    protected Vector2 input;
    protected bool isPushing;
    private bool isGrounded;  

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

        if (player.InputHandle.IsJumping() && player.JumpState.CanJump())
        {
            playerStateMachine.ChangeState(player.JumpState);
            player.InputHandle.SetJumpInputToFalse();
        }
        else if (player.InputHandle.IsAttacking() && Mathf.Abs(input.x) <= 0.1f)
        {
            playerStateMachine.ChangeState(player.AttackState);
            player.InputHandle.SetAttackInputToFalse();
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
