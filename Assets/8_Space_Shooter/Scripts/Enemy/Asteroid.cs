using UnityEngine;
using Random = UnityEngine.Random;

public class Asteroid : Enemy
{
    [SerializeField] [Range(0, 20f)] private float _minSpeed = 3f;
    [SerializeField] [Range(0, 20f)] private float _maxSpeed = 10f;
    [SerializeField] private bool _moveUp;

    private float _speed;

    private void Start()
    {
        _speed = Random.Range(_minSpeed, _maxSpeed);
        if (!_moveUp) _speed *= -1f;
    }

    protected override void HandleMovement()
    {
        Vector2 currPosition = transform.localPosition;
        currPosition.y += _speed * Time.deltaTime;
        transform.localPosition = currPosition;
    }
}