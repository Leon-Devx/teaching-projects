using UnityEngine;

public class AdditionalLife : Powerup
{
    [SerializeField] private Transform _floatingPlusOneLife;

    protected override void OnCollectPowerup(Spaceship spaceship)
    {
        base.OnCollectPowerup(spaceship);
        spaceship.Lives++;

        if (_floatingPlusOneLife != null)
            Instantiate(_floatingPlusOneLife, transform.localPosition, _floatingPlusOneLife.rotation);
    }
}