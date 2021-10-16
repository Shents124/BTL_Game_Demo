using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerStateMachine playerStateMachine;
    protected PlayerData playerData;
    protected bool isAnimationFinished;
    protected float startTime;

    private string animBoolName;

    public PlayerState()
    {

    }

    public PlayerState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName)
    {
        this.player = player;
        this.playerStateMachine = playerStateMachine;
        this.playerData = playerData;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        DoChecks();
        startTime = Time.time;
        player.Anim.SetBool(animBoolName, true);
        isAnimationFinished = false;
    }

    public virtual void Exit()
    {
        player.Anim.SetBool(animBoolName, false);
    }

    public virtual void LogicUpdate()
    {
        if (player.InputHandle.IsDashing())
        {
            playerStateMachine.ChangeState(player.DashState);
            player.InputHandle.SetDashInputToFalse();
        }
    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void DoChecks() { }

    public virtual void AnimationTrigger() { }

    public virtual void AnimationFinishTrigger() { isAnimationFinished = true; }
}
