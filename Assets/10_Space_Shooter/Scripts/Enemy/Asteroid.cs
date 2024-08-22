using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private int _health = 1;

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