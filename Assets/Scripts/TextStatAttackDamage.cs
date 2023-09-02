using TMPro;
using UnityEngine;

public class TextStatAttackDamage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    private float valueMin;
    private float valueMax;

    public void SetText(float valueMin, float valueMax)
    {
        text.text = valueMin.ToString() + " - " + valueMax.ToString();
    }
}
