using UnityEngine;

public class PlayerIdleState : GroundedState
{
    public PlayerIdleState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {

    }
    
    public override void Enter()
    {
        base.Enter();
        player.SetVelocityX(0f);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Mathf.Abs(input.x) > 0.1f)
        {
            if (isPushing == false)
                playerStateMachine.ChangeState(player.MoveState);
            else
                playerStateMachine.ChangeState(player.PushState);
        }
    }
}
