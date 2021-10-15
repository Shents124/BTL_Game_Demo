using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float movementVelocity = 10f;

    [Header("Jump State")]
    public float jumpVelocity = 15f;
    public int amountOfJumps = 2;

    [Header("Attack State")]
    public int dame = 10;

    [Header("Push State")]
    public LayerMask whatIsPush;
    public float pushVelocity = 5f;

    [Header("Check Veriables")]
    public float groundCheckRadius = 0.2f;
    public LayerMask whatIsGround;
}
