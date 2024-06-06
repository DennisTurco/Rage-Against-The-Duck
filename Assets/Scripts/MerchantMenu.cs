using UnityEngine;

public class MerchantMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Optionally, you can perform some initialization here if needed
    }

    // Update is called once per frame
    void Update()
    {
        // Optionally, you can perform some updates here if needed
    }

    public void BombTrade()
    {
        int bombCost = 3;

        // Check if the player has enough coins
        if (GameManager.Instance.coins >= bombCost)
        {
            // Deduct coins and add a bomb
            GameManager.Instance.coins -= bombCost;
            GameManager.Instance.bombs += 1;

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
