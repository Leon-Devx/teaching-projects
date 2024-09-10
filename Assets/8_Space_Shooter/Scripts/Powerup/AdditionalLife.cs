using Lean.Pool;
using UnityEngine;

public class AdditionalLife : Powerup
{
    [SerializeField] private Transform _floatingPlusOneLife;

    protected override void OnCollectPowerup(Spaceship spaceship)
    {
        base.OnCollectPowerup(spaceship);
        spaceship.Lives++;

        if (_floatingPlusOneLife != null)
            LeanPool.Spawn(_floatingPlusOneLife.gameObject, transform.localPosition, _floatingPlusOneLife.rotation);
    }
}