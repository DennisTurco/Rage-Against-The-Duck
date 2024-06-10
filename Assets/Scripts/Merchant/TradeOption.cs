using UnityEngine;

[CreateAssetMenu]
public class TradeOption : ScriptableObject
{
    public int coinCost;
    public int itemQuantity;
    public float probability;   // TODO: set
    public float discount;      // TODO: set
    public bool limited;            // TODO: set
    public int limitTimesPurchese;  // TODO: set
    public ItemName ItemName;
}