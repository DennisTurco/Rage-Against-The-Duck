using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class KeybindsReadOnlyUI : MonoBehaviour
{
    [Serializable]
    public struct BindingRow
    {
        public string actionName;
        public KeyCode key;
    }

    [Header("UI")]
    [SerializeField] private RectTransform contentRoot;
    [SerializeField] private KeybindRowUI rowPrefab;
    [SerializeField] private ScrollRect scrollRect; // AGGIUNGI riferimento alla ScrollRect

    [Header("Bindings (read-only)")]
    [SerializeField] private List<BindingRow> bindings = new();

    [Header("Scroll Settings")]
    [SerializeField] private float scrollSensitivity = 20f; // Aumenta per scroll più veloce

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
            // Disabilita movimento orizzontale
            scrollRect.horizontal = false;
            scrollRect.vertical = true;

            // Aumenta sensibilità scroll
            scrollRect.scrollSensitivity = scrollSensitivity;

            // DISABILITA pan/drag
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
                var key = bindings[i].key;
                var keyLabel = (key == KeyCode.Escape) ? "ESC" : key.ToString();
                ui.Set(bindings[i].actionName, keyLabel);
            }
        }

        Debug.Log($"[KeybindsReadOnlyUI] Spawned rows: {bindings.Count} | Content children now: {contentRoot.childCount}");

        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentRoot);
    }
}
