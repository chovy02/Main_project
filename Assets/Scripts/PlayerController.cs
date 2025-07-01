using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 7f;
    private bool isGrounded;
    private bool hasJumpedInAir;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private Animator animator;

    private Rigidbody2D rb;

    bool doubleJump = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        UpdateAnimation();
    }

    private void HandleMovement()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveSpeed * moveInput, rb.linearVelocity.y);

        if (moveInput < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (moveInput > 0)
            transform.localScale = new Vector3(1, 1, 1);
    }

    private void HandleJump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if (isGrounded)
        {
            hasJumpedInAir = false;
            doubleJump = false;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            }
            else if (!isGrounded && !hasJumpedInAir)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                hasJumpedInAir = true;
                doubleJump = true;
            }
        }
    }

    private void UpdateAnimation()
    {
        bool isRunning = Mathf.Abs(rb.linearVelocity.x) > 0.1f;
        bool isJumping = !isGrounded;
        bool isDoubleJumping = !isGrounded && doubleJump;
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isJumping", isJumping);
        animator.SetBool("isDoubleJumping", isDoubleJumping);
    }
}
