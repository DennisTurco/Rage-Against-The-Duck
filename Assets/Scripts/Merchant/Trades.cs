using UnityEngine;

// CreateAssetMenu allows to create new LootSpawn object into unity editor: right click -> Trade
[CreateAssetMenu]
public class Trades : ScriptableObject // ScriptableObject allows to work in the unity editor
{
    public int cost;
    public int quantity;
    public float discount;
    public ItemName item;
}

public enum ItemName
{
    Bomb,
    Minion,
    FullHeart,
    HalfHeart
}