using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZigZagEnemy : Enemy
{
    [SerializeField] [Range(0, 20f)] private float _verticalSpeed = 3f;
    [SerializeField] [Range(0, 20f)] private float _horizontalSpeed = 3f;
    [SerializeField] private float _width = 0.5f;
    [SerializeField] private bool _moveUp;

    private Vector3 _screenBounds;
    private HorizontalMoveDirection _horizontalDirection = HorizontalMoveDirection.None;

    private void Start()
    {
        base.Awake();
        _screenBounds =
            Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        if (!_moveUp) _verticalSpeed *= -1f;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _horizontalDirection =
            (HorizontalMoveDirection)Random.Range(1, Enum.GetValues(typeof(HorizontalMoveDirection)).Length);
    }

    protected override void HandleMovement()
    {
        HandleVerticalMovement();
        HandleHorizontalMovement();
    }

    private void HandleVerticalMovement()
    {
        Vector2 currPosition = transform.localPosition;
        currPosition.y += _verticalSpeed * Time.deltaTime;
        SetPosition(currPosition);
    }

    private void HandleHorizontalMovement()
    {
        float direction = default;
        switch (_horizontalDirection)
        {
            case HorizontalMoveDirection.None:
                direction = 0f;
                break;
            case HorizontalMoveDirection.Left:
                direction = -1f;
                break;
            case HorizontalMoveDirection.Right:
                direction = 1f;
                break;
        }

        Vector2 currPosition = transform.localPosition;
        currPosition.x += _horizontalSpeed * Time.deltaTime * direction;
        SetPosition(currPosition);
        CheckForScreenBounds();
    }

    private void SetPosition(Vector3 newPosition) => transform.localPosition = newPosition;

    private void CheckForScreenBounds()
    {
        if (transform.localPosition.x >= _screenBounds.x - _width)
            _horizontalDirection = HorizontalMoveDirection.Left;
        else if (transform.localPosition.x <= -_screenBounds.x + _width)
            _horizontalDirection = HorizontalMoveDirection.Right;
    }
}