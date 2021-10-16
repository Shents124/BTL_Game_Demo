using UnityEngine;
public class PlayerPushState : GroundedState
{

    public PlayerPushState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isPushing == false || Mathf.Abs(input.x) <= 0.01f)
        {
            playerStateMachine.ChangeState(player.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        player.SetVelocityX(playerData.pushVelocity * input.normalized.x * Time.deltaTime);
    }
}
