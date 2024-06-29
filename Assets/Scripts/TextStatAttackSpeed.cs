using TMPro;
using UnityEngine;

public class TextStatAttackSpeed : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public void SetText(float value)
    {
        text.text = value.ToString();
    }
}
