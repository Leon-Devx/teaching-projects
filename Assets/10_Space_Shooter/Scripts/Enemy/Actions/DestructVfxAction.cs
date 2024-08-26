using UnityEngine;

public class DestructVfxAction : MonoBehaviour
{
    [SerializeField] private GameObject _destructEffect;

    private Enemy _enemy;

    private void Awake() => _enemy = GetComponent<Enemy>();
    private void OnEnable() => _enemy.OnDestroyed += SpawnVfx;

    private void SpawnVfx()
    {
        Vector2 spawnPosition = transform.localPosition;
        Instantiate(_destructEffect, spawnPosition, _destructEffect.transform.rotation);
    }
}