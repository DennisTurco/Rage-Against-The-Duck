using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObject : MonoBehaviour
{
    [SerializeField] private int hitsToDestroy = 3;
    [SerializeField] private float hitAnimationFrameDelay = 0.15f; // Delay tra i frame dell'animazione del colpo
    [SerializeField] private float destructionAnimationFrameDelay = 0.15f; // Delay tra i frame dell'animazione di distruzione
    [SerializeField] private Sprite[] hitAnimationFrames;
    [SerializeField] private Sprite[] destructionAnimationFrames;
    [SerializeField] private GameObject droppedItemPrefab;
    [SerializeField] private List<LootSpawn> lootList = new List<LootSpawn>();
    [SerializeField] private int maxNumberOfDrops = 3;
    [SerializeField] private Collider2D playerCollider;
    [SerializeField] private Collider2D destructionCollider;

    private SpriteRenderer spriteRenderer;
    private bool isDestroyed = false;
    private int currentHits = 0;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerCollider.enabled = true;
        destructionCollider.enabled = true;
    }

    private IEnumerator PlayHitAnimation()
    {
        foreach (var frame in hitAnimationFrames)
        {
            spriteRenderer.sprite = frame;
            yield return new WaitForSeconds(hitAnimationFrameDelay);
        }
    }

    public void TakeHit()
    {
        if (isDestroyed) return;

        currentHits++;

        if (currentHits < hitsToDestroy)
        {
            StartCoroutine(PlayHitAnimation());
        }
        else
        {
            DestroyObject();
        }
    }

    public void DestroyObject()
    {
        if (!isDestroyed)
        {
            isDestroyed = true;
            StartCoroutine(PlayDestructionAnimation());
        }
    }

    private IEnumerator PlayDestructionAnimation()
    {
        foreach (var frame in destructionAnimationFrames)
        {
            spriteRenderer.sprite = frame;
            yield return new WaitForSeconds(destructionAnimationFrameDelay);
        }

        spriteRenderer.sprite = destructionAnimationFrames[destructionAnimationFrames.Length - 1];
        playerCollider.enabled = false;
        destructionCollider.enabled = false;
        InstantiateLootSpawn(transform.position);
    }

    private void InstantiateLootSpawn(Vector3 spawnPosition)
    {
        List<LootSpawn> droppedItems = GetDroppedItems();

        if (droppedItems == null || droppedItems.Count == 0) return;

        foreach (var droppedItem in droppedItems)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
            Vector3 itemPosition = spawnPosition + randomOffset;
            GameObject lootGameObject = Instantiate(droppedItemPrefab, itemPosition, Quaternion.identity);
            lootGameObject.GetComponent<SpriteRenderer>().sprite = droppedItem.lootSprite;
            lootGameObject.name = droppedItem.lootName.ToString();

            Rigidbody2D rb = lootGameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 launchDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(0.5f, 1f)).normalized;
                float launchForce = Random.Range(2f, 5f);
                rb.AddForce(launchDirection * launchForce, ForceMode2D.Impulse);
            }

            lootGameObject.layer = LayerMask.NameToLayer("Item");
        }
    }

    private List<LootSpawn> GetDroppedItems()
    {
        List<LootSpawn> droppedItems = new List<LootSpawn>();

        for (int i = 0; i < maxNumberOfDrops; i++)
        {
            int random = Random.Range(1, 101);
            foreach (LootSpawn item in lootList)
            {
                if (random <= item.dropChance)
                {
                    droppedItems.Add(item);
                    break;
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
