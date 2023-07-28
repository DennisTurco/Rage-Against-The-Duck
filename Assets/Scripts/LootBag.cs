using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    public GameObject droppedItemPrefab;
    public List<LootSpawn> lootList = new List<LootSpawn>();

    private LootSpawn GetDroppedItem()
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
            LootSpawn droppedItem = possibleLoot[Random.Range(0, possibleLoot.Count)];
            return droppedItem;
        }

        Debug.Log("No loot dropped");
        return null;
    }

    public void InstantiateLootSpawn(Vector3 spawnPosition)
    {
        LootSpawn droppedItem = GetDroppedItem();

        if (droppedItem != null)
        {
            GameObject lootGameObject = Instantiate(droppedItemPrefab, spawnPosition, Quaternion.identity);
            lootGameObject.GetComponent<SpriteRenderer>().sprite = droppedItem.lootSprite;

            float dropForce = 300f;
            Vector2 dropDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            lootGameObject.GetComponent<Rigidbody2D>().AddForce(dropDirection * dropForce, ForceMode2D.Impulse);
        }
    }

}
