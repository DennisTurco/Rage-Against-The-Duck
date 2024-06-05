using System.Collections.Generic;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    [SerializeField] private GameObject droppedItemPrefab;
    [SerializeField] private List<LootSpawn> lootList = new List<LootSpawn>();

    private List<LootSpawn> GetDroppedItem()
    {
        int random = Random.Range(1, 101);  // random between 1 - 100
        List<LootSpawn> possibleLoot = new List<LootSpawn>();
        foreach (LootSpawn item in lootList)
        {
            if (random <= item.dropChance)
            {
                possibleLoot.Add(item);
            }
        }

        // found a possible loot
        if (possibleLoot.Count > 0)
        {
            return possibleLoot;
        }

        Debug.Log("No loot dropped");
        return null;
    }

    public void InstantiateLootSpawn(Vector3 spawnPosition)
    {
        List<LootSpawn> droppedItems = GetDroppedItem();

        if (droppedItems == null || droppedItems.Count == 0) return;

        foreach (LootSpawn droppedItem in droppedItems)
        {
            // Add a random offset to the spawn position
            Vector3 randomOffset = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
            Vector3 itemPosition = spawnPosition + randomOffset;
            GameObject lootGameObject = Instantiate(droppedItemPrefab, itemPosition, Quaternion.identity);
            lootGameObject.GetComponent<SpriteRenderer>().sprite = droppedItem.lootSprite;

            // Set custom name to prefab
            lootGameObject.name = droppedItem.lootName;

            // Apply initial force to the object to "launch" it
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
}
