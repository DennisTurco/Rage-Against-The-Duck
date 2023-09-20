using TMPro;
using UnityEngine;

public class TextStatAttackRate : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    private float value;

    public void SetText(float value)
    {
        text.text = value.ToString();
    }
}
