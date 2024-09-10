using Lean.Pool;
using UnityEngine;

public class Pickup : Powerup
{
    [SerializeField] private GameObject _floatingText;

    [SerializeField] private int _score = 50;

    protected override void OnCollectPowerup(Spaceship spaceship)
    {
        base.OnCollectPowerup(spaceship);
        if (ScoreCounter.Instance != null) ScoreCounter.Instance.AddScore(_score);

        if (_floatingText != null)
        {
            GameObject textObj = LeanPool.Spawn(_floatingText, transform.localPosition, _floatingText.transform.rotation);
            ScoreText spawnedText = textObj.GetComponent<ScoreText>();
            spawnedText.UpdateScoreText(_score);
        }
    }
}
