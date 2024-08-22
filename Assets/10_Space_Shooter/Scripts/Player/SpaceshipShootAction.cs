using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipShootAction : MonoBehaviour
{
    [SerializeField] private GameObject _projectile;
    [SerializeField] private Transform[] _spawnLocationsArray = new Transform[2];
    [SerializeField] private float _projectileSpawnDelay = 0.5f;

    private Vector3 _spawnPosition;

    private float _projectileSpawnTimer;

    private int _spawnLocationIndex = 0;

    private void Start()
    {
        _spawnPosition = _spawnLocationsArray[_spawnLocationIndex].position;
    }

    private void Update()
    {
        _projectileSpawnTimer -= Time.deltaTime;
        if (_projectileSpawnTimer <= 0f)
        {
            SpawnProjectile();
            _projectileSpawnTimer = _projectileSpawnDelay;
        }
    }

    private void SpawnProjectile()
    {
        _spawnLocationIndex++;
        _spawnPosition = _spawnLocationsArray[_spawnLocationIndex % 2].position;
        Instantiate(_projectile, _spawnPosition, _projectile.transform.localRotation);
    }
}
