using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextManager : MonoBehaviour
{
    public GameObject textContainer;
    public GameObject textPrefab;

    [Header("Stack layout (bottom-left corner)")]
    [SerializeField] private float marginX = 20f;
    [SerializeField] private float marginY = 20f;
    [SerializeField] private float lineHeight = 28f;

    private List<FloatingText> pool = new List<FloatingText>();
    private List<FloatingText> activeTexts = new List<FloatingText>();

    public void Show(string message, int fontSize, Color color, float duration)
    {
        FloatingText floatingText = GetFloatingText();

        floatingText.txt.text = message;
        floatingText.txt.fontSize = fontSize;
        floatingText.txt.color = color;
        floatingText.duration = duration;

        floatingText.Show();
        activeTexts.Add(floatingText);
    }

    private void Update()
    {
        for (int i = activeTexts.Count - 1; i >= 0; i--)
        {
            FloatingText text = activeTexts[i];
            text.UpdateFloatingText();

            if (!text.active)
            {
                activeTexts.RemoveAt(i);
            }
        }

        // Stack active messages bottom-up in the corner, oldest at the bottom, so they never overlap.
        for (int i = 0; i < activeTexts.Count; i++)
        {
            activeTexts[i].rect.anchoredPosition = new Vector2(marginX, marginY + i * lineHeight);
        }
    }

    private FloatingText GetFloatingText()
    {
        FloatingText txt = pool.Find(t => !t.active);

        if (txt == null)
        {
            txt = new FloatingText();
            txt.go = Instantiate(textPrefab);
            txt.go.transform.SetParent(textContainer.transform, false);
            txt.txt = txt.go.GetComponent<Text>();
            txt.rect = txt.go.GetComponent<RectTransform>();

            // Anchor to the bottom-left corner instead of following the world-space item position.
            txt.rect.anchorMin = new Vector2(0, 0);
            txt.rect.anchorMax = new Vector2(0, 0);
            txt.rect.pivot = new Vector2(0, 0);
            txt.txt.alignment = TextAnchor.LowerLeft;

            pool.Add(txt);
        }

        return txt;
    }
}
