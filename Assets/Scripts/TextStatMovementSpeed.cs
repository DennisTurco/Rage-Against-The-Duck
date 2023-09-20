using TMPro;
using UnityEngine;

public class TextStatMovementSpeed : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    private float value;

    public void SetText(float value)
    {
        text.text = value.ToString();
    }
}
