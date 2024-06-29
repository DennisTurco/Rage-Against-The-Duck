using TMPro;
using UnityEngine;

public class TextStatMovementSpeed : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public void SetText(float value)
    {
        text.text = value.ToString();
    }
}
