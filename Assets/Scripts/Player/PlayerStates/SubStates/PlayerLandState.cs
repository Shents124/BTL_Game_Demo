
public class PlayerLandState : GroundedState
{
    public PlayerLandState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (input.x != 0)
            playerStateMachine.ChangeState(player.MoveState);
        else if (isAnimationFinished)
            playerStateMachine.ChangeState(player.IdleState);
    }
}
