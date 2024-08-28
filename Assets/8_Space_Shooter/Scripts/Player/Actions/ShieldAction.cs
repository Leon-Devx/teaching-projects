using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldAction : MonoBehaviour, IPlayer
{
    [SerializeField] private float _shieldDuration = 10f;

    private bool _isShieldEnabled;

    public bool IsShieldEnabled
    {
        get => _isShieldEnabled;
        set => _isShieldEnabled = value;
    }

    public void EnableShield()
    {
        
    }
}