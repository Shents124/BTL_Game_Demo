using UnityEngine;

public class UICanvasControllerInput : MonoBehaviour
{
    [SerializeField] private PlayerInputHandle playerInputHandle;

    public void VirtualMoveInput(Vector2 virtualMoveInput)
    {
        playerInputHandle.MoveInput(virtualMoveInput);
    }

    public void VirtualJumpInput(bool virtualJumpState)
    {
        playerInputHandle.JumpInput(virtualJumpState);
    }

    public void VirtualDashInput(bool virtualDashState)
    {
        playerInputHandle.DashInput(virtualDashState);
    }

    public void VirtualAttackInput(bool virtualAttackState)
    {
        playerInputHandle.AttackInput(virtualAttackState);
    }
}
