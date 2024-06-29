using TMPro;
using UnityEngine;

public class TextStatAttackRate : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public void SetText(float value)
    {
        text.text = value.ToString();
    }
}
