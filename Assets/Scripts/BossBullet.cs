﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;

    [SerializeField]
    float[] scaleCounters;

    float scaleSpeed;

    private void Start()
    {
        scaleSpeed = scaleCounters[Random.Range(0, scaleCounters.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, -moveSpeed * Time.deltaTime, 0);
        transform.localScale += new Vector3(0.1f, 0.1f,0f) * scaleSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Barrier")
        {
            Destroy(gameObject);
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
