using UnityEngine;

public class AttackSpeed : Powerup
{
    [SerializeField] private Transform _floatingPlusOneAts;
    
    protected override void OnCollectPowerup(Spaceship spaceship)
    {
        base.OnCollectPowerup(spaceship);
        spaceship.ShootAction.AttackSpeedPowerups++;

        if (_floatingPlusOneAts != null)
            Instantiate(_floatingPlusOneAts, transform.localPosition, _floatingPlusOneAts.rotation);
    }
}
