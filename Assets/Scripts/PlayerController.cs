using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 300f;
    public float jumpForce;
    public Transform groundCheckPosition;
    public Transform leftCheckWall;
    public Transform rightCheckWall;
    public float radius = 0.2f;
    public LayerMask groundLayer;
    public float slideSpeed = 20f;
    private float gravity = 1f;
    private float fallMutiplier = 5f;

    public float dashTime;
    public float dashSpeed;
    public float distanceBetweenImages;
    public float dashCoolDown;
    private float dashTimeLeft;
    private float lastImagesXpos;
    private float lastDash;
    private int facingDirection = 1;

    private bool isRunning;
    private bool isGround;
    private bool onWall;
    private bool isJump = false;
    private bool isDashing;

    private float horizontal;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D playerRid;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerRid = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");

        Flip();

        if (Mathf.Abs(horizontal) > 0)
        {
            isRunning = true;
        }

        else
            isRunning = false;

        if (Input.GetKeyDown(KeyCode.F))
            animator.SetTrigger("Attack");

        Jump();

        UpdateAnimation();
        SlideWall();
        CheckDash();

        if (Input.GetKeyDown(KeyCode.G))
        {
            if (Time.time >= (lastDash + dashCoolDown))
                AttemptToDash();
        }
        
    }

    private void AttemptToDash()
    {
        isDashing = true;
        dashTimeLeft = dashTime;
        lastDash = Time.time;

        PlayerAfterImagePool.Instance.GetFromPool();
        lastImagesXpos = transform.position.x;
    }

    private void CheckDash()
    {
        if (isDashing)
        {
            if (dashTimeLeft > 0)
            {
                playerRid.velocity = new Vector2(dashSpeed * facingDirection, playerRid.velocity.y);
                dashTimeLeft -= Time.deltaTime;

                if(Mathf.Abs(transform.position.x - lastImagesXpos) > distanceBetweenImages)
                {
                    PlayerAfterImagePool.Instance.GetFromPool();
                    lastImagesXpos = transform.position.x;
                }
            }

            if(dashTimeLeft <= 0 || onWall)
            {
                isDashing = false;
            }
        }
    }

    private void FixedUpdate()
    {
        ModifyPhysics();
        GroundCheck();
        Move();
        CheckWall();
    }

    private void GroundCheck()
    {
        isGround = Physics2D.OverlapCircle(groundCheckPosition.position, radius, groundLayer);
        isJump = !isGround;
    }

    private void CheckWall()
    {
        onWall = Physics2D.OverlapCircle(leftCheckWall.position, radius, groundLayer)
                || Physics2D.OverlapCircle(rightCheckWall.position, radius, groundLayer);
    }

    private void UpdateAnimation()
    {
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isJump", isJump);
        animator.SetFloat("yVelocity", playerRid.velocity.y);
    }

    private void SlideWall()
    {
        if (onWall && !isGround)
            playerRid.velocity = new Vector2(playerRid.velocity.x, -slideSpeed * Time.deltaTime);
    }

    private void Move()
    {
        if (Mathf.Abs(horizontal) > 0)
        {
            playerRid.velocity = new Vector2(horizontal * Time.deltaTime * speed, playerRid.velocity.y);
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround == true)
        {
            playerRid.velocity = Vector2.up * jumpForce;
            animator.SetBool("isJump", true);
        }
    }



    void ModifyPhysics()
    {
        if (playerRid.velocity.y < 0)
            playerRid.gravityScale = gravity * fallMutiplier;
        else if (playerRid.velocity.y > 0 && !Input.GetKeyDown(KeyCode.UpArrow))
            playerRid.gravityScale = gravity * (fallMutiplier / 2);
        else if (playerRid.velocity.y == 0)
            playerRid.gravityScale = 1f;
    }

    private void Flip()
    {
        if (horizontal < 0)
        {
            spriteRenderer.flipX = true;
            facingDirection = -1;
        }          
        else if (horizontal > 0)
        {
            spriteRenderer.flipX = false;
            facingDirection = 1;
        }
            
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheckPosition.position, radius);
    }
}
