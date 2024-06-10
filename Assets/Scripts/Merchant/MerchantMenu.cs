using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class MerchantMenu : MonoBehaviour
{
    [SerializeField] private List<TradeOption> tradeOptions;
    [SerializeField] private List<GameObject> trades;

    private List<TextMeshProUGUI> texts;
    private List<Button> tradeButtons;

    private void Awake()
    {
        tradeButtons = new List<Button>();
        texts = new List<TextMeshProUGUI>();

        for (int i = 0; i < trades.Count; i++)
        {
            Transform buttonTransform = trades[i].transform.Find("TradeButton");
            if (buttonTransform != null)
            {
                Button tradeButton = buttonTransform.GetComponent<Button>();
                if (tradeButton != null)
                {
                    tradeButtons.Add(tradeButton);
                }
            }

            Transform textTransform = trades[i].transform.Find("Description");
            if (textTransform != null)
            {
                TextMeshProUGUI text = textTransform.GetComponent<TextMeshProUGUI>();
                if (text != null)
                {
                    texts.Add(text);
                }
            }
        }

        if (tradeOptions.Count != texts.Count)
        {
            Debug.LogError("Mismatch between the number of trade options and text components!");
        }

        if (tradeOptions.Count != tradeButtons.Count)
        {
            Debug.LogError("Mismatch between the number of trade options and button components!");
        }
    }

    private void OnEnable()
    {
        Merchant.UpdateTrade += UpdateTradeButtons;
    }

    private void OnDisable()
    {
        Merchant.UpdateTrade -= UpdateTradeButtons;
    }

    public void ExecuteTradeByIndex(int index)
    {
        if (index < 0 || index >= tradeOptions.Count)
        {
            return;
        }

        TradeOption tradeOption = tradeOptions[index];
        ExecuteTrade(tradeOption);
    }

    private void ExecuteTrade(TradeOption tradeOption)
    {
        int playerCoins = GameManager.Instance.coins;
        if (playerCoins >= tradeOption.coinCost)
        {
            ItemCoin.UseItemCoin(tradeOption.coinCost);
            switch (tradeOption.ItemName)
            {
                case ItemName.Coin:
                    ItemCoin.CollectItemCoin(tradeOption.itemQuantity);
                    break;
                case ItemName.Bomb:
                    ItemBomb.CollectItemBomb(tradeOption.itemQuantity);
                    break;
                case ItemName.Minion:
                    ItemMinion.CollectItemMinion(tradeOption.itemQuantity);
                    break;
                case ItemName.FullHeart:
                    ItemHeart.CollectItemHeart(tradeOption.itemQuantity);
                    break;
                case ItemName.HalfHeart:
                    break;
                case ItemName.Key:
                    break;
                default:
                    throw new System.Exception("ItemName '" + tradeOption.ItemName.ToString() + "' for ExecuteTrade not valid");
            }
            
            Debug.Log($"The player traded {tradeOption.coinCost} coins for {tradeOption.itemQuantity} items.");
            Debug.Log("Coins left: " + GameManager.Instance.coins);
            UpdateTradeButtons();
        }
    }

    public void UpdateTradeButtons()
    {
        // Check if lists are of the same length before proceeding
        if (tradeOptions.Count != texts.Count || tradeOptions.Count != tradeButtons.Count)
        {
            Debug.LogError("Mismatch in list counts, cannot update trade buttons!");
            return;
        }

        for (int i = 0; i < tradeOptions.Count; i++)
        {
            texts[i].text = $"{tradeOptions[i].coinCost} Coins for {tradeOptions[i].itemQuantity} {tradeOptions[i].ItemName.ToString()}";
        }

        int playerCoins = GameManager.Instance.coins;
        for (int i = 0; i < tradeOptions.Count; i++)
        {
            TradeOption option = tradeOptions[i];
            Button button = tradeButtons[i];
            UpdateButtonState(option, button, playerCoins);
        }
    }

    private void UpdateButtonState(TradeOption option, Button button, int playerCoins)
    {
        if (button != null)
        {
            if (playerCoins >= option.coinCost)
            {
                button.image.color = Color.white;
                button.interactable = true;
            }
            else
            {
                button.image.color = Color.gray;
                button.interactable = false;
            }
        }
    }
}
