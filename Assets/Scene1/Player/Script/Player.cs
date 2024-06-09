using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField] GameObject activeBoss;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private Health health;
    private int maxHealth = 100;
    private int currentHealth;

    [SerializeField] private TextMeshProUGUI scoreCollected;
    private static int _score = 0;

    [SerializeField] GameObject resultGameObject;
    [SerializeField] TextMeshProUGUI resultScore;
    [SerializeField] TextMeshProUGUI resultTime;

    private float startTime;
    private float endTime;

    [SerializeField] AudioSource slashSource;
    [SerializeField] AudioClip slashClip;
    [SerializeField] AudioSource painSource;
    [SerializeField] AudioClip painClip;
    [SerializeField] AudioSource coinSource;
    [SerializeField] AudioClip coinClip;
    [SerializeField] AudioSource swordHitSource;
    [SerializeField] AudioClip swordHitClip;
    [SerializeField] AudioSource scene1Source;
    [SerializeField] AudioSource victorySource;
    [SerializeField] AudioClip victoryClip;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius = 0.8f;
    [SerializeField] private LayerMask enemyLayer;

    private Rigidbody2D rb;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentHealth = maxHealth;
        health.SetMaxHealth(maxHealth);

        scoreCollected.text = _score.ToString();

        slashSource.clip = slashClip;
        painSource.clip = painClip;
        coinSource.clip = coinClip;
        swordHitSource.clip = swordHitClip;
        victorySource.clip = victoryClip;

        startTime = Time.time; //Lưu thời gian bắt đầu
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
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
            rb.velocity = new Vector2(rb.velocity.x, Input.GetAxis("Vertical") * climbSpeed);
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
        slashSource.PlayOneShot(slashSource.clip);
        if (Time.time - lastAttackTime >= 0.5f)
        {
            attackCount = 0;
        }
        lastAttackTime = Time.time;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<BossHealth>().TakeDamage(50);
            swordHitSource.PlayOneShot(swordHitSource.clip);
        }
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
    private void OnCollisionEnter2D(Collision2D player)
    {
        if (player.gameObject.CompareTag("Trap"))
        {
            TakeDamage(10);
            painSource.PlayOneShot(painSource.clip);
        }
    }
    private void OnTriggerEnter2D(Collider2D players)
    {
        if (players.CompareTag("Coin"))
        {
            _score += 10;
            scoreCollected.text = _score.ToString();
            coinSource.PlayOneShot(coinSource.clip);
        }

        if (players.CompareTag("ActiveBoss"))
        {
            activeBoss.SetActive(true);
        }

        if (players.tag == "End")
        {
            scene1Source.Pause();
            resultGameObject.SetActive(true);
            victorySource.PlayOneShot(victorySource.clip);
            endTime = Time.time; // Lưu thời gian kết thúc
            PrintSummarry();
            Time.timeScale = 0f;
        }
    }
    private void PrintSummarry()
    {
        float timePlayed = endTime - startTime;
        resultScore.text = $"Score: {_score}";
        resultTime.text = $"Time: {timePlayed:F1} seconds";
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        health.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            gameOver.SetActive(true);
            Time.timeScale = 0f;
            Debug.Log("Game Over");
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
