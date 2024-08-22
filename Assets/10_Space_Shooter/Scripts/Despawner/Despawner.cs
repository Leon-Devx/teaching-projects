using System.Collections;
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
        _coroutine = StartCoroutine(Despawn());
    }

    private void OnDisable()
    {
        if (_coroutine == null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }

    private IEnumerator Despawn()
    {
        yield return _wait;
        gameObject.SetActive(false);
    }
}