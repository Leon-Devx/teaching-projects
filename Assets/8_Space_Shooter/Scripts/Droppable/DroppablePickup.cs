using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class DroppablePickup
{
    [SerializeField] [Range(0, 100)] private int _dropChance;
    [SerializeField] private Powerup _droppableObj;
    
    public Transform TryGetPickupToSpawn()
    {
        int chance = Random.Range(0, 100);
        if (_dropChance >= chance)
            return _droppableObj.transform;

        return null;
    }
}
