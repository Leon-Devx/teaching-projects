using UnityEngine;

public class SizeRandomizerAction : MonoBehaviour
{
    [Range(0, 100f), SerializeField] private float _maxSizePercentage;

    private Vector2 _originalScale;
    
    private void Awake()
    {
        _originalScale = transform.localScale;
    }

    private void Start()
    {
        float randomScalePercentage = Random.Range(0f, _maxSizePercentage);
        Vector2 newScale = _originalScale;
        newScale += _originalScale * (randomScalePercentage / 100f);
        transform.localScale = newScale;
    }
}
