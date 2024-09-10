using Lean.Pool;
using UnityEngine;

public class DestructVfxAction : MonoBehaviour
{
    [SerializeField] private GameObject _destructEffect;

    private Enemy _enemy;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _enemy.OnDestroyed += SpawnVfx;
    }

    private void SpawnVfx(Enemy enemy)
    {
        Vector2 spawnPosition = transform.localPosition;
        LeanPool.Spawn(_destructEffect, spawnPosition, _destructEffect.transform.rotation);
    }
}