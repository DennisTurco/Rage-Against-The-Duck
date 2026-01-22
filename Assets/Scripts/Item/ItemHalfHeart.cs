using System;
using UnityEngine;

public class ItemHalfHeart : MonoBehaviour
{
    public static event Action OnHalfHeartCollected;

    public void CollectItemHalfHeart()
    {
        OnHalfHeartCollected?.Invoke();
    }

    public static void CollectItemHalfHeart(int hearts)
    {
        for (int i = 0; i < hearts; ++i) {
            OnHalfHeartCollected?.Invoke();
        }
    }

    public bool CanCollectHeart()
    {
        return Heart.CanCollectHearts();
    }
}