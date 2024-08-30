using UnityEngine;
using System;

public class Enemy : MonoBehaviour, IDamageDealer
{
    public event Action OnDestroyed;

    [SerializeField] private int _score;

    public int Score => _score;

    protected virtual void DestroyActionPerformed()
    {
        OnDestroyed?.Invoke();
        if (ScoreCounter.Instance != null) ScoreCounter.Instance.AddScore(_score);
    }
}