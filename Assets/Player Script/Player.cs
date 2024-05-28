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
    private bool _isFacingRight = true;
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
        Move();
        Attack();
        Jump();
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
        }
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }
}
