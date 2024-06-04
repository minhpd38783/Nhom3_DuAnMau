using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float inputMove;
    private float moveSpeed = 8f;
    [SerializeField] private bool isFacingRight = true;
    private float jumpForce = 8f;
    [SerializeField] private bool isGrounded;
    int attackCount = 0;
    private float rollSpeed = 0.5f;
    float lastAttackTime;
    float lastRollTime;
    private bool onLadder;
    private float climbSpeed = 9f;
    

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump(); 
        }
        animator.SetBool("Jump", !isGrounded);

        if (Input.GetMouseButtonDown(0) && Time.time - lastAttackTime >= 0.3f)
        {
            Attack();
            lastAttackTime = Time.time;
        }

        Roll();

        GroundCheck();
    }
    private void Move()
    {
        if (onLadder)
        {
            rb.velocity = new Vector2(rb.velocity.x, Input.GetAxis( "Vertical") * climbSpeed);
        }
        else if (!onLadder)
        {
            inputMove = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(inputMove * moveSpeed, rb.velocity.y);

            if (isFacingRight == true && inputMove == -1)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                isFacingRight = false;
            }
            if (isFacingRight == false && inputMove == 1)
            {
                transform.localScale = new Vector3(1, 1, 1);
                isFacingRight = true;
            }
            animator.SetFloat("Running", Mathf.Abs(inputMove));
        }
    }
    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        if (isGrounded != true)
        {
            return;
        }
        animator.SetBool("Jump", false);
    }
    private void GroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }
    private void Attack()
    {
        animator.SetTrigger("Attack" + (attackCount % 2 + 1));
        attackCount++;
        if (Time.time - lastAttackTime >= 0.5f)
        {
            attackCount = 0;
        }
        lastAttackTime = Time.time;
    }
    private void Roll()
    {
        if (Input.GetMouseButtonDown(1) && Time.time - lastRollTime >= 0.8f)
        {
            float rollDirection = isFacingRight ? 1f : -1f;
            Vector3 rollVector = new Vector3(rollDirection * rollSpeed, 0f, 0f);
            transform.position += rollVector;
            animator.SetTrigger("Roll");
            lastRollTime = Time.time;
        }
    }
    private void OnTriggerEnter2D(Collider2D players)
    {
        if (players.CompareTag("Ladder"))
        {
            onLadder = true;
        }
    }
    private void OnTriggerExit2D(Collider2D players)
    {
        if (players.CompareTag("Ladder"))
        {
            onLadder = false;
        }
    }
}
