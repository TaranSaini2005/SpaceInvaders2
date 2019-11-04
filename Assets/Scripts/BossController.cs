using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    // Movement varibles 
    public float moveSpeed;
    Rigidbody2D _rigidBody;
    Vector2 movement;

    // Animation varibles
    SpriteRenderer _spriteRenderer;
    public Sprite baseImage;
    public Sprite AltImage;
    public Sprite explodeAnimation;
    public float imageChangeTime = 0.5f;

    // Shooting varibles
    public Transform cannonTip;
    public GameObject bulletPrefab;

    float fireRate;
    float nextFire;
  
    public int health;
    BossSpawner bossSpawner;

    // Use this for initialization
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidBody = GetComponent<Rigidbody2D>();
        bossSpawner = GameObject.FindObjectOfType<BossSpawner>();

        StartCoroutine(CycleAlienSpriteImage());

        fireRate = Random.Range(1, 3);
        nextFire = fireRate;

        movement = new Vector3(1, 0);
        _rigidBody.velocity = movement * moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad > nextFire)
        {
            fireRate = Random.Range(1, 3);
            nextFire = Time.timeSinceLevelLoad + fireRate;
            Instantiate(bulletPrefab, cannonTip.transform.position, Quaternion.identity);
        }
    }

    // Turn Alien in opposite direction after reaching boundaries
    public void ReverseDirection(int direction)
    {
        Vector2 newVelocity = _rigidBody.velocity;
        newVelocity.x = moveSpeed * direction;
        _rigidBody.velocity = newVelocity;
    }

    void Die()
    {

        StopCoroutine(CycleAlienSpriteImage());
        AudioManager.audioManager.PlaySound("Boss Hit");
        _spriteRenderer.sprite = explodeAnimation;
        Destroy(gameObject, 0.1f);
    }

    private void OnDestroy()
    {

        if (bossSpawner.bosses.Contains(gameObject))
        {
            bossSpawner.bosses.Remove(gameObject);
        }
    }

    public void TakeDamage()
    {
        AudioManager.audioManager.PlaySound("Alien Hit");
        health--;
        if (health <= 0)
        {
            Die();
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
