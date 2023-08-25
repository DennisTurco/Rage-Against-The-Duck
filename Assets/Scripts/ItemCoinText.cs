using TMPro;
using UnityEngine;

public class ItemCoinText : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    private int coinCount;

    private void OnEnable()
    {
        ItemCoin.OnCoinCollected += IncrementCoinCount;
    }

    private void OnDisable()
    {
        ItemCoin.OnCoinCollected -= IncrementCoinCount;
    }

    private void IncrementCoinCount()
    {
        coinCount++;
        coinText.text = $"Coins: {coinCount}";
    }
}
