using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSpawnStart : MonoBehaviour
{
    [SerializeField]
    float minSPR;

    [SerializeField]
    float maxSPR;

    [SerializeField]
    GameObject motherShip;

    public bool canWeSpawn;
    float spawnRate = 0;


    // Start is called before the first frame update
    void Start()
    {
        canWeSpawn = true;
        spawnRate = spawnRate + Random.Range(minSPR, maxSPR);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad > spawnRate && canWeSpawn == true)
        {
            Instantiate(motherShip, gameObject.transform.position, Quaternion.identity);
            canWeSpawn = false;
        }
    }

    public void CreateNewSpawnRate()
    {
        spawnRate = Random.Range(minSPR, maxSPR) + Time.timeSinceLevelLoad;
    }

    public void BeginSpawning()
    {
        canWeSpawn = true;
    }
}
