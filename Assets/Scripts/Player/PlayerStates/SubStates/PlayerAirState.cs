using UnityEngine;

public class PlayerAirState : PlayerState
{
    private const string YVelocity = "yVelocity";
    private bool isGround;
    private Vector2 input;
    public PlayerAirState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGround = player.CheckIfGround();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        input = player.InputHandle.GetMove();

        if (isGround && player.CurrentVelocity.y < 0.01f)
        {
            playerStateMachine.ChangeState(player.LandState);
        }
        else if (player.InputHandle.IsJumping() && player.JumpState.CanJump())
        {
            playerStateMachine.ChangeState(player.JumpState);
            player.InputHandle.SetJumpInputToFalse();
        }
        else if (player.InputHandle.IsAttacking() && Mathf.Abs(input.x) <= 0.1f)
        {
            playerStateMachine.ChangeState(player.AttackState);
            player.InputHandle.SetAttackInputToFalse();
        }
        else
        {
            player.CheckIfShouldFlip(input.x);
            player.Anim.SetFloat(YVelocity, player.CurrentVelocity.y);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (isGround == false)
        {
            player.SetVelocityX(playerData.movementVelocity * input.normalized.x * Time.deltaTime);
        }
    }

}
