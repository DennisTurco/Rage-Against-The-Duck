using System;
using Unity.VisualScripting;
using UnityEngine;

public class Items : MonoBehaviour, ICollectible
{
    [SerializeField] private string ItemName; 

    private ItemCoin itemCoin;
    private ItemBomb itemBomb;
    private ItemHeart itemHeart;

    private void Start()
    {
        ItemName = this.name;
        if (ItemName.Equals("coin")) itemCoin = gameObject.AddComponent<ItemCoin>();
        else if (ItemName.Equals("bomb")) itemBomb = gameObject.AddComponent<ItemBomb>();
        else if (ItemName.Equals("heart")) itemHeart = gameObject.AddComponent<ItemHeart>();
    }

    public void Collect()
    {
        Debug.Log("Item collected: " + ItemName);
        GameManager.Instance.ShowFloatingText("+" + 1 + " " + ItemName, 25, Color.yellow, transform.position, Vector3.up * 100, 1.5f);

        // collected a coin
        if (ItemName.Equals("coin")) itemCoin.CollectItemCoin();

        // collected a bomb
        else if (ItemName.Equals("bomb")) itemBomb.CollectItemBomb();

        // collected a heart //TODO: block collecting if health bar is full
        else if (ItemName.Equals("heart")) itemHeart.CollectItemHeart();

        else throw new ArgumentException("Item doesn't exist");

        Destroy(gameObject);
    }

}
