using UnityEngine;

public class Powerup : MonoBehaviour
{
    [Range(-1f, -10f)] [SerializeField] private float _dropSpeed = -2f;
    [SerializeField] private Transform _collectVfx;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Spaceship spaceship))
            OnCollectPowerup();
    }

    protected virtual void OnCollectPowerup()
    {
        if (_collectVfx != null)
            Instantiate(_collectVfx, transform.localPosition, _collectVfx.rotation);

        Destroy(gameObject);
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 currPosition = transform.localPosition;
        currPosition.y += _dropSpeed * Time.deltaTime;
        transform.localPosition = currPosition;
    }
}