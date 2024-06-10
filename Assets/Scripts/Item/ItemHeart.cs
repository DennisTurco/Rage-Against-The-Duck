using System;
using UnityEngine;

public class ItemHeart : MonoBehaviour
{
    public static event Action CanCollectHearts;
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

    public static bool CanCollectHeart(int hearts)
    {
        for (int i = 0; i < hearts; ++i)
        {
            if (!Heart.CanCollectHearts())
            {
                return false;
            }
        }
        return true;
    }
}