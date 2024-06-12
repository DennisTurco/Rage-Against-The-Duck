using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObject : MonoBehaviour
{
    [SerializeField] private int hitsToDestroy = 3;
    [SerializeField] private float hitAnimationFrameDelay = 0.1f; // Delay between hit animation frames
    [SerializeField] private float destructionAnimationFrameDelay = 0.1f; // Delay between destruction animation frames
    [SerializeField] private Sprite[] hitAnimationFrames; // Frames for hit animation
    [SerializeField] private Sprite[] destructionAnimationFrames; // Frames for destruction animation
    [SerializeField] private GameObject droppedItemPrefab; // Prefab for the dropped item
    [SerializeField] private List<LootSpawn> lootList = new List<LootSpawn>(); // List of possible loot items
    [SerializeField] private int maxNumberOfDrops = 3; // Maximum number of items to drop
    [SerializeField] private Collider2D playerCollider; // Collider for player interaction
    [SerializeField] private Collider2D destructionCollider; // Collider for destruction

    private SpriteRenderer spriteRenderer;
    private bool isDestroyed = false;
    private int currentHits = 0;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerCollider.enabled = true;
        destructionCollider.enabled = true;
    }

    // Method to handle when the object takes a hit
    public void TakeHit()
    {
        if (isDestroyed) return;

        currentHits++;

        if (currentHits < hitsToDestroy)
        {
            // Play hit animation
            StartCoroutine(PlayHitAnimation());
        }
        else
        {
            // Destroy the object
            DestroyObject();
        }
    }

    // Coroutine to play the hit animation
    private IEnumerator PlayHitAnimation()
    {
        // Play each frame of the hit animation
        foreach (var frame in hitAnimationFrames)
        {
            spriteRenderer.sprite = frame;
            yield return new WaitForSeconds(hitAnimationFrameDelay);
        }

        // Keep the last frame of the hit animation for a brief period
        yield return new WaitForSeconds(hitAnimationFrameDelay);
    }

    // Method to destroy the object
    public void DestroyObject()
    {
        if (!isDestroyed)
        {
            isDestroyed = true;
            StartCoroutine(PlayDestructionAnimation());
        }
    }

    // Coroutine to play the destruction animation
    private IEnumerator PlayDestructionAnimation()
    {
        // Play each frame of the destruction animation
        foreach (var frame in destructionAnimationFrames)
        {
            spriteRenderer.sprite = frame;
            yield return new WaitForSeconds(destructionAnimationFrameDelay);
        }

        // Keep the last frame of the destruction animation
        spriteRenderer.sprite = destructionAnimationFrames[destructionAnimationFrames.Length - 1];

        // Disable the colliders
        playerCollider.enabled = false;
        destructionCollider.enabled = false;

        // Spawn the loot
        InstantiateLootSpawn(transform.position);
    }

    // Method to instantiate the loot items
    private void InstantiateLootSpawn(Vector3 spawnPosition)
    {
        List<LootSpawn> droppedItems = GetDroppedItems();

        if (droppedItems == null || droppedItems.Count == 0) return;

        foreach (var droppedItem in droppedItems)
        {
            // Add a random offset to the spawn position
            Vector3 randomOffset = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
            Vector3 itemPosition = spawnPosition + randomOffset;
            GameObject lootGameObject = Instantiate(droppedItemPrefab, itemPosition, Quaternion.identity);
            lootGameObject.GetComponent<SpriteRenderer>().sprite = droppedItem.lootSprite;

            // Set the custom name to the prefab
            lootGameObject.name = droppedItem.lootName.ToString();

            // Apply an initial force to the object to "throw" it
            Rigidbody2D rb = lootGameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 launchDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(0.5f, 1f)).normalized;
                float launchForce = Random.Range(2f, 5f);
                rb.AddForce(launchDirection * launchForce, ForceMode2D.Impulse);
            }

            // Ensure the object is assigned to the correct layer
            lootGameObject.layer = LayerMask.NameToLayer("Item");
        }
    }

    // Method to get the items to be dropped
    private List<LootSpawn> GetDroppedItems()
    {
        List<LootSpawn> droppedItems = new List<LootSpawn>();

        for (int i = 0; i < maxNumberOfDrops; i++)
        {
            int random = Random.Range(1, 101); // random between 1 and 100
            foreach (LootSpawn item in lootList)
            {
                if (random <= item.dropChance)
                {
                    droppedItems.Add(item); // Changed 'add' to 'Add'
                    break; // Drop only one item per iteration
                }
            }
        }

        if (droppedItems.Count > 0)
        {
            return droppedItems;
        }

        Debug.Log("No loot dropped");
        return null;
    }
}