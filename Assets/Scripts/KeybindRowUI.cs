using TMPro;
using UnityEngine;

public class KeybindRowUI : MonoBehaviour
{
    [SerializeField] private TMP_Text actionLabel;
    [SerializeField] private TMP_Text keyText;

    public void Set(string action, string key)
    {
        if (actionLabel != null) actionLabel.text = action;
        if (keyText != null) keyText.text = key;
    }
}
