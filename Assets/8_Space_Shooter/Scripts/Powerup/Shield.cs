using UnityEngine;

public class Shield : Powerup
{
    [SerializeField] private Transform _floatingShield;
    
    protected override void OnCollectPowerup(Spaceship spaceship)
    {
        base.OnCollectPowerup(spaceship);
        
        if (_floatingShield != null)
            Instantiate(_floatingShield, transform.localPosition, _floatingShield.rotation);
    }
}
