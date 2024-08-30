using TMPro;
using UnityEngine;

public class ScorePresenter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreAmountText;

    private void Start()
    {
        ScoreCounter.Instance.OnUpdateScore += UpdateScore;
    }

    private void UpdateScore(int score)
    {
        if (score > 9000)
        {
            _scoreAmountText.text = "Over 9K!!!";
            return;
        }

        _scoreAmountText.text = score.ToString();
    }
}