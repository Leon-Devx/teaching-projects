using UnityEngine;
using System;
using Lean.Pool;

public abstract class Enemy : MonoBehaviour, IDamageDealer
{
    public event Action OnDestroyed;
    public event Action OnDamaged;

    [SerializeField] private int _health = 1;

    [SerializeField] private int _score;

    private int _totalHealth;

    #region Properties

    protected int Health
    {
        get => _health;
        set => _health = value;
    }

    #endregion

    protected virtual void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        _totalHealth = Health;
    }

    protected virtual void OnEnable()
    {
        Health = _totalHealth;
    }

    protected virtual void Update()
    {
        HandleMovement();
    }

    protected abstract void HandleMovement();

    protected virtual void OnHealthReduced()
    {
        OnDamaged?.Invoke();

        if (Health <= 0)
        {
            DestroyActionPerformed();
            LeanPool.Despawn(gameObject);
        }
    }

    protected virtual void DestroyActionPerformed()
    {
        OnDestroyed?.Invoke();
        if (ScoreCounter.Instance != null) ScoreCounter.Instance.AddScore(_score);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out IPlayer player))
        {
            Health--;

            if (player is Projectile projectile)
                projectile.gameObject.SetActive(false);
            else if (player is Spaceship)
                Health = 0;

            OnHealthReduced();
        }
    }
}