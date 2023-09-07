using TMPro;
using UnityEngine;

public class ItemKeyText : MonoBehaviour
{
    public TextMeshProUGUI keyText;
    int keyCount;

    private void OnEnable()
    {
        ItemKey.OnKeyCollected += IncrementKeyCount;
    }

    private void OnDisable()
    {
        ItemKey.OnKeyCollected -= IncrementKeyCount;
    }

    private void IncrementKeyCount()
    {
        keyCount++;
        keyText.text = $"x {keyCount}";
    }
}