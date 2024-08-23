using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private int _health = 1;
    [SerializeField] [Range(0, 20f)] private float _minSpeed = 3f;
    [SerializeField] [Range(0, 20f)] private float _maxSpeed = 10f;
    [SerializeField] private bool _moveUp;

    private float _speed;
    
    private void Start()
    {
        _speed = Random.Range(_minSpeed, _maxSpeed);
        if (!_moveUp) _speed *= -1f;
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 currPosition = transform.localPosition;
        currPosition.y += _speed * Time.deltaTime;
        transform.localPosition = currPosition;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Projectile projectile))
        {
            projectile.gameObject.SetActive(false);
            _health--;
            OnHealthReduced();
        }
    }

    private void OnHealthReduced()
    {
        if (_health <= 0)
            Destroy(gameObject);
    }
}