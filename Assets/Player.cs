using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private float xInput;

    [SerializeField] private float MoveSpeed;
    [SerializeField]private float JumpForce;

    private Animator Anim;

    private SpriteRenderer spriteRenderer;

    private int facingDirection = 1 ;
    private bool facingRight = true;

    private bool isGrounded;

    [Header("Collsion Info")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Anim = GetComponentInChildren<Animator>();  
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        CheckInput();

        CollisionChecks();

        Debug.Log(isGrounded);

        flipController();
        AnimatorControllers();
    }

    private void CollisionChecks()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
    }

    private void CheckInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");


        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Movement()
    {
        rb.velocity = new Vector2(xInput * MoveSpeed, rb.velocity.y);
    }

    private void Jump()
    {
        if(isGrounded) 
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
    }

    private void AnimatorControllers()
    {
        bool isMoving = rb.velocity.x != 0;
        
        Anim.SetBool("isMoving", isMoving);
        
    }

    private void Flip()
    {
        facingDirection = facingDirection * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);

    }

    private void flipController()
    {
        if(rb.velocity.x > 0 && !facingRight)
        {
            Flip();
        }

        else if(rb.velocity.x < 0 && facingRight)
        {
            Flip();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundCheckDistance));
    }
}
