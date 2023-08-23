using UnityEngine.UI;
using UnityEngine;

public class FloatingText
{
    public bool active;
    public GameObject go;
    public Text txt;
    public Vector3 motion;
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
        if(!active) return;

        if (Time.time - lastShown > duration) Hide(); // the duration has to be long enough

        go.transform.position += motion * Time.deltaTime;
    }
}
