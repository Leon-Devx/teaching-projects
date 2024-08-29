using UnityEngine;
using System.Collections;

public class ShieldAction : MonoBehaviour, IPlayer
{
    [SerializeField] private float _shieldDuration = 9f;
    [SerializeField] private float _shieldBlinkDelay = .15f;
    [SerializeField] private int _blinkCount = 5;
    [SerializeField] private GameObject _shield;

    private Coroutine _shieldDurationCoroutine;
    private Coroutine _shieldBlinkCoroutine;
    private WaitForSeconds _waitShieldDuration;
    private WaitForSeconds _waitBlinkDelay;
    
    private bool _isShieldEnabled;

    private void Awake()
    {
        _waitShieldDuration = new WaitForSeconds(_shieldDuration);
        _waitBlinkDelay = new WaitForSeconds(_shieldBlinkDelay);
    }

    public bool IsShieldEnabled
    {
        get => _isShieldEnabled;
        set => _isShieldEnabled = value;
    }

    public void ActivateShield()
    {
        if (IsShieldEnabled)
        {
            if (_shieldDurationCoroutine != null) StopCoroutine(_shieldDurationCoroutine);
            if (_shieldBlinkCoroutine != null) StopCoroutine(_shieldBlinkCoroutine);
        }
        
        IsShieldEnabled = true;
        _shield.SetActive(true);
        _shieldDurationCoroutine = StartCoroutine(ApplyShield());
    }

    private IEnumerator ApplyShield()
    {
        yield return _waitShieldDuration;
        _shieldBlinkCoroutine = StartCoroutine(BlinkShield());
    }

    private IEnumerator BlinkShield()
    {
        for (int i = 0; i < _blinkCount; i++)
        {
            _shield.SetActive(false);
            yield return _waitBlinkDelay;
            _shield.SetActive(true);
            yield return _waitBlinkDelay;
        }
        
        DeactivateShield();
    }

    private void DeactivateShield()
    {
        IsShieldEnabled = false;
        _shield.SetActive(false);
    }
}