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
            StartCoroutine(StopJumpAnim());
        }
        GroundCheck();
    }
    private void Move()
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
    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        animator.SetTrigger("Jump");
    }
    IEnumerator StopJumpAnim()
    {
        yield return new WaitForSeconds(2.5f);
        //animator.SetBool("Fall", true);
        //if (isGrounded)
        //{
        //    animator.SetBool("Fall", false);
        //}
    }
    private void GroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }
}
