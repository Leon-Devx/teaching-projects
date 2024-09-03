using DG.Tweening;
using UnityEngine;

public class SpriteFlashAction : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private Enemy _enemy;
    private Material _material;
    private Tween _flashTween;

    private readonly string _hitEffectBlend = "_HitEffectBlend";

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _material = _spriteRenderer?.material;
    }

    private void OnEnable()
    {
        _enemy.OnDamaged += Flash;
        _enemy.OnDestroyed += Reset;
    }

    private void Flash()
    {
        _material?.SetFloat(_hitEffectBlend, 1f);
        KillTween();
        _flashTween = DOVirtual.DelayedCall(.15f, () =>
        {
            _material?.SetFloat(_hitEffectBlend, 0f);
        });
    }

    private void Reset()
    {
        KillTween();
        _material?.SetFloat(_hitEffectBlend, 0f);
    }

    private void KillTween()
    {
        if (_flashTween != null) _flashTween.Kill();
    }
}