using System.Collections;
using Lean.Pool;
using UnityEngine;

public class KamikazeEnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform _enemy;
    [SerializeField] private float _minSpawnInterval = 5f;
    [SerializeField] private float _maxSpawnInterval = 8f;

    private Collider2D _collider2D;
    private Vector3 _colliderExtents;

    private void Awake()
    {
        _collider2D = GetComponent<Collider2D>();
        _colliderExtents = _collider2D.bounds.extents;
    }

    private void OnEnable()
    {
        DifficultyController.OnAnyEnableKamikazeEnemy += EnableSpawner;
    }

    private void OnDisable()
    {
        DifficultyController.OnAnyEnableKamikazeEnemy -= EnableSpawner;
    }

    public void EnableSpawner() => StartCoroutine(SpawnEnemy());

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            Vector2 _spawnerPosition = transform.localPosition;
            float xPosition = Random.Range(-_colliderExtents.x, _colliderExtents.x) + _spawnerPosition.x;
            float yPosition = Random.Range(-_colliderExtents.y, _colliderExtents.y) + _spawnerPosition.y;
            Vector2 spawnPosition = new Vector2(xPosition, yPosition);

            LeanPool.Spawn(_enemy, spawnPosition, _enemy.localRotation);

            float randomWaitDuration = Random.Range(_minSpawnInterval, _maxSpawnInterval);

            yield return new WaitForSeconds(randomWaitDuration);
        }
    }
}