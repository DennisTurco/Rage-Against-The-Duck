using UnityEngine.UI;
using UnityEngine;

public class FloatingText
{
    private const float FadeOutTime = 0.3f;

    public bool active;
    public GameObject go;
    public RectTransform rect;
    public Text txt;
    public float duration;
    public float lastShown;

    public void Show()
    {
        active = true;
        lastShown = Time.time;
        go.SetActive(active);
    }

    public void Hide()
    {
        active = false;
        go.SetActive(active);
    }

    public void UpdateFloatingText()
    {
        if (!active) return;

        float elapsed = Time.time - lastShown;
        if (elapsed > duration)
        {
            Hide();
            return;
        }

        // Fade out over the last bit of its life instead of disappearing abruptly.
        float remaining = duration - elapsed;
        if (remaining < FadeOutTime)
        {
            Color color = txt.color;
            color.a = remaining / FadeOutTime;
            txt.color = color;
        }
    }
}
