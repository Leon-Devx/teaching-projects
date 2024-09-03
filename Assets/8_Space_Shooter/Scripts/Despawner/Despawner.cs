using System.Collections;
using Lean.Pool;
using UnityEngine;

public class Despawner : MonoBehaviour
{
    [SerializeField] private float _despawnTimer = 3f;
    private WaitForSeconds _wait;

    private Coroutine _coroutine;

    private void Awake()
    {
        _wait = new WaitForSeconds(_despawnTimer);
    }

    private void OnEnable()
    {
        TryToStopCoroutine();
        _coroutine = StartCoroutine(Despawn());
    }

    private void OnDisable() => TryToStopCoroutine();

    private void TryToStopCoroutine()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }

    private IEnumerator Despawn()
    {
        yield return _wait;
        LeanPool.Despawn(gameObject);
    }
}