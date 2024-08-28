using System;
using UnityEngine;

public class Spaceship : MonoBehaviour, IPlayer
{
    public event Action<Spaceship> OnTakeDamage;
    public event Action OnDestroyed;

    [SerializeField] private int _lives = 3;
    [SerializeField] private Transform _destructVfx;
    [SerializeField] private Vector2 _respawnPosition;

    private InvincibilityAction _invincibilityAction;

    #region Properties

    public int Lives => _lives;

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
        _lives = 0;
        Destruct();
    }

    private void Destruct()
    {
        if (_destructVfx != null)
            Instantiate(_destructVfx, transform.localPosition, _destructVfx.rotation);
        
        _lives -= 1;
        if (_lives < 0)
        {
            gameObject.SetActive(false);
            OnDestroyed.Invoke();
            return;
        }

        OnTakeDamage?.Invoke(this);
        transform.localPosition = _respawnPosition;
    }
}