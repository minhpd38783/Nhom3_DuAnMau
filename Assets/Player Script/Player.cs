using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Player : MonoBehaviour
{
    private Collider2D _col;
    private Rigidbody2D _rb;
    private Animator _animator;

    private float _inputMove;
    private float _moveSpeed = 6f;
    private int _jumpForce = 5;
    private int _jumpCount;
    private const int _maxJumps = 2;
    private bool _isGrounded = true;
    private bool _isFacingRight = true;
    private float _dashForce = 10f;
    private bool _isDashing = false;
    private bool _isOnDashTime;
    private float _arrowVelocity = 40f;

    [SerializeField] GameObject dashEff;
    [SerializeField] GameObject gunPos;
    [SerializeField] GameObject arrowBullet;
    // Start is called before the first frame update
    void Start()
    {
        _col = GetComponent<Collider2D>();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isDashing)
        {
            Move();
        }

        Attack();

        if (_isGrounded)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && !_isOnDashTime)
        {
            Dash();
            _isOnDashTime = true;
            Invoke(nameof(ResetDash), 0.65f);
        }
    }
    private void ResetDash()
    {
        _isOnDashTime = false;
    }
    private void Move()
    {
        _inputMove = Input.GetAxisRaw("Horizontal");

        _rb.velocity = new Vector2(_inputMove * _moveSpeed, _rb.velocity.y);

        if (_isFacingRight == true && _inputMove == -1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            _isFacingRight = false;
        }
        if (_isFacingRight == false && _inputMove == 1)
        {
            transform.localScale = new Vector3(1, 1, 1);
            _isFacingRight = true;
        }
        _animator.SetFloat("Running", MathF.Abs(_inputMove));
    }
    private void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _animator.SetTrigger("Attack");
            StartCoroutine(WaitToAttack());
        }
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded && _jumpCount < _maxJumps)
        {
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            _jumpCount++;
        }
    }
    private void OnCollisionEnter2D(Collision2D player)
    {
        if (player.gameObject.CompareTag("Ground"))
        {

            _isGrounded = true;
            _jumpCount = 0;
        }
        else
        {
            _isGrounded = false;
            _jumpCount = 0;
        }
    }
    private void Dash()
    {
        float dashDirection = _isFacingRight ? 1f : -1f;
        _rb.velocity = new Vector2(dashDirection * _dashForce, _rb.velocity.y);
        dashEff.SetActive(true);
        _animator.SetTrigger("Dash");
        _isDashing = true;
        StartCoroutine(StopDash());
    }
    IEnumerator StopDash()
    {
        yield return new WaitForSeconds(0.4f);
        _rb.velocity = new Vector2(0, _rb.velocity.y);
        dashEff.SetActive(false);
        _isDashing = false;
    }
    IEnumerator WaitToAttack()
    {
        yield return new WaitForSeconds(0.3f);
        GameObject arrow = Instantiate(arrowBullet, gunPos.transform.position, Quaternion.identity);
        Rigidbody2D arrowRb = arrow.GetComponent<Rigidbody2D>();
        _rb.velocity = new Vector2(_arrowVelocity, _rb.velocity.y);
        arrowRb.velocity = new Vector2(_isFacingRight ? _arrowVelocity : -_arrowVelocity, _rb.velocity.y);
        Destroy(arrow, 0.5f);
    }
}
