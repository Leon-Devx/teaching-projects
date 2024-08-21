using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _rotateSpeed = 2.5f;
    [SerializeField] private bool _canMoveDiagonally = true;

    private Rigidbody2D _rigidbody2D;
    private Vector2 _movement;
    private bool _isMovingHorizontally = true;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void Update()
    {
        float verticalInput = Input.GetAxisRaw("Vertical");
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        if (_canMoveDiagonally)
        {
            _movement = new Vector2(horizontalInput, verticalInput);
        }
        else
        {
            if (horizontalInput != 0)
                _isMovingHorizontally = true;
            else if (verticalInput != 0)
                _isMovingHorizontally = false;

            if (_isMovingHorizontally)
            {
                _movement = new Vector2(horizontalInput, 0f);
            }
            else
            {
                _movement = new Vector2(0f, verticalInput);
            }
        }
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        _rigidbody2D.velocity = _movement * _speed;
    }

    private void HandleRotation()
    {
        if (_movement == Vector2.zero) return;
        
        Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, _movement.normalized);
        transform.localRotation =
            Quaternion.RotateTowards(transform.localRotation, toRotation, _rotateSpeed * Time.fixedDeltaTime);
    }
}