using TMPro;
using UnityEngine;

public class TextStatAttackRange : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public void SetText(float valueMin, float valueMax)
    {
        text.text = valueMin.ToString() + " - " + valueMax.ToString();
    }
}
