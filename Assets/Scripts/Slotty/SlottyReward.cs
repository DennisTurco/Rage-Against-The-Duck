using UnityEngine;

[CreateAssetMenu]
public class SlottyReward : ScriptableObject
{
    public ItemName itemName; // Nome dell'oggetto
    public int rewardQuantity; // Quantità del premio
    public Sprite itemSprite; // Sprite dell'oggetto

    [Range(0f, 1f)]
    public float probability; // Probabilità di vincita, tra 0 e 1
}
