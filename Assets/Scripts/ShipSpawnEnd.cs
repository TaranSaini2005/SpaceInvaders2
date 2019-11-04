using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSpawnEnd : MonoBehaviour
{
    [SerializeField]
    ShipSpawnStart _shipSpawner;   

    private void OnTriggerEnter2D(Collider2D col)
    {
        // If a mothership has collided with the endpoint - call ShipSpawnStart functions to spawn a new mothership
        if (col.gameObject.tag == "Ship")
        {
            _shipSpawner.CreateNewSpawnRate();
            _shipSpawner.BeginSpawning();   
        }
    }
}
