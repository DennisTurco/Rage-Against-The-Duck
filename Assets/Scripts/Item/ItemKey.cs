using System;
using UnityEngine;

public class ItemKey : MonoBehaviour
{
    public static event Action UpdateKeyText;

    public void CollectItemKey()
    {
        GameManager.Instance.keys++;
        UpdateKeyText?.Invoke();
    }

    public static void CollectItemKey(int key)
    {
        GameManager.Instance.keys += key;
        UpdateKeyText?.Invoke();
    }

    public static void UseItemKey()
    {
        GameManager.Instance.keys--;
        UpdateKeyText?.Invoke();
    }
}