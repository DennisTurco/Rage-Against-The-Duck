using TMPro;
using UnityEngine;

public class ItemKeyText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI keyText;

    private void OnEnable()
    {
        ItemKey.UpdateKeyText += SetKeyText;
    }

    private void OnDisable()
    {
        ItemKey.UpdateKeyText -= SetKeyText;
    }

    private void SetKeyText()
    {
        keyText.text = $"x {GameManager.Instance.keys}";
    }
}