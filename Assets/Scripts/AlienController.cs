using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienController : MonoBehaviour
{
    // Movement varibles 
    public float moveSpeed;
    Rigidbody2D _rigidBody;
    Vector2 movement;
    int decreaseAmount = 1;

    // Animation varibles
    SpriteRenderer _spriteRenderer;
    public Sprite baseImage;
    public Sprite AltImage;
    public Sprite explodeAnimation;
    public float imageChangeTime = 0.5f;

    // Shooting varibles
    public Transform cannonTip;
    public GameObject bulletPrefab;
    float fireRate = 0;
    public float minFR, maxFR;

    bool canWeMoveDown = true;
    AlienSpawner alienSpawner;

    // Use this for initialization
    void Start ()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidBody = GetComponent<Rigidbody2D>();
        alienSpawner = GameObject.FindObjectOfType<AlienSpawner>();

        StartCoroutine(CycleAlienSpriteImage());

        fireRate = fireRate + Random.Range(minFR, maxFR);

        movement = new Vector3(1, 0);
        _rigidBody.velocity = movement * moveSpeed;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Time.timeSinceLevelLoad > fireRate)
        {
            // Set new FireRate
            fireRate = fireRate + Random.Range(minFR, maxFR);
            Instantiate(bulletPrefab, cannonTip.transform.position, Quaternion.identity);
        }

        if (gameObject.transform.position.y <= -9)
        {
            canWeMoveDown = false;
        }
    }

    // Turn Alien in opposite direction after reaching boundaries
    public void ReverseDirection(int direction)
    {
        Vector2 newVelocity = _rigidBody.velocity;
        moveSpeed++;
        newVelocity.x = moveSpeed * direction;
        _rigidBody.velocity = newVelocity;
    }

    // Move Alien down after reaching boundaries 
    public void MoveDown()
    {
        if (canWeMoveDown)
        {
            Vector2 newPosition = transform.position;
            newPosition.y -= decreaseAmount;
            transform.position = newPosition;
        }
       
    }

    public void Die()
    {
        StopCoroutine(CycleAlienSpriteImage());
        AudioManager.audioManager.PlaySound("Alien Hit");
        _spriteRenderer.sprite = explodeAnimation;
        Destroy(gameObject, 0.1f);
    }

    private void OnDestroy()
    {
        
        if (alienSpawner.aliens.Contains(gameObject))
        {
            alienSpawner.aliens.Remove(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            PlayerController _playerController = col.gameObject.GetComponent<PlayerController>();
            if (_playerController != null)
            {
                _playerController.TakeDamage();
                Destroy(gameObject);
            }
        }

        if (col.gameObject.tag == "Rock")
        {
            Rock _rock = col.gameObject.GetComponent<Rock>();
            if (_rock != null)
            {
                _rock.TakeDamage();
            }

            Destroy(gameObject);
        }
    }

    public IEnumerator CycleAlienSpriteImage()
    {
        while (true)
        {
            if (_spriteRenderer.sprite == baseImage)
            {
                _spriteRenderer.sprite = AltImage;
            }
            else
            {
                _spriteRenderer.sprite = baseImage;
            }

            yield return new WaitForSeconds(imageChangeTime);
        }
    }
}
