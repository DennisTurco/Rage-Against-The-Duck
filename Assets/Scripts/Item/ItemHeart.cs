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

    public bool CanCollectHeart()
    {
        return Heart.CanCollectHearts();
    }
}