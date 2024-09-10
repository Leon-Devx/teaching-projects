using System;
using Lean.Pool;
using UnityEngine;

public class Projectile : MonoBehaviour, IPlayer
{
    public static event Action<Projectile> OnAnyProjectileSpawn;
    public static event Action<Projectile> OnAnyProjectileDeSpawn;

    [SerializeField] private float _speed = 15f;

    private bool _isSpawned;

    private void OnEnable()
    {
        _isSpawned = true;
        OnAnyProjectileSpawn?.Invoke(this);
    }
    
    private void OnDisable()
    {
        _isSpawned = false;
        OnAnyProjectileDeSpawn?.Invoke(this);
    }

    public void HandleMovement()
    {
        Vector3 newPosition = transform.localPosition;
        float speed = _speed * Time.deltaTime;
        newPosition += transform.up * speed; 
        transform.localPosition = newPosition;
    }

    public void Despawn()
    {
        if (!_isSpawned) return;
        LeanPool.Despawn(gameObject);
    }
}
