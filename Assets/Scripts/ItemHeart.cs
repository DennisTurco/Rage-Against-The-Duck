using System;
using UnityEngine;

public class ItemHeart : MonoBehaviour
{
    public static event Action OnHeartCollected;

    public void CollectItemHeart()
    {
        OnHeartCollected?.Invoke();
        GameManager.Instance.AddHeart();
    }
}
