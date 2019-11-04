using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField]
    int health;

    // Sprite stuff
    SpriteRenderer _spriteRenderer;
    BoxCollider2D _boxCollider;

    public Sprite[] AltImages;
    public Sprite deathAnimation;
    int spriteCounter = 0;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();   // Cache reference to this gameobjects sprite renderer
        _boxCollider = GetComponent<BoxCollider2D>();

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Alien")
        {
            AlienController _alienController = col.gameObject.GetComponent<AlienController>();
            if (_alienController != null)
            {
                _alienController.Die();
                Destroy(gameObject);
            }
        }
    }

    public void TakeDamage()
    {
        health--;
        AudioManager.audioManager.PlaySound("Rock Hit");

        if (health == 0)
        {
            Die();  
        }
        else
        {
            // Update sprite (rock being destroyed more and more as it is hit by the player)
            _spriteRenderer.sprite = AltImages[spriteCounter];
        }

        // Increase the sprite counter only if we have sprite avaiable within the AltImage array
        if (spriteCounter < AltImages.Length)
        {
            spriteCounter++;
        }       
    }

    void Die()
    {
        _boxCollider.enabled = false;
        _spriteRenderer.sprite = deathAnimation;
        Destroy(gameObject, 0.1f);
    }
}
