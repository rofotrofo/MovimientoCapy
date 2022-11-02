using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private float horizontalMove;
    public float jumpForce;
    private bool isGrounded;
    public bool right = true;
    bool isJumping = false;
    Rigidbody2D rb;
    public LayerMask ground;
    private Animator animator;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalMove * speed, rb.velocity.y);
        animator.SetFloat("Horizontal", Mathf.Abs(horizontalMove));

        if (horizontalMove > 0 && !right)
        {
            Flip();
        }
        else if (horizontalMove < 0 && right)
        {
            Flip();
        }
        if (Input.GetButton("Jump") && !isJumping)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isJumping = true;
            isGrounded = false;
        }
    }

    private void FixedUpdate()
    {
        animator.SetBool("isGrounded", isGrounded);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            isGrounded = true;
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }

    private void Flip()
    {
        right = !right;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
    }
}
