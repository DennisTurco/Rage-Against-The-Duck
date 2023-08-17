using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemBombText : MonoBehaviour
{
    public TextMeshProUGUI bombText;
    int bombCount;

    private void OnEnable()
    {
        ItemCoin.OnCoinCollected += IncrementBombCount;
    }

    private void OnDisable()
    {
        ItemCoin.OnCoinCollected -= IncrementBombCount;
    }

    private void IncrementBombCount()
    {
        bombCount++;
        bombText.text = $"Bombs: {bombCount}";
    }
}
