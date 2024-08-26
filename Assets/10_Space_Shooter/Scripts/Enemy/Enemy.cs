using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public event Action OnDestroyed;

    protected virtual void DestroyActionPerformed()
    {
        OnDestroyed?.Invoke();
    }
}
