using System;
using UnityEngine;

public class SpaceshipShootAction : MonoBehaviour
{
    [SerializeField] private GameObject _projectile;
    [SerializeField] private GameObject _muzzleEffect;
    [SerializeField] private float _projectileSpawnDelay = 0.5f;

    [Header("Projectile Bonuses (Attack Speed)")] [SerializeField]
    private int _attackSpeedPowerups = 0;

    [SerializeField] private float _attackSpeedPowerupPercentage = 15f;
    [SerializeField] private int _maxAttackSpeedPowerups = 25;

    [Header("Projectile Bonuses (Projectile Count)")] [SerializeField]
    private int _projectileCount = 1;

    [SerializeField] private ProjectileSpawnerSet[] _projectileSpawnerSetArray;

    private float _projectileSpawnTimer;

    private int _spawnLocationIndex = 0;

    private ProjectileSpawnerSet _projectileSpawnerSet;

    #region Properties

    public int AttackSpeedPowerups
    {
        get => _attackSpeedPowerups;
        set => _attackSpeedPowerups = Mathf.Clamp(value, 0, _maxAttackSpeedPowerups);
    }

    public int ProjectileCount
    {
        get => _projectileCount;
        set => _projectileCount = Mathf.Clamp(value, 1, _projectileSpawnerSetArray.Length);
    }

    public int MaxAttackSpeedPowerups => _maxAttackSpeedPowerups;
    public int MaxProjectileCount => _projectileSpawnerSetArray.Length;

    #endregion

    private void Update()
    {
        _projectileSpawnTimer -= Time.deltaTime;
        if (_projectileSpawnTimer <= 0f)
        {
            if (_projectileCount == 1)
                SpawnIndividualProjectiles();
            else
                SpawnMultipleProjectiles();

            _projectileSpawnTimer = GetAttackRateTimer();
        }
    }

    private float GetAttackRateTimer()
    {
        float attackRate = _projectileSpawnDelay;
        for (int i = 0; i < _attackSpeedPowerups; i++)
        {
            float attackRateBoost = attackRate * (_attackSpeedPowerupPercentage / 100f);
            attackRate -= attackRateBoost;
        }

        return attackRate;
    }

    private void SpawnIndividualProjectiles()
    {
        _spawnLocationIndex++;
        _projectileSpawnerSet = _projectileSpawnerSetArray[_projectileCount - 1];
        InitSpawnProjectile(_projectileSpawnerSet.SpawnLocationsArray[_spawnLocationIndex % 2], true);
    }

    private void SpawnMultipleProjectiles()
    {
        _projectileSpawnerSet = _projectileSpawnerSetArray[_projectileCount - 1];
        SpawnProjectiles();
    }

    private void SpawnProjectiles()
    {
        for (int i = 0; i < _projectileSpawnerSet.SpawnLocationsArray.Length; i++)
        {
            InitSpawnProjectile(_projectileSpawnerSet.SpawnLocationsArray[i], i < 2);
        }
    }

    private void InitSpawnProjectile(Transform spawnTransform, bool spawnMuzzleEffect)
    {
        Vector3 spawnPosition = spawnTransform.position;
        Vector3 rotation = spawnTransform.localRotation.eulerAngles;
        Instantiate(_projectile, spawnPosition, Quaternion.Euler(rotation));
        if (spawnMuzzleEffect)
            Instantiate(_muzzleEffect, spawnPosition, _muzzleEffect.transform.localRotation);
    }
}

[Serializable]
public struct ProjectileSpawnerSet
{
    [SerializeField] private int _index;
    [SerializeField] private Transform[] _spawnLocationsArray;

    public int Index => _index;
    public Transform[] SpawnLocationsArray => _spawnLocationsArray;
}