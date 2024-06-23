using System;
using UnityEngine;

public class ItemHeart : MonoBehaviour
{
    public static event Action OnHeartCollected;

    public void CollectItemHeart()
    {
        OnHeartCollected?.Invoke();
    }

    public static void CollectItemHeart(int hearts)
    {
        for (int i = 0; i < hearts; ++i) {
            OnHeartCollected?.Invoke();
        }
    }

    public bool CanCollectHeart()
    {
        return Heart.CanCollectHearts();
    }
}