using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public class ExtraProjectile : Powerup
{
    [SerializeField] private Transform _floatingPlusOneProjectile;
    
    protected override void OnCollectPowerup(Spaceship spaceship)
    {
        base.OnCollectPowerup(spaceship);
        spaceship.ShootAction.ProjectileCount++;

        if (_floatingPlusOneProjectile != null)
            LeanPool.Spawn(_floatingPlusOneProjectile.gameObject, transform.localPosition, _floatingPlusOneProjectile.rotation);
    }
}
