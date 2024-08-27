using UnityEngine;
using System.Collections;

public class InvincibilityAction : MonoBehaviour
{
    [SerializeField] private float _flashDuration = .25f;
    [SerializeField] private int _flashCount = 8;
    [SerializeField] private SpriteRenderer[] _spriteRendererArray;

    private Spaceship _spaceship;
    private WaitForSeconds _wait;

    private bool _isInvincible;

    public bool IsInvincible => _isInvincible;

    private void Awake()
    {
        _spaceship = GetComponent<Spaceship>();
        _wait = new WaitForSeconds(_flashDuration);
    }

    private void OnEnable()
    {
        _spaceship.OnTakeDamage += EnableInvincibility;
    }

    private void EnableInvincibility(Spaceship spaceship)
    {
        _isInvincible = true;
        StartCoroutine(DisableInvincibility());
    }

    private IEnumerator DisableInvincibility()
    {
        for (int i = 0; i < _flashCount; i++)
        {
            UpdateSpriteRenderers(false);
            yield return _wait;
            UpdateSpriteRenderers(true);
            yield return _wait;
        }

        _isInvincible = false;
    }

    private void UpdateSpriteRenderers(bool enabled)
    {
        for (int i = 0; i < _spriteRendererArray.Length; i++)
            _spriteRendererArray[i].enabled = enabled;
    }
}