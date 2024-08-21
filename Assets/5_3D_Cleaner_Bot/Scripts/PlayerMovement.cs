using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _rotationSpeed = 25f;
    [SerializeField] private float _thrustForce = 15;

    private Rigidbody _rigidbody;

    private bool _canJump = true;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleRotation();
        HandleFlying();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Vector3 jumpDirection  = new Vector3(0f, _thrustForce, 0f);
            _rigidbody.AddForce(jumpDirection, ForceMode.VelocityChange);
        }
    }

    private void HandleMovement()
    {
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 newPosition = verticalInput * transform.forward * _speed * Time.fixedDeltaTime;
        _rigidbody.MovePosition(_rigidbody.position + newPosition);
    }

    private void HandleRotation()
    {
        float turnInput = Input.GetAxis("Horizontal");
        float turnAmount = turnInput * _rotationSpeed;
        Quaternion turnRotation = Quaternion.Euler(0f, turnAmount, 0f);
        _rigidbody.MoveRotation(_rigidbody.rotation * turnRotation);
    }

    private void HandleFlying()
    {
        if (Input.GetButton("Jump"))
        {
            Vector3 jumpDirection  = new Vector3(0f, _thrustForce, 0f);
            _rigidbody.AddForce(jumpDirection, ForceMode.Force);
        }
    }
}