using System;
using Unity.VisualScripting;
using UnityEngine;

public class Items : MonoBehaviour, ICollectible
{
    [SerializeField] private string item;

    private ItemCoin itemCoin;
    private ItemBomb itemBomb;
    private ItemKey itemKey;
    private ItemHeart itemHeart;
    private ItemMinion itemMinion;

    private void Start()
    {
        item = this.name;
        if (item.Equals(ItemName.Coin.ToString())) itemCoin = gameObject.AddComponent<ItemCoin>();
        else if (item.Equals(ItemName.Bomb.ToString())) itemBomb = gameObject.AddComponent<ItemBomb>();
        else if (item.Equals(ItemName.Key.ToString())) itemKey = gameObject.AddComponent<ItemKey>();
        else if (item.Equals(ItemName.FullHeart.ToString())) itemHeart = gameObject.AddComponent<ItemHeart>();
        else if (item.Equals(ItemName.Minion.ToString())) itemMinion = gameObject.AddComponent<ItemMinion>();
    }

    public void Collect()
    {
        Debug.Log("Item collected: " + item);

        if (item.Equals(ItemName.Coin.ToString()))
        {
            itemCoin.CollectItemCoin();
        }
        else if (item.Equals(ItemName.Bomb.ToString()))
        {
            itemBomb.CollectItemBomb();
        }
        else if (item.Equals(ItemName.Key.ToString()))
        {
            itemKey.CollectItemKey();
        }
        else if (item.Equals(ItemName.FullHeart.ToString()))
        {
            if (!itemHeart.CanCollectHeart())
                return;

            itemHeart.CollectItemHeart();
        }
        else if (item.Equals(ItemName.Minion.ToString()))
        {
            itemMinion.CollectItemMinion();
        }
        else
        {
            throw new ArgumentException("Item doesn't exist");
        }

        GameManager.Instance.ShowFloatingText("+" + 1 + " " + item, 25, Color.yellow, transform.position, Vector3.up * 100, 1.5f);
        Destroy(gameObject);
    }

}