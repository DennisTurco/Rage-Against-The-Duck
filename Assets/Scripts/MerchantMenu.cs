using UnityEngine;

public class MerchantMenu : MonoBehaviour
{
    public void BombTrade()
    {
        int bombCost = 3;

        // Check if the player has enough coins
        if (GameManager.Instance.coins >= bombCost)
        {
            // Deduct coins and add a bomb
            ItemCoin.UseItemCoin(bombCost);
            new ItemBomb().CollectItemBomb();

            Debug.Log("The player traded 3 coins for a bomb.");
            Debug.Log("Coins left: " + GameManager.Instance.coins);
            Debug.Log("Bombs now: " + GameManager.Instance.bombs);
        }
        else
        {
            // Inform the player that they don't have enough coins
            Debug.Log("Not enough coins to trade for a bomb.");
        }
    }
}
