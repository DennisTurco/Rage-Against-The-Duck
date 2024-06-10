using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TradeOptions
{
    public int id;
    public Button tradeButton;
    public int coinCost;
    public int itemQuantity;
    public GameObject itemPrefab;
    
    public static Color normalColor = Color.white;
    public static Color disabledColor = Color.gray;

    public void UpdateButtonState(int playerCoins)
    {
        if (tradeButton != null)
        {
            if (playerCoins >= coinCost)
            {
                tradeButton.image.color = normalColor;
                tradeButton.interactable = true;
            }
            else
            {
                tradeButton.image.color = disabledColor;
                tradeButton.interactable = false;
            }
        }
    }

    public void ExecuteTrade(int playerCoins, System.Action onSuccess)
    {
        if (playerCoins >= coinCost)
        {
            onSuccess.Invoke();
        }
        else
        {
            UpdateButtonState(playerCoins);
        }
    }
}
