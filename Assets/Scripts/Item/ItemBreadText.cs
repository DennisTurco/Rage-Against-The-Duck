using System;
using TMPro;
using UnityEngine;

public class ItemBreadText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI breadText;

    private void OnEnable()
    {
        ItemBread.UpdateBreadText += SetBreadText;
    }

    private void OnDisable()
    {
        ItemBread.UpdateBreadText -= SetBreadText;
    }

    private void SetBreadText()
    {
        breadText.text = $"x {GameManager.Instance.bread}";
    }
}
