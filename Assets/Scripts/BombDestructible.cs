using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDestructableObject : MonoBehaviour
{
    [SerializeField] private int bombsToDestroy = 2;

    [SerializeField] private GenericAnimation idleAnimation;
    [SerializeField] private float destructionAnimationFrameDelay = 0.15f;
    [SerializeField] private Sprite[] destructionAnimationFrames;

    [SerializeField] private GameObject droppedItemPrefab;
    [SerializeField] private List<LootSpawn> lootList = new List<LootSpawn>();
    [SerializeField] private int maxNumberOfDrops = 3;

    [SerializeField] private Collider2D entityCollider;
    [SerializeField] private Collider2D destructionCollider;

    private SpriteRenderer spriteRenderer;
    private bool isDestroyed = false;
    private int currentBombHits = 0;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (entityCollider != null) entityCollider.enabled = true;
        if (destructionCollider != null) destructionCollider.enabled = true;
    }

    public void TakeBombHit(int amount = 1)
    {
        if (isDestroyed) return;

        currentBombHits += amount;
        if (currentBombHits >= bombsToDestroy)
            DestroyObject();
    }

    public void DestroyObject()
    {
        if (isDestroyed) return;
        isDestroyed = true;

        if (idleAnimation != null)
            idleAnimation.enabled = false;

        StartCoroutine(PlayDestructionAnimation());
    }

    private IEnumerator PlayDestructionAnimation()
    {
        if (destructionAnimationFrames != null && destructionAnimationFrames.Length > 0)
        {
            for (int i = 0; i < destructionAnimationFrames.Length; i++)
            {
                spriteRenderer.sprite = destructionAnimationFrames[i];
                yield return new WaitForSeconds(destructionAnimationFrameDelay);
            }

            spriteRenderer.sprite = destructionAnimationFrames[destructionAnimationFrames.Length - 1];
        }

        if (entityCollider != null) entityCollider.enabled = false;
        if (destructionCollider != null) destructionCollider.enabled = false;

        InstantiateLootSpawn(transform.position);
        Destroy(gameObject);
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

        if (droppedItems.Count > 0) return droppedItems;
        return null;
    }
}
