using System;
using UnityEngine;

public class ItemKey : MonoBehaviour
{
    public static event Action OnKeyCollected;

    public void CollectItemKey()
    {
        OnKeyCollected?.Invoke();
        GameManager.Instance.keys++;
    }
}
