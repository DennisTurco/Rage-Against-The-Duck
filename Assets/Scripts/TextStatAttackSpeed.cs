using TMPro;
using UnityEngine;

public class TextStatAttackSpeed : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    private float value;

    public void SetText(float value)
    {
        text.text = value.ToString();
    }
}
