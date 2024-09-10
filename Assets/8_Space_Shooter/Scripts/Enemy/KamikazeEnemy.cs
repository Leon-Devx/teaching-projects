using UnityEngine;

public class KamikazeEnemy : Enemy
{
    [SerializeField] private float _speed = 30f;
    [SerializeField] private bool _moveUp;
    
    private Rigidbody2D _rigidbody2D;

    protected override void Initialize()
    {
        base.Initialize();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (!_moveUp) _speed *= -1f;
    }

    protected override void HandleMovement()
    {
        Vector2 newPosition = _rigidbody2D.position;
        newPosition.y += _speed * Time.deltaTime;
        _rigidbody2D.MovePosition(newPosition);
    }
}
