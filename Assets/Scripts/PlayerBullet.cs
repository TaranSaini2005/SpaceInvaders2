using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float moveSpeed;
    PlayerController _playerController;

    private void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update ()
    {
        transform.Translate(0, moveSpeed * Time.deltaTime, 0);
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Barrier")
        {
            Destroy(gameObject);
        }

        if (col.gameObject.tag == "Rock")
        {
            Rock _rock = col.gameObject.GetComponent<Rock>();
            if (_rock != null)
            {
                _rock.TakeDamage();
                Destroy(gameObject);
            }
        }

        if (col.gameObject.tag == "Alien")
        {
            AlienController _alienController = col.gameObject.GetComponent<AlienController>();
            if (_alienController != null)
            {
                _alienController.Die();
                IncreaseScore(10);
                Destroy(gameObject);
            }
        }

        if (col.gameObject.tag == "Boss")
        {
            BossController _bossController = col.gameObject.GetComponent<BossController>();
            if (_bossController != null)
            {
                _bossController.TakeDamage();
                IncreaseScore(5);
                Destroy(gameObject);
            }
        }

        if (col.gameObject.tag == "Ship")
        {
            MotherShip _motherShip = col.gameObject.GetComponent<MotherShip>();
            if (_motherShip != null)
            {
                _motherShip.Die();
                AudioManager.audioManager.PlaySound("Ship Hit");

                ShipSpawnStart _shipSpawner = GameObject.FindObjectOfType<ShipSpawnStart>();
                _shipSpawner.canWeSpawn = true;
                _shipSpawner.CreateNewSpawnRate();

                IncreaseScore(50);
                Destroy(gameObject);
            }
        }
        
    }

    void IncreaseScore (int amount)
    {
        _playerController.score += amount;
        DataControl.dataControl.currentScore = _playerController.score;
        DataControl.dataControl.Save();

        if (_playerController.score > DataControl.dataControl.highScore)
        {
            DataControl.dataControl.highScore = _playerController.score;
            DataControl.dataControl.Save();
        }
    }

    private void OnDestroy()
    {
        _playerController.ReloadBullets();
    }
}
