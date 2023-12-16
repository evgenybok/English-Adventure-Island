using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float speed = 15f;
    [SerializeField] private float jumpingPower = 16f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private float horizontal;
    private bool isFacingRight = true;
    private bool isGrounded = true;
    private bool canJump = true;
    const float groundCheckRadius = 0.1f;

    // Reference to the Animator component
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>(); // Get the Animator component
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && isGrounded && canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            canJump = false;
        }


        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        Flip();

        // Set the "IsMoving" parameter in the Animator based on the movement
        bool isMoving = Mathf.Abs(horizontal) > 0f;
        animator.SetBool("IsMoving", isMoving);
    }

    private void FixedUpdate()
    {
        GroundCheck();
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    void GroundCheck()
    {
        bool wasGrounded = isGrounded;
        isGrounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius, groundLayer);
        if (colliders.Length > 0)
        {
            isGrounded = true;
            if (wasGrounded)
            {
                canJump = true;
            }
            else canJump = false;
        }
    }

    public float Horizontal
    {
        get { return horizontal; }
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
