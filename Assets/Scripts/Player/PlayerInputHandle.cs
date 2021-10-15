using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInputHandle : MonoBehaviour
{
    private Vector2 move;
    private bool jump;
    private bool dash;
    private bool attack;

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveInput(context.ReadValue<Vector2>());
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        JumpInput(context.action.triggered);    
    }
    public void OnDash(InputAction.CallbackContext context)
    {
        DashInput(context.action.triggered);
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        AttackInput(context.action.triggered);
    }

    public void MoveInput(Vector2 newMoveDirection)
    {
        move = newMoveDirection;
        //Debug.Log(move);
    }

    public void JumpInput(bool newJumpState)
    {
        jump = newJumpState;
        // Debug.Log(jump);
    }

    public void DashInput(bool newDashState)
    {
        dash = newDashState;
    }

    public void AttackInput(bool newAttackState)
    {
        attack = newAttackState;
    }

    public Vector2 GetMove()
    {
        return move;
    }

    public bool IsJumping()
    {
        return jump;
    }

    public bool IsDashing()
    {
        return dash;
    }
    public bool IsAttacking()
    {
        return attack;
    }
}
