using UnityEngine;

public class Powerup : MonoBehaviour
{
    [Range(-1f, -10f)] [SerializeField] private float _dropSpeed = -1.5f;
    [SerializeField] private Transform _collectVfx;

    private void Update() => HandleMovement();

    private void HandleMovement()
    {
        Vector2 currPosition = transform.localPosition;
        currPosition.y += _dropSpeed * Time.deltaTime;
        transform.localPosition = currPosition;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Spaceship spaceship))
        {
            OnCollectPowerup(spaceship);
            Destroy(gameObject);
        }
    }

    protected virtual void OnCollectPowerup(Spaceship spaceship)
    {
        if (_collectVfx != null)
            Instantiate(_collectVfx, transform.localPosition, _collectVfx.rotation);
    }
}