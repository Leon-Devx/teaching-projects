using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private List<Asteroid> _asteroidList;
    [SerializeField] private float _spawnStartDelay = 1f;
    [SerializeField] private float _spawnInterval = 0.5f;

    private Collider2D _collider2D;
    private WaitForSeconds _waitStart;
    private WaitForSeconds _wait;
    private Vector3 _colliderExtents;

    private void Awake()
    {
        _collider2D = GetComponent<Collider2D>();
        _waitStart = new WaitForSeconds(_spawnStartDelay);
        _wait = new WaitForSeconds(_spawnInterval);
        _colliderExtents = _collider2D.bounds.extents;
    }

    private void Start() => StartCoroutine(SpawnAsteroid());

    private IEnumerator SpawnAsteroid()
    {
        yield return _waitStart;

        while (true)
        {
            Vector2 _spawnerPosition = transform.localPosition;
            float xPosition = Random.Range(-_colliderExtents.x, _colliderExtents.x) + _spawnerPosition.x;
            float yPosition = Random.Range(-_colliderExtents.y, _colliderExtents.y) + _spawnerPosition.y;
            Vector2 spawnPosition = new Vector2(xPosition, yPosition);

            int asteroidIndex = Random.Range(0, _asteroidList.Count);
            LeanPool.Spawn(_asteroidList[asteroidIndex].gameObject, spawnPosition,
                _asteroidList[asteroidIndex].transform.localRotation);

            yield return _wait;
        }
    }
}