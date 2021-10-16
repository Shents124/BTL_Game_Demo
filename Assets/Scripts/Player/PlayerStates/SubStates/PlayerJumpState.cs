using UnityEngine;
public class PlayerJumpState : PlayerAbilityState
{
    private int amountOfJumpsLeft;
    public PlayerJumpState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {
        amountOfJumpsLeft = playerData.amountOfJumps;
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocityY(playerData.jumpVelocity);
        isAbilityDone = true;
        amountOfJumpsLeft--;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public bool CanJump()
    {
        if (amountOfJumpsLeft > 0)
            return true;

        return false;
    }

    public void ResetAmountOfJumpLeft() => amountOfJumpsLeft = playerData.amountOfJumps;

    public void DecreaseAmoutOfJumpLeft() => amountOfJumpsLeft--;
}
