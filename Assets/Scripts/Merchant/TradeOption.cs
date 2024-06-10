using UnityEngine;

[CreateAssetMenu]
public class TradeOption : ScriptableObject
{
    public int coinCost;
    public int itemQuantity;

    [Range(0f, 1f)]
    public float probability = 1.0f;   // Default to 100% probability

    [Range(0f, 1f)]
    public float discount = 0.0f;      // Default to no discount

    public bool limited = false;       // Default to unlimited purchases
    public int limitTimesPurchase = 1;

    public ItemName itemName;
}