using System;
using System.Collections;
using Lean.Pool;
using UnityEngine;

public class Spaceship : MonoBehaviour, IPlayer
{
    private static Spaceship _instance;
    public static Spaceship Instance => _instance;
    
    public event Action<Spaceship> OnTakeDamage;
    public event Action<int> OnUpdateLives;
    public event Action OnDestroyed;

    [SerializeField] private int _lives = 3;
    [SerializeField] private Transform _destructVfx;
    [SerializeField] private Vector2 _respawnPosition;

    private InvincibilityAction _invincibilityAction;
    private ShieldAction _shieldAction;
    private SpaceshipShootAction _shootAction;
    private Coroutine _collisionCoroutine;
    private WaitForSeconds _wait;

    private bool _hasCollided = false;
    
    #region Properties

    public int Lives
    {
        get => _lives;
        set
        {
            _lives = value;
            OnUpdateLives?.Invoke(_lives);
        }
    }

    public ShieldAction ShieldAction => _shieldAction;
    public SpaceshipShootAction ShootAction => _shootAction;

    #endregion

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);

        _invincibilityAction = GetComponent<InvincibilityAction>();
        _shieldAction = GetComponent<ShieldAction>();
        _shootAction = GetComponent<SpaceshipShootAction>();
        _wait = new WaitForSeconds(.1f);
    }

    private void OnEnable() => SettingsUI.OnAnyClickRestartButton += InstantDestruct;
    private void OnDisable() => SettingsUI.OnAnyClickRestartButton -= InstantDestruct;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_hasCollided) return;
        if (_invincibilityAction.IsInvincible) return;
        if (other.TryGetComponent(out IDamageDealer damageDealer))
        {
            _hasCollided = true;
            if (_collisionCoroutine != null) StopCoroutine(_collisionCoroutine);
            _collisionCoroutine = StartCoroutine(ResetCollision());
            
            if (damageDealer is EnemyProjectile enemyProjectile)
                enemyProjectile.Despawn();
            
            if (_shieldAction.IsShieldEnabled)
            {
                _shieldAction.OnShieldHit();
                return;
            }
            
            Destruct();
        }
    }

    private IEnumerator ResetCollision()
    {
        yield return _wait;
        _hasCollided = false;
    }

    private void InstantDestruct()
    {
        Lives = 0;
        Destruct();
    }

    private void Destruct()
    {
        if (_destructVfx != null)
            LeanPool.Spawn(_destructVfx.gameObject, transform.localPosition, _destructVfx.rotation);

        Lives -= 1;
        if (Lives < 0)
        {
            gameObject.SetActive(false);
            OnDestroyed.Invoke();
            return;
        }

        OnTakeDamage?.Invoke(this);
        transform.localPosition = _respawnPosition;

        ShootAction.ProjectileCount -= 2;
        ShootAction.AttackSpeedPowerups -= 2;
    }
}