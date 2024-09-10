using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public class SplitAction : MonoBehaviour
{
    [SerializeField] private int _minSplitAmount = 2;
    [SerializeField] private int _maxSplitAmount = 3;

    [SerializeField] private List<GameObject> _smallerUnitList;

    [Header("Spawning Offset"), SerializeField]
    private Vector2 _spawningOffset;

    private Asteroid _asteroid;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _asteroid = GetComponent<Asteroid>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        
        _asteroid.OnDestroyed += Split;
    }

    private void Split(Enemy enemy)
    {
        int splitAmount = Random.Range(_minSplitAmount, _maxSplitAmount + 1);
        
        for (int i = 0; i < splitAmount; i++)
        {
            Vector2 spriteExtents = _spriteRenderer.sprite.bounds.extents;
            float xExtent = spriteExtents.x;
            float yExtent = spriteExtents.y;

            float randomXOffset = Random.Range(-_spawningOffset.x, _spawningOffset.x);
            float randomYOffset = Random.Range(-_spawningOffset.y, _spawningOffset.y);

            Vector2 currPosition = transform.position;

            float randomXSpawnPosition = Random.Range(-xExtent, xExtent) + currPosition.x + randomXOffset;
            float randomYSpawnPosition = Random.Range(-yExtent, yExtent) + currPosition.y + randomYOffset;

            Vector2 spawnPosition = new Vector2(randomXSpawnPosition, randomYSpawnPosition);
            int randomUnit = Random.Range(0, _smallerUnitList.Count);
            LeanPool.Spawn(_smallerUnitList[randomUnit].gameObject, spawnPosition, Quaternion.identity);
        }
    }
}