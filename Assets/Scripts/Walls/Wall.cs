using UnityEngine;

public class Wall : MonoBehaviour
{
    private Animator animator;
    private BoxCollider2D boxCollider2D;
    private void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
    public void OnpenWall()
    {
        animator.SetTrigger("wallOpen");
        boxCollider2D.enabled = false;
        
    }
    public void CloseWall()
    {
        animator.SetTrigger("wallClose");
        boxCollider2D.enabled = true;
    }
}
