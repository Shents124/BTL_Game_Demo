using UnityEngine;

public class DoorEscape : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().Escape();         
        }
    }
}
