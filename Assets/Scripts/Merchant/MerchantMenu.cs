using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

        foreach (var trade in trades)
        {
            Transform buttonTransform = trade.transform.Find("TradeButton");
            if (buttonTransform != null)
            {
                Button tradeButton = buttonTransform.GetComponent<Button>();
                if (tradeButton != null)
                {
                    tradeButtons.Add(tradeButton);
                }
            }

            Transform textTransform = trade.transform.Find("Description");
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

    public void ExecuteTradeById(int id)
    {
        TradeOption tradeOption = tradeOptions.Find(option => option.id == id);
        if (tradeOption != null)
        {
            ExecuteTrade(tradeOption);
        }
    }

    private void ExecuteTrade(TradeOption tradeOption)
    {
        int playerCoins = GameManager.Instance.coins;
        if (playerCoins >= tradeOption.coinCost)
        {
            ItemCoin.UseItemCoin(tradeOption.coinCost);
            for (int i = 0; i < tradeOption.itemQuantity; i++)
            {
                switch(tradeOption.ItemName) {
                    case ItemName.Coin:
                        new ItemCoin().CollectItemCoin();
                        break;
                    case ItemName.Bomb:
                        new ItemBomb().CollectItemBomb();
                        break;
                    case ItemName.Minion:
                        new ItemMinion().CollectItemMinion();
                        break;
                    case ItemName.FullHeart:
                        new ItemHeart().CollectItemHeart();
                        break;
                    case ItemName.HalfHeart:
                        break;
                    case ItemName.Key:
                        break;
                    default:
                        throw new System.Exception("ItemName '" + tradeOption.ItemName.ToString() + "' for ExecuteTrade not valid");

                }
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
            texts[i].text = $"trade x {tradeOptions[i].coinCost} coins for x {tradeOptions[i].itemQuantity} {tradeOptions[i].ItemName.ToString()}";
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