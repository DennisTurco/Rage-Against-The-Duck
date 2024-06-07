using System;
using Unity.VisualScripting;
using UnityEngine;

public class Items : MonoBehaviour, ICollectible
{
    [SerializeField] private string ItemName;

    private ItemCoin itemCoin;
    private ItemBomb itemBomb;
    private ItemHeart itemHeart;
    private ItemMinion itemMinion;

    private void Start()
    {
        ItemName = this.name;
        if (ItemName.Equals("coin")) itemCoin = gameObject.AddComponent<ItemCoin>();
        else if (ItemName.Equals("bomb")) itemBomb = gameObject.AddComponent<ItemBomb>();
        else if (ItemName.Equals("heart")) itemHeart = gameObject.AddComponent<ItemHeart>();
        else if (ItemName.Equals("minion")) itemMinion = gameObject.AddComponent<ItemMinion>();
    }

    public void Collect()
    {
        Debug.Log("Item collected: " + ItemName);

        if (ItemName.Equals("coin"))
        {
            itemCoin.CollectItemCoin();
        }
        else if (ItemName.Equals("bomb"))
        {
            itemBomb.CollectItemBomb();
        }
        else if (ItemName.Equals("heart"))
        {
            if (!itemHeart.CanCollectHeart())
                return;

            itemHeart.CollectItemHeart();
        }
        else if (ItemName.Equals("minion"))
        {
            itemMinion.CollectItemMinion();
        }
        else
        {
            throw new ArgumentException("Item doesn't exist");
        }

        GameManager.Instance.ShowFloatingText("+" + 1 + " " + ItemName, 25, Color.yellow, transform.position, Vector3.up * 100, 1.5f);
        Destroy(gameObject);
    }

}