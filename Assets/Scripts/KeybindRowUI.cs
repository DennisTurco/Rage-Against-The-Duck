using System.Reflection;
using TMPro;
using UnityEditor.PackageManager.UI;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEditorInternal.ReorderableList;

public class KeybindRowUI : MonoBehaviour
{
    [SerializeField] private TMP_Text actionLabel;
    [SerializeField] private TMP_Text keyText;
    [SerializeField] private RectTransform keyBackground; // Il cerchietto/background

    [Header("Dynamic Size Settings")]
    [SerializeField] private float minWidth = 60f;
    [SerializeField] private float maxWidth = 200f;
    [SerializeField] private float padding = 20f; // Spazio ai lati del testo

    public void Set(string action, string key)
    {
        if (actionLabel != null) actionLabel.text = action;
        if (keyText != null)
        {
            keyText.text = key;
            AdjustKeyBackgroundSize();
        }
    }

    private void AdjustKeyBackgroundSize()
    {
        if (keyBackground == null || keyText == null) return;

        // Forza aggiornamento layout testo
        Canvas.ForceUpdateCanvases();
        keyText.ForceMeshUpdate();

        // Salva posizione SINISTRA del background
        float leftEdge = keyBackground.anchoredPosition.x - (keyBackground.rect.width * keyBackground.pivot.x);

        // Calcola larghezza necessaria
        float textWidth = keyText.preferredWidth;
        float targetWidth = Mathf.Clamp(textWidth + padding, minWidth, maxWidth);

        // Applica nuova larghezza
        keyBackground.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, targetWidth);

        // Ricalcola posizione X per mantenere bordo sinistro fisso
        float newX = leftEdge + (targetWidth * keyBackground.pivot.x);
        keyBackground.anchoredPosition = new Vector2(newX, keyBackground.anchoredPosition.y);

        // Forza refresh layout
        LayoutRebuilder.ForceRebuildLayoutImmediate(keyBackground);
        LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
    }
}