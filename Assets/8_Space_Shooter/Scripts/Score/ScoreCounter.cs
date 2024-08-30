using System;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public event Action<int> OnUpdateScore;
    
    private static ScoreCounter _instance;
    public static ScoreCounter Instance => _instance;

    [SerializeField] private int _score;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }

    private void Start() => AddScore(0);

    public void AddScore(int amount)
    {
        _score += amount;
        OnUpdateScore?.Invoke(_score);
    }
}