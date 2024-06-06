using TMPro;
using UnityEngine;

public class ItemBombText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bombText;

    private void OnEnable()
    {
        ItemBomb.UpdateBombText += SetBombText;
    }

    private void OnDisable()
    {
        ItemBomb.UpdateBombText -= SetBombText;
    }

    private void SetBombText()
    {
        bombText.text = $"x {GameManager.Instance.bombs}";
    }
}