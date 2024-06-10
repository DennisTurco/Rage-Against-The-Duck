using UnityEngine;

[CreateAssetMenu]
public class TradeOption : ScriptableObject
{
    public int id;
    public int coinCost;
    public int itemQuantity;
    public float probability;   // TODO: set
    public float discount;      // TODO: set
    public ItemName ItemName;
}