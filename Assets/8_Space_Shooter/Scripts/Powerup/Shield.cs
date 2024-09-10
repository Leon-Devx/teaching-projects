using Lean.Pool;
using UnityEngine;

public class Shield : Powerup
{
    [SerializeField] private Transform _floatingShield;
    
    protected override void OnCollectPowerup(Spaceship spaceship)
    {
        base.OnCollectPowerup(spaceship);
        
        if (_floatingShield != null)
            LeanPool.Spawn(_floatingShield.gameObject, transform.localPosition, _floatingShield.rotation);
        
        if (spaceship.ShieldAction != null)
            spaceship.ShieldAction.ActivateShield();
    }
}
