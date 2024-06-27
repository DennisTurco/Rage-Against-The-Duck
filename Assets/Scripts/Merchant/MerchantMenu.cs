using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class MerchantMenu : MonoBehaviour
{
    [SerializeField] private List<TradeOption> tradeOptions;
    [SerializeField] private List<GameObject> trades;

    private List<TextMeshProUGUI> texts;
    private List<Button> tradeButtons;
    private Dictionary<TradeOption, int> selectedOptions = new Dictionary<TradeOption, int>(); // the second value rappresent the purchaseCount for that option

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

        if (tradeOptions.Count < 3)
        {
            Debug.LogError("Not enough trade options to select 3!");
            return;
        }

        SelectTradeOptions();

        if (selectedOptions.Count != 3)
        {
            Debug.LogError("Failed to select 3 trade options!");
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

    private void SelectTradeOptions()
    {
        selectedOptions.Clear();
        List<TradeOption> availableOptions = new List<TradeOption>(tradeOptions);

        for (int i = 0; i < 3; i++)
        {
            TradeOption selectedOption = GetRandomOptionBasedOnProbability(availableOptions);
            if (selectedOption != null)
            {
                Debug.Log($"Selected Trade Option: {selectedOption.itemName} with probability {selectedOption.probability}");
                selectedOptions.Add(selectedOption, 0); // Inizializza il conteggio degli acquisti a 0
                availableOptions.Remove(selectedOption);
            }
        }

        Debug.Log($"Selected Options Count: {selectedOptions.Count}");
    }

    private TradeOption GetRandomOptionBasedOnProbability(List<TradeOption> options)
    {
        float totalProbability = 0f;
        foreach (TradeOption option in options)
        {
            totalProbability += option.probability;
        }

        float randomPoint = Random.value * totalProbability;
        Debug.Log($"Random Point: {randomPoint} / Total Probability: {totalProbability}");

        foreach (TradeOption option in options)
        {
            if (randomPoint < option.probability)
            {
                return option;
            }
            else
            {
                randomPoint -= option.probability;
            }
        }

        return null;
    }

    public void ExecuteTradeByIndex(int index)
    {
        if (index < 0 || index >= selectedOptions.Count)
        {
            return;
        }

        TradeOption tradeOption = selectedOptions.Keys.ElementAt(index);
        ExecuteTrade(tradeOption);
    }

    private void ExecuteTrade(TradeOption tradeOption)
    {
        int playerCoins = GameManager.Instance.coins;
        int effectiveCost = Mathf.RoundToInt(tradeOption.coinCost * (1 - tradeOption.discount)); // Apply discount

        if (selectedOptions.TryGetValue(tradeOption, out int purchaseCount)) // Ottieni il conteggio degli acquisti per l'opzione selezionata
        {
            if (tradeOption.limited && purchaseCount >= tradeOption.limitTimesPurchase)
            {
                Debug.Log("Purchase limit reached for this item.");
                return;
            }

            if (playerCoins >= effectiveCost)
            {
                ItemCoin.UseItemCoin(effectiveCost);

                switch (tradeOption.itemName)
                {
                    case ItemName.Coin:
                        ItemCoin.CollectItemCoin(tradeOption.itemQuantity);
                        break;
                    case ItemName.Bomb:
                        ItemBomb.CollectItemBomb(tradeOption.itemQuantity);
                        break;
                    case ItemName.MinionOrbiter:
                        ItemMinion.CollectItemMinionOrbiter(tradeOption.itemQuantity);
                        break;
                    case ItemName.MinionFollower:
                        ItemMinion.CollectItemMinionFollower(tradeOption.itemQuantity);
                        break;
                    case ItemName.FullHeart:
                        ItemHeart.CollectItemHeart(tradeOption.itemQuantity);
                        break;
                    case ItemName.HalfHeart:
                        break;
                    case ItemName.Key:
                        break;
                    default:
                        throw new System.Exception("ItemName '" + tradeOption.itemName.ToString() + "' for ExecuteTrade not valid");
                }

                if (tradeOption.limited)
                {
                    selectedOptions[tradeOption]++; // Incrementa il conteggio degli acquisti per l'opzione selezionata
                }

                Debug.Log($"The player traded {effectiveCost} coins for {tradeOption.itemQuantity} items.");
                Debug.Log("Coins left: " + GameManager.Instance.coins);
                UpdateTradeButtons();
            }
            else
            {
                Debug.Log("Not enough coins or probability check failed.");
            }
        }
    }

    public void UpdateTradeButtons()
    {
        if (selectedOptions.Count != texts.Count || selectedOptions.Count != tradeButtons.Count)
        {
            Debug.LogError("Mismatch in list counts, cannot update trade buttons!");
            return;
        }

        int i = 0;
        foreach (var kvp in selectedOptions)
        {
            TradeOption option = kvp.Key;
            int purchaseCount = kvp.Value;
            int effectiveCost = Mathf.RoundToInt(option.coinCost * (1 - option.discount));
            string limitText = option.limited ? $" (Max: {option.limitTimesPurchase - purchaseCount})" : "";
            texts[i].text = $"{effectiveCost} Coins for {option.itemQuantity} {option.itemName.ToString()}{limitText}";
            UpdateButtonState(option, tradeButtons[i], GameManager.Instance.coins);
            i++;
        }
    }

    private void UpdateButtonState(TradeOption option, Button button, int playerCoins)
    {
        if (button != null)
        {
            int effectiveCost = Mathf.RoundToInt(option.coinCost * (1 - option.discount));
            bool canPurchase = playerCoins >= effectiveCost && (!option.limited || selectedOptions[option] < option.limitTimesPurchase);
            button.image.color = canPurchase ? Color.white : Color.gray;
            button.interactable = canPurchase;
        }
    }
}
