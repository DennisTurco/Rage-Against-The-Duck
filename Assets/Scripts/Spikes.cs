using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private float swapinterval = 0.15f;
    private bool trapActive;
    private float timeSinceLastSwap;
    private int currentSpriteIndex = 0;
    private SpriteRenderer spriteRenderer;
    private List<Collider2D> collidersInTrap = new List<Collider2D>();
    private List<Collider2D> collidersToRemove = new List<Collider2D>();
    private List<Collider2D> collidersToDamage = new List<Collider2D>();

    private void Start()
    {
        // Initialize the trap as inactive
        trapActive = false;

        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Set the initial sprite if the sprite list is not empty
        if (sprites.Count > 0)
        {
            spriteRenderer.sprite = sprites[0];
        }
    }

    private void Update()
    {
        // Increment the time elapsed since the last sprite swap
        timeSinceLastSwap += Time.deltaTime;

        // Keep track of the previous trap state
        bool wasTrapActive = trapActive;

        // Update the trap state based on the sprite index
        if (currentSpriteIndex >= 2 && currentSpriteIndex <= 5)
        {
            trapActive = true;
        }
        else
        {
            trapActive = false;
        }

        // If the trap becomes active, inflict damage to the colliders within the box
        if (trapActive && !wasTrapActive)
        {
            foreach (var collider in collidersInTrap)
            {
                if (collider == null)
                {
                    collidersToRemove.Add(collider);
                }
                else
                {
                    collidersToDamage.Add(collider);
                }
            }

            // Process damage after collecting colliders to avoid modifying the list during iteration
            foreach (var collider in collidersToDamage)
            {
                if (collider.gameObject.TryGetComponent<HealthBar>(out HealthBar barComponent))
                {
                    barComponent.TakeDamage();
                }
                else if (collider.gameObject.TryGetComponent<EnemyAI>(out EnemyAI AIbarComponent))
                {
                    AIbarComponent.TakeDamage(1);
                }
            }

            // Clear the list of colliders to damage after processing
            collidersToDamage.Clear();

            // Remove null colliders from the list
            foreach (var collider in collidersToRemove)
            {
                collidersInTrap.Remove(collider);
            }
            collidersToRemove.Clear();
        }

        // Swap the sprite if the swap interval has passed
        if (timeSinceLastSwap >= swapinterval)
        {
            timeSinceLastSwap = 0f;
            SwapSprite();
        }
    }

    // Method to change the trap sprite
    private void SwapSprite()
    {
        if (sprites.Count > 0)
        {
            // Update the sprite index and set the new sprite
            currentSpriteIndex = (currentSpriteIndex + 1) % sprites.Count;
            spriteRenderer.sprite = sprites[currentSpriteIndex];
        }
    }

    // Method called when an object enters the collision box
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Add the collider to the list if it's not already present
        if (!collidersInTrap.Contains(collision))
        {
            collidersInTrap.Add(collision);
        }

        // Inflict immediate damage if the trap is active
        if (trapActive)
        {
            if (collision.gameObject.TryGetComponent<HealthBar>(out HealthBar barComponent))
            {
                barComponent.TakeDamage();
            }
            else if (collision.gameObject.TryGetComponent<EnemyAI>(out EnemyAI AIbarComponent))
            {
                AIbarComponent.TakeDamage(1);
            }
        }
    }

    // Method called when an object exits the collision box
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Remove the collider from the list when it exits the collision box
        if (collidersInTrap.Contains(collision))
        {
            collidersInTrap.Remove(collision);
        }
    }
}
