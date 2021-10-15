using UnityEngine;
public class PlayerAttackState : PlayerAbilityState
{
    private Vector2 input;
    private bool isGround;

    public PlayerAttackState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGround = player.CheckIfGround();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        input = player.InputHandle.GetMove();

        if (isAnimationFinished)
        {
            if (input.x != 0)
                playerStateMachine.ChangeState(player.MoveState);
            else if (input.x == 0)
                playerStateMachine.ChangeState(player.IdleState);
            else if (isGround == false)
                playerStateMachine.ChangeState(player.AirState);
        }
    }
}
