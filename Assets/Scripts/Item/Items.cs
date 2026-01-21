using System;
using UnityEngine;

public class Items : MonoBehaviour, ICollectible
{
    [SerializeField] private string item;
    [SerializeField] private float levitationAmplitude = 0.1f; // Ampiezza del movimento
    [SerializeField] private float levitationFrequency = 0.2f; // Frequenza del movimento
    private Vector3 startPosition;

    private ItemCoin itemCoin;
    private ItemBomb itemBomb;
    private ItemKey itemKey;
    private ItemHeart itemHeart;
    private ItemBread itemBread;
    
    private void Start()
    {
        startPosition = transform.position;

        item = this.name;
        if (item.Equals(ItemName.Coin.ToString())) itemCoin = gameObject.AddComponent<ItemCoin>();
        else if (item.Equals(ItemName.Bomb.ToString())) itemBomb = gameObject.AddComponent<ItemBomb>();
        else if (item.Equals(ItemName.Key.ToString())) itemKey = gameObject.AddComponent<ItemKey>();
        else if (item.Equals(ItemName.FullHeart.ToString())) itemHeart = gameObject.AddComponent<ItemHeart>();
        else if (item.Equals(ItemName.Bread.ToString())) itemBread = gameObject.AddComponent<ItemBread>();

    }

    private void FixedUpdate()
    {
        float newY = startPosition.y + Mathf.PingPong(Time.time * levitationFrequency, levitationAmplitude) - (levitationAmplitude / 2);
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
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
        else if (item.Equals(ItemName.MinionOrbiter.ToString()))
        {
            GameManager.Instance.SpawnMinion(ItemName.MinionOrbiter);
        }
        else if (item.Equals(ItemName.MinionFollower.ToString()))
        {
            GameManager.Instance.SpawnMinion(ItemName.MinionFollower);
        }
        else
        {
            throw new ArgumentException("Item doesn't exist");
        }

        GameManager.Instance.ShowFloatingText("+" + 1 + " " + item, 25, Color.yellow, transform.position, Vector3.up * 100, 1.5f);
        Destroy(gameObject);
    }
}
