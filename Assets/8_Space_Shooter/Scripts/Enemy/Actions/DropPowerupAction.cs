using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DropPowerupAction : MonoBehaviour
{
    [SerializeField] private List<DroppablePowerup> _droppableList;
    [SerializeField] [Range(0f, 100f)] private float _powerupDropChance;
    [SerializeField] private List<DroppablePickup> _pickupDroppableList;
    [SerializeField] [Range(0f, 100f)] private float _pickupDropChance;

    private Enemy _enemy;

    private void Awake() => _enemy = GetComponent<Enemy>();

    private void OnEnable()
    {
        _enemy.OnDestroyed += TryDropPowerup;
    }

    private void TryDropPowerup()
    {
        float randomValue = Random.Range(0f, 100f);
        if (_droppableList.Count > 0 && _powerupDropChance >= randomValue)
        {
            int randomDroppable = Random.Range(0, _droppableList.Count);
            Transform objectToSpawn = _droppableList[randomDroppable].TryGetPowerupToSpawn();
            if (objectToSpawn != null)
            {
                Instantiate(objectToSpawn, transform.position, objectToSpawn.rotation);
                return;
            }
        }

        TryDropPickup();
    }

    private void TryDropPickup()
    {
        float randomValue = Random.Range(0f, 100f);
        if (_pickupDroppableList.Count > 0 && _pickupDropChance >= randomValue)
        {
            int randomDroppable = Random.Range(0, _pickupDroppableList.Count);
            Transform objectToSpawn = _pickupDroppableList[randomDroppable].TryGetPickupToSpawn();
            if (objectToSpawn != null)
                Instantiate(objectToSpawn, transform.position, objectToSpawn.rotation);
        }
    }
}