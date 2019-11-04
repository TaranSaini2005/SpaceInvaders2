using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    // public static PlayerController playerController;
    Vector2 playerStartPos;
    SpriteRenderer _spriteRenderer;
    BoxCollider2D _boxCollider;

    public Sprite explodeAnimation;
    public Sprite baseImage;
    public Sprite altImage;

    // Movement varibles
    public float moveSpeed;
    float moveHorizontal;
    Vector3 movement;
    Rigidbody2D _rigidBody;
    public float xMin, xMax;

    // Shooting varibles
    public Transform cannonTip;
    public GameObject bulletPrefab;
    public bool canShoot;

    int clipCount = 1;
    int ammoCount;

    public int lives = 3;
    public int score = 0;
    public bool shieldActive = false;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start ()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider2D>();

        ammoCount = clipCount;
        playerStartPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
    }

    // Update is called once per frame
    void Update ()
    {
        // Player shooting
        if (Input.GetKeyDown("space") && canShoot == true)
        {
            ammoCount--;
            Instantiate(bulletPrefab, cannonTip.transform.position, Quaternion.identity);
            AudioManager.audioManager.PlaySound("Player Shoot");
        }

        // Manging player ammo amount 
        if (ammoCount <= 0)
        {
            canShoot = false;
        }

        if (ammoCount > 0)
        {
            canShoot = true;
        } 
	}

    private void FixedUpdate()
    {
        // Moving the player 
        moveHorizontal = Input.GetAxis("Horizontal");
        movement = new Vector3(moveHorizontal, 0, 0);
        _rigidBody.velocity = movement * moveSpeed;

        // Adding player boundaries though scripting
        _rigidBody.position = new Vector3(Mathf.Clamp(_rigidBody.position.x, xMin, xMax), _rigidBody.position.y, 0);
    }

    public void EnableShield()
    {
        shieldActive = true;
        _spriteRenderer.sprite = altImage;
    }

    public void DisableShield()
    {
        shieldActive = false;
        _spriteRenderer.sprite = baseImage;
    }

    public void IncreaseClipCount()
    {
        if (clipCount < 2)
        {
            clipCount++;
            ResetAmmo();
        }
    }

    public void DecreaseClipCount()
    {
        clipCount = 1;
        ResetAmmo();
    }

    public void ReloadBullets()
    {
        if (ammoCount < clipCount)
        {
            ammoCount++;
        }
    }

    public void ResetAmmo()
    {
        ammoCount = clipCount;
    }


    public void IncreaseLives()
    {
        lives++;
    }

    void ReduceLives()
    {
        lives--;
        AudioManager.audioManager.PlaySound("Player Death");
        if (lives <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(Respawn());
        }
    }

    public void TakeDamage()
    {
        if (shieldActive)
        {
            DisableShield();
        }
        else
        {
            ReduceLives();
        }
    }

    IEnumerator Respawn()
    {
        _boxCollider.enabled = false;
        _spriteRenderer.sprite = explodeAnimation;

        yield return new WaitForSeconds(0.1f);
        gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 100);
        yield return new WaitForSeconds(0.9f);

        // Repostion player 
        DecreaseClipCount();
        ResetAmmo();
        _spriteRenderer.sprite = baseImage; 
        gameObject.transform.position = playerStartPos;
        _boxCollider.enabled = true;
    }

    void Die()
    {
        _boxCollider.enabled = false;
        _spriteRenderer.sprite = explodeAnimation;
        Destroy(gameObject, 0.1f);
        SceneManager.LoadScene("Game Over");
    }

}
