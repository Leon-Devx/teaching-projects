using System.Collections.Generic;
using UnityEngine;

public class ProjectileMover : MonoBehaviour
{
    private Dictionary<int, Projectile> _projectileDictionary = new Dictionary<int, Projectile>();

    private void OnEnable()
    {
        Projectile.OnAnyProjectileSpawn += RegisterProjectile;
        Projectile.OnAnyProjectileDeSpawn += RemoveProjectile;
    }

    private void OnDisable()
    {
        Projectile.OnAnyProjectileSpawn -= RegisterProjectile;
        Projectile.OnAnyProjectileDeSpawn -= RemoveProjectile;
    }

    private void Update()
    {
        MoveProjectiles();
    }

    private void RegisterProjectile(Projectile projectile)
    {
        _projectileDictionary.Add(projectile.gameObject.GetInstanceID(), projectile);
    }

    private void RemoveProjectile(Projectile projectile)
    {
        int projectileId = projectile.gameObject.GetInstanceID();
        if (_projectileDictionary.ContainsKey(projectileId))
        {
            _projectileDictionary.Remove(projectileId);
        }
    }

    private void MoveProjectiles()
    {
        for (int i = 0; i < _projectileDictionary.Count; i++)
            _projectileDictionary[i].HandleMovement();
    }
}