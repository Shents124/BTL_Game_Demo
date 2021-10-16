using UnityEngine;

public class PlayerDashState : PlayerState
{
    private Vector2 lastAfterImagePos;
    public PlayerDashState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        player.SetGravity(0);
        PlaceAfterimage();
    }

    public override void Exit()
    {
        base.Exit();
        player.SetGravity(5);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        CheckIfShouldPlaceAfterImage();

        if (Time.time >= playerData.dashCoolDown + startTime)
        {
            if (player.CheckIfGround())
                playerStateMachine.ChangeState(player.IdleState);
            else
                playerStateMachine.ChangeState(player.AirState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

         player.SetVelocityX(playerData.dashVelocity * player.FacingDirection * Time.deltaTime);
         player.SetVelocityY(0);     
    }

    private void CheckIfShouldPlaceAfterImage()
    {
        if(Vector2.Distance(player.transform.position, lastAfterImagePos) >= playerData.distanceBetweenImages)
        {
            PlaceAfterimage();
        }
    }

    private void PlaceAfterimage()
    {
        PlayerAfterImagePool.Instance.GetFromPool();
        lastAfterImagePos = player.transform.position;
    }
}
