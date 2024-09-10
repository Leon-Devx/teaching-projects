using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    [SerializeField] private TextMeshPro _scoreText;

    public void UpdateScoreText(int score) => _scoreText.text = $"+{score}";
}