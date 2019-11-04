using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherShip : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;

    // Spawning power-ups
    public GameObject[] powerUps;
    int spawnIndex;
    int spawnCounter;

    // Death animation 
    SpriteRenderer _spriteRenderer;
    public Sprite explodeAnimation;

    private void Start()
    {
        spawnCounter = Random.Range(1, 10);
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveSpeed * Time.deltaTime,0 , 0);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "End")
        {
            Destroy(gameObject);
        }
    }

     public void Die()
    {
        //Check to see if we want to spawn a power-up(spawn rate determined randomly) 
        // Spawn a power-up(randomly generated) 
        if (spawnCounter > 5)
        {
            spawnIndex = Random.Range(0,powerUps.Length);
            Instantiate(powerUps[spawnIndex], gameObject.transform.position, Quaternion.identity);
        }

        // Destroy mothership
        _spriteRenderer.sprite = explodeAnimation;
        Destroy(gameObject, 0.1f);
    }

}
