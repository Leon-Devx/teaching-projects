using Lean.Pool;
using UnityEngine;

public class AttackSpeed : Powerup
{
    [SerializeField] private Transform _floatingPlusOneAts;

    protected override void OnCollectPowerup(Spaceship spaceship)
    {
        base.OnCollectPowerup(spaceship);
        spaceship.ShootAction.AttackSpeedPowerups++;

        if (_floatingPlusOneAts != null)
            LeanPool.Spawn(_floatingPlusOneAts.gameObject, transform.localPosition, _floatingPlusOneAts.rotation);
    }
}