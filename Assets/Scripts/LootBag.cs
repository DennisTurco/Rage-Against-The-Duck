using System.Collections.Generic;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    public GameObject droppedItemPrefab;
    public List<LootSpawn> lootList = new List<LootSpawn>();

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

        // founded a possible loot
        if (possibleLoot.Count > 0)
        {
            //LootSpawn droppedItem = possibleLoot[Random.Range(0, possibleLoot.Count)];
            return possibleLoot;
        }

        Debug.Log("No loot dropped");
        return null;
    }

    public void InstantiateLootSpawn(Vector3 spawnPosition)
    {
        List<LootSpawn> droppedItems = GetDroppedItem();

        if (droppedItems.Count == 0) return;

        foreach (LootSpawn droppedItem in droppedItems)
        {
            GameObject lootGameObject = Instantiate(droppedItemPrefab, spawnPosition, Quaternion.identity);
            lootGameObject.GetComponent<SpriteRenderer>().sprite = droppedItem.lootSprite;

            // set custom name to prefab
            lootGameObject.name = droppedItem.lootName;

            //float dropForce = 300f;
            //Vector2 dropDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            //lootGameObject.GetComponent<Rigidbody2D>().AddForce(dropDirection * dropForce, ForceMode2D.Impulse);
        }
    }

}
