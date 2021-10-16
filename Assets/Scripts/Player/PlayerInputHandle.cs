using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInputHandle : MonoBehaviour
{
    private Vector2 move;
    private bool jump;
    private bool dash;
    private bool attack;

    public void OnMovement(InputValue value)
    {
        MoveInput(value.Get<Vector2>());
    }
    public void OnJump(InputValue value)
    {
        JumpInput(value.isPressed);
    }
    public void OnDash(InputValue value)
    {
        DashInput(value.isPressed);
    }
    public void OnAttack(InputValue value)
    {
        AttackInput(value.isPressed);
    }

    public void MoveInput(Vector2 newMoveDirection) => move = newMoveDirection;

    public void JumpInput(bool newJumpState) => jump = newJumpState;

    public void DashInput(bool newDashState) => dash = newDashState;

    public void AttackInput(bool newAttackState) => attack = newAttackState;

    public Vector2 GetMove() => move;

    public bool IsJumping() => jump;

    public bool IsDashing() => dash;

    public bool IsAttacking() => attack;

    public void SetJumpInputToFalse() => jump = false;
    public void SetAttackInputToFalse() => attack = false;
    public void SetDashInputToFalse() => dash = false;
}
