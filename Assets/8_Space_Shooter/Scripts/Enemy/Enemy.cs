using UnityEngine;
using System;
using System.Collections;
using Lean.Pool;

public abstract class Enemy : MonoBehaviour, IDamageDealer
{
    public event Action<Enemy> OnDestroyed;
    public event Action OnDamaged;

    [SerializeField] private int _health = 1;

    [SerializeField] private int _score;

    private Coroutine _coroutine;

    private int _totalHealth;
    private bool _isDestroyedByCollisionWithPlayerSpaceship;
    private bool _registerCollision = true;

    #region Properties

    protected int Health
    {
        get => _health;
        set => _health = value;
    }

    public bool IsDestroyedByCollisionWithPlayerSpaceship => _isDestroyedByCollisionWithPlayerSpaceship;

    #endregion

    protected virtual void Awake()
    {
        Initialize();
    }

    protected virtual void Initialize()
    {
        _totalHealth = Health;
    }

    protected virtual void OnEnable()
    {
        Health = _totalHealth;
        _isDestroyedByCollisionWithPlayerSpaceship = false;
        _registerCollision = true;
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
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
        OnDestroyed?.Invoke(this);
        if (ScoreCounter.Instance != null) ScoreCounter.Instance.AddScore(_score);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        //if (!_registerCollision) return;
        if (Health <= 0) return;

        if (other.gameObject.TryGetComponent(out IPlayer player))
        {
            Health--;
            if (other.TryGetComponent(out Projectile projectile))
                projectile.Despawn();
            //_registerCollision = false;
            //_coroutine = StartCoroutine(EnableCollision());
            if (player is Spaceship)
            {
                _isDestroyedByCollisionWithPlayerSpaceship = true;
                Health = 0;
            }

            OnHealthReduced();
        }
    }

    private IEnumerator EnableCollision()
    {
        yield return new WaitForSeconds(.1f);
        _registerCollision = true;
    }
}