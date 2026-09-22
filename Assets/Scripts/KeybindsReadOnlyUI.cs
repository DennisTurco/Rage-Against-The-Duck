using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeybindsReadOnlyUI : MonoBehaviour
{
    [Serializable]
    public struct BindingRow
    {
        public string actionName;
        public KeyCode key;
        [TextArea(1, 2)]
        public string manualText;
    }

    [Header("UI")]
    [SerializeField] private RectTransform contentRoot;
    [SerializeField] private KeybindRowUI rowPrefab;
    [SerializeField] private ScrollRect scrollRect;

    [Header("Bindings (not interactable)")]
    [SerializeField] private List<BindingRow> bindings = new();

    [Header("Scroll Settings")]
    [SerializeField] private float scrollSensitivity = 20f;

    private readonly List<GameObject> spawned = new();

    private void Start()
    {
        ConfigureScrollRect();
    }

    private void ConfigureScrollRect()
    {
        if (scrollRect == null)
        {
            scrollRect = GetComponentInChildren<ScrollRect>();
        }

        if (scrollRect != null)
        {
            scrollRect.horizontal = false;
            scrollRect.vertical = true;
            scrollRect.scrollSensitivity = scrollSensitivity;
            scrollRect.movementType = ScrollRect.MovementType.Clamped;
            scrollRect.inertia = false;
        }
    }

    public void Rebuild()
    {
        if (contentRoot == null || rowPrefab == null)
        {
            Debug.LogWarning("[KeybindsReadOnlyUI] Missing contentRoot or rowPrefab.");
            return;
        }

        // clear
        for (int i = 0; i < spawned.Count; i++)
            if (spawned[i] != null) Destroy(spawned[i]);
        spawned.Clear();

        // spawn
        for (int i = 0; i < bindings.Count; i++)
        {
            var go = Instantiate(rowPrefab.gameObject, contentRoot);
            spawned.Add(go);

            var ui = go.GetComponent<KeybindRowUI>();
            if (ui != null)
            {
                // USA manualText se presente, altrimenti KeyCode
                string displayText = string.IsNullOrEmpty(bindings[i].manualText)
                    ? bindings[i].key.ToString()
                    : bindings[i].manualText;

                ui.Set(bindings[i].actionName, displayText);
            }
        }

        Debug.Log($"[KeybindsReadOnlyUI] Spawned rows: {bindings.Count} | Content children now: {contentRoot.childCount}");

        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentRoot);
    }
}
