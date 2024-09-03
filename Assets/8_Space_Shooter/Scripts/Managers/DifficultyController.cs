using System;
using UnityEngine;

public class DifficultyController : MonoBehaviour
{
    public static event Action OnAnyEnableKamikazeEnemy;

    [SerializeField] private float _enableKamikazeEnemyDuration = 5f;

    private float _timePassed = 0f;

    private bool _enableKamikazeEnemy;

    private void Update()
    {
        _timePassed = Time.time;
        TryToEnableKamikazeEnemy();
    }

    private void TryToEnableKamikazeEnemy()
    {
        if (_enableKamikazeEnemy) return;
        if (_timePassed > _enableKamikazeEnemyDuration)
        {
            _enableKamikazeEnemy = true;
            OnAnyEnableKamikazeEnemy?.Invoke();
        }
    }
}