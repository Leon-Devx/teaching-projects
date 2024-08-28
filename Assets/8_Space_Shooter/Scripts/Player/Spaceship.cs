using System;
using UnityEngine;

public class Spaceship : MonoBehaviour, IPlayer
{
    public event Action<Spaceship> OnTakeDamage;
    public event Action<int> OnUpdateLives;
    public event Action OnDestroyed;

    [SerializeField] private int _lives = 3;
    [SerializeField] private Transform _destructVfx;
    [SerializeField] private Vector2 _respawnPosition;

    private InvincibilityAction _invincibilityAction;

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

    #endregion

    private void Awake() => _invincibilityAction = GetComponent<InvincibilityAction>();
    private void OnEnable() => SettingsUI.OnAnyClickRestartButton += InstantDestruct;
    private void OnDisable() => SettingsUI.OnAnyClickRestartButton -= InstantDestruct;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_invincibilityAction.IsInvincible) return;
        if (other.TryGetComponent(out IDamageDealer damageDealer))
        {
            Destruct();
        }
    }

    private void InstantDestruct()
    {
        Lives = 0;
        Destruct();
    }

    private void Destruct()
    {
        if (_destructVfx != null)
            Instantiate(_destructVfx, transform.localPosition, _destructVfx.rotation);
        
        Lives -= 1;
        if (Lives < 0)
        {
            gameObject.SetActive(false);
            OnDestroyed.Invoke();
            return;
        }

        OnTakeDamage?.Invoke(this);
        transform.localPosition = _respawnPosition;
    }
}