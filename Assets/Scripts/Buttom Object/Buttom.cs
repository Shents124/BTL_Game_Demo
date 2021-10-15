using UnityEngine;
using UnityEngine.Events;

public class Buttom : MonoBehaviour
{
    [SerializeField] private SpriteRenderer buttom;
    [SerializeField] private PolygonCollider2D buttomPolyonCollider;
    [SerializeField] private GameObject buttomPressed;

    public UnityEvent OnButtomPress;
    public UnityEvent OnButtomUnPress;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Stone"))
        {
            ButtomPress();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Stone"))
        {
            ButtomNotPress();
        }
    }
    private void ButtomPress()
    {
        buttom.enabled = false;
        buttomPolyonCollider.enabled = false;

        buttomPressed.SetActive(true);

        OnButtomPress?.Invoke();
        Debug.Log("Press");
    }

    private void ButtomNotPress()
    {
        buttom.enabled = true;
        buttomPolyonCollider.enabled = true;

        buttomPressed.SetActive(false);

        OnButtomUnPress?.Invoke();
    }
}
