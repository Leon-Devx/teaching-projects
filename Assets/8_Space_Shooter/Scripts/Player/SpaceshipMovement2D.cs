using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpaceshipMovement2D : MonoBehaviour
{
    [SerializeField] private bool _moveWithMouse = false;
    [SerializeField] private float _speed = 2f;
    
    private SpriteRenderer _spriteRenderer;

    private float _spaceshipWidth;
    private float _spaceshipHeight;

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _spaceshipWidth = _spriteRenderer.bounds.size.x / 2f;
        _spaceshipHeight = _spriteRenderer.bounds.size.y / 2f;
    }

    private void Update()
    {
        if (_moveWithMouse)
            HandleMobileMovement();
        else
            HandleMovement();
    }

    private void HandleMobileMovement()
    {
        if (float.IsPositiveInfinity(Input.mousePosition.x)) return;
        if (float.IsPositiveInfinity(Input.mousePosition.y)) return;
        
        Vector3 currentPosition = transform.localPosition;
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (EventSystem.current.IsPointerOverGameObject())
            newPosition = transform.localPosition;
        newPosition.z = currentPosition.z;
        float movementSpeed = _speed * Time.deltaTime;
        currentPosition = Vector3.MoveTowards(currentPosition, newPosition, movementSpeed);
        transform.localPosition = currentPosition;
        ClampPlayerPosition();
    }

    private void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        float newXPosition = horizontalInput * _speed * Time.deltaTime;
        float newYPosition = verticalInput * _speed * Time.deltaTime;

        Vector3 newPosition = transform.localPosition;
        newPosition.x += newXPosition;
        newPosition.y += newYPosition;

        transform.localPosition = newPosition;

        ClampPlayerPosition();
    }

    private void ClampPlayerPosition()
    {
        var screenBounds =
            Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        float clampedXValue =
            Mathf.Clamp(transform.localPosition.x, -screenBounds.x + _spaceshipWidth,
                screenBounds.x - _spaceshipWidth);
        float clampedYValue =
            Mathf.Clamp(transform.localPosition.y, -screenBounds.y + _spaceshipHeight * 7,
                screenBounds.y - _spaceshipHeight);
        Vector3 clampedPosition = new Vector3(clampedXValue, clampedYValue, transform.localPosition.z);
        transform.localPosition = clampedPosition;
    }
}