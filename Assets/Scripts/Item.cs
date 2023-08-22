using System;
using Unity.VisualScripting;
using UnityEngine;

public class Items : MonoBehaviour, ICollectible
{
    [SerializeField] private string ItemName; 

    private ItemCoin itemCoin;
    private ItemBomb itemBomb;

    private void Start()
    {
        ItemName = this.name;
        if (ItemName.Equals("coin")) itemCoin = gameObject.AddComponent<ItemCoin>();
        else if (ItemName.Equals("bomb")) itemBomb = gameObject.AddComponent<ItemBomb>();
    }

    public void Collect()
    {
        Debug.Log("Item collected: " + ItemName);

        // collected a coin
        if (ItemName.Equals("coin")) itemCoin.CollectItemCoin();

        // collected a bomb
        else if (ItemName.Equals("bomb")) itemBomb.CollectItemBomb();

        else throw new ArgumentException("Item doesn't exist");

        Destroy(gameObject);
    }

}
