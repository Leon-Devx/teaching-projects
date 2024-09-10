using System.Collections;
using UnityEngine;

public class TextMessageUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup _startGame;
    [SerializeField] private GameObject _gameOver;

    [Header("Flash")] [SerializeField] private float _displayStartDelay = 1.5f;
    [SerializeField] private float _flashDuration = .25f;
    [SerializeField] private int _flashCount = 8;

    private Spaceship _spaceship;
    private WaitForSeconds _waitStartDelay;
    private WaitForSeconds _waitFlash;

    private void Awake()
    {
        _startGame.enabled = true;
        _gameOver.SetActive(false);

        _spaceship = FindObjectOfType<Spaceship>();
        _waitStartDelay = new WaitForSeconds(_displayStartDelay);
        _waitFlash = new WaitForSeconds(_flashDuration);
    }

    private void OnEnable() => _spaceship.OnDestroyed += TryToDisplayGameOverText;
    private void OnDisable() => _spaceship.OnDestroyed -= TryToDisplayGameOverText;
    
    private void Start() => StartCoroutine(FlashGameText(_startGame));

    private IEnumerator FlashGameText(CanvasGroup canvasGroup)
    {
        yield return _waitStartDelay;

        for (int i = 0; i < _flashCount; i++)
        {
            canvasGroup.enabled = false;
            yield return _waitFlash;
            canvasGroup.enabled = true;
            yield return _waitFlash;
        }
    }

    private void TryToDisplayGameOverText()
    {
        _gameOver.SetActive(true);
    }
}