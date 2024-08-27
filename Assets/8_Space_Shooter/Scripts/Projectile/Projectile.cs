using UnityEngine;

public class Projectile : MonoBehaviour, IPlayer
{
    [SerializeField] private float _speed = 15f;
    
    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector3 newPosition = transform.localPosition;
        float speed = _speed * Time.deltaTime;
        newPosition.y += speed;
        transform.localPosition = newPosition;
    }
}
