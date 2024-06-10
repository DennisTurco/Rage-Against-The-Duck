using UnityEngine;
using System.Collections.Generic;

public class MerchantMenu : MonoBehaviour
{
    public List<TradeOptions> tradeOptions;

    private void OnEnable()
    {
        Merchant.UpdateTrade += UpdateTradeButtons;
    }

    private void OnDisable()
    {
        Merchant.UpdateTrade -= UpdateTradeButtons;
    }

    public void ExecuteTradeById(int id)
    {
        TradeOptions tradeOption = tradeOptions.Find(option => option.id == id);
        if (tradeOption != null)
        {
            tradeOption.ExecuteTrade(GameManager.Instance.coins, () =>
            {
                ItemCoin.UseItemCoin(tradeOption.coinCost);

                for (int i = 0; i < tradeOption.itemQuantity; i++)
                {
                    Instantiate(tradeOption.itemPrefab);
                }

                Debug.Log($"The player traded {tradeOption.coinCost} coins for {tradeOption.itemQuantity} items.");
                Debug.Log("Coins left: " + GameManager.Instance.coins);

                UpdateTradeButtons();
            });
        }
    }

    public void UpdateTradeButtons()
    {
        foreach (TradeOptions option in tradeOptions)
        {
            option.UpdateButtonState(GameManager.Instance.coins);
        }
    }
}