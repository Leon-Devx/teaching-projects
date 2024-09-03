using UnityEngine;

public class KamikazeEnemy : Enemy
{
    [SerializeField] private float _speed = 30f;
    [SerializeField] private bool _moveUp;

    private void Start()
    {
        if (!_moveUp) _speed *= -1f;
    }

    protected override void HandleMovement()
    {
        Vector2 currPosition = transform.localPosition;
        currPosition.y += _speed * Time.deltaTime;
        transform.localPosition = currPosition;
    }
}
