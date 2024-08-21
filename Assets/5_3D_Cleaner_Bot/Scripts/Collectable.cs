using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;

    private void Update()
    {
        transform.Rotate(0, _rotationSpeed * Time.deltaTime, 0f);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerMovement playerMovement))
            gameObject.SetActive(false);
    }
}