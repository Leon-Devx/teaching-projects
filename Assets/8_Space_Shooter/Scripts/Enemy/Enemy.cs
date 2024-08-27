using UnityEngine;
using System;

public class Enemy : MonoBehaviour, IDamageDealer
{
    public event Action OnDestroyed;

    protected virtual void DestroyActionPerformed()
    {
        OnDestroyed?.Invoke();
    }
}
