using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class DroppablePowerup
{
    [SerializeField] [Range(0, 100)] private int _dropChance;
    [SerializeField] private Powerup _droppableObj;

    public Transform TryGetPowerupToSpawn()
    {
        switch (_droppableObj)
        {
            case AdditionalLife additionalLife:
                if (Spaceship.Instance.Lives >= 5) return null;
                break;
            case Shield shield:
                if (Spaceship.Instance.ShieldAction.IsShieldEnabled) return null;
                break;
            case AttackSpeed attackSpeed:
                if (Spaceship.Instance.ShootAction.AttackSpeedPowerups >=
                    Spaceship.Instance.ShootAction.MaxAttackSpeedPowerups) return null;
                break;
            case ExtraProjectile extraProjectile:
                if (Spaceship.Instance.ShootAction.ProjectileCount >=
                    Spaceship.Instance.ShootAction.MaxProjectileCount) return null;
                break;
        }

        int chance = Random.Range(0, 100);
        if (_dropChance >= chance)
            return _droppableObj.transform;

        return null;
    }
}