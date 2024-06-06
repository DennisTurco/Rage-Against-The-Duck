using System;
using TMPro;
using UnityEngine;

public class ItemCoinText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;

    private void OnEnable()
    {
        ItemCoin.UpdateCoinText += SetCoinText;
    }

    private void OnDisable()
    {
        ItemCoin.UpdateCoinText -= SetCoinText;
    }

    private void SetCoinText()
    {
        coinText.text = $"x {GameManager.Instance.coins}";
    }
}
