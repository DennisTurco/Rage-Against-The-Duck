using TMPro;
using UnityEngine;

public class ItemBombText : MonoBehaviour
{
    public TextMeshProUGUI bombText;
    int bombCount;

    private void OnEnable()
    {
        ItemBomb.OnBombCollected += IncrementBombCount;
    }

    private void OnDisable()
    {
        ItemBomb.OnBombCollected -= IncrementBombCount;
    }

    private void IncrementBombCount()
    {
        bombCount++;
        bombText.text = $"Bombs: {bombCount}";
    }
}
