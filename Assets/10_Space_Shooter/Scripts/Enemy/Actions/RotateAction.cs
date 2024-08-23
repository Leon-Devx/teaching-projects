using UnityEngine;

public class RotateAction : MonoBehaviour
{
    [Range(-2f, 2f)] [SerializeField] private float _minRotation = -5f;
    [Range(-2f, 2f)] [SerializeField] private float _maxRotation = 5f;

    private float _rotation;

    private void Start()
    {
        _rotation = Random.Range(_minRotation, _maxRotation);
    }

    private void Update()
    {
        transform.Rotate(0f, 0f, _rotation);
    }
}