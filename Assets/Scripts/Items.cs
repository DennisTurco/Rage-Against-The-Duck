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
        //GameManager.instance.ShowText("+" + 1 + " " + ItemName, 25, Color.yellow, transform.position, Vector3.up * 50, 3.0f);

        // collected a coin
        if (ItemName.Equals("coin")) itemCoin.CollectItemCoin();

        // collected a bomb
        else if (ItemName.Equals("bomb")) itemBomb.CollectItemBomb();

        else throw new ArgumentException("Item doesn't exist");

        Destroy(gameObject);
    }

}
