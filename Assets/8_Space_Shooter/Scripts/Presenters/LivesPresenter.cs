using TMPro;
using UnityEngine;

public class LivesPresenter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _livesText;

    private Spaceship _spaceship;

    private void Awake()
    {
        _spaceship = FindObjectOfType<Spaceship>();
        _spaceship.OnTakeDamage += UpdateLives;
        _spaceship.OnDestroyed += SetLivesToZero;
    }

    private void UpdateLives(Spaceship spaceship) => _livesText.text = $"X{spaceship.Lives}";
    private void SetLivesToZero() => _livesText.text = "X0";
}