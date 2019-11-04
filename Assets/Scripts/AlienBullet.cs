using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBullet : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, -moveSpeed * Time.deltaTime, 0);
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

        if (col.gameObject.tag == "Player")
        {
            PlayerController _playerController = col.gameObject.GetComponent<PlayerController>();
            if (_playerController != null)
            {
                _playerController.TakeDamage();
                Destroy(gameObject);
            }
        }
    }
}
