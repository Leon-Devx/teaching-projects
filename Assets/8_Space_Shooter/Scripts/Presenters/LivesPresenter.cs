using TMPro;
using UnityEngine;

public class LivesPresenter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _livesText;

    private Spaceship _spaceship;

    private void Awake()
    {
        _spaceship = FindObjectOfType<Spaceship>();
        _spaceship.OnUpdateLives += UpdateLives;
    }

    private void UpdateLives(int lives)
    {
        if (lives < 0) lives = 0;
        _livesText.text = $"X{lives}";
    }
}