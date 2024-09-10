using Lean.Pool;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour, IDamageDealer
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private bool _moveUp;

    private void Awake() => _speed = _moveUp ? _speed : -_speed;
    
    private void Update()
    {
        Vector3 newPosition = transform.localPosition;
        float speed = _speed * Time.deltaTime;
        newPosition += transform.up * speed;
        transform.localPosition = newPosition;
    }

    public void Despawn()
    {
        if (!gameObject.activeSelf) return;
        LeanPool.Despawn(gameObject);
    }
}
