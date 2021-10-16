using UnityEngine;

public class PlayerMoveState : GroundedState
{
    public PlayerMoveState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
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

        player.CheckIfShouldFlip(input.x);

        if (Mathf.Abs(input.x) <= 0.1f)
        {
            playerStateMachine.ChangeState(player.IdleState);
        }
        else if (isPushing == true)
        {
            playerStateMachine.ChangeState(player.PushState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        player.SetVelocityX(playerData.movementVelocity * input.normalized.x * Time.deltaTime);
    }
}
