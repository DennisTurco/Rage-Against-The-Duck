using System;
using UnityEngine;

public class ItemHeart : MonoBehaviour
{
    public static event Action OnHeartCollected;

    public void CollectItemHeart()
    {
        //GameManager.Instance.AddHeart();
        OnHeartCollected?.Invoke();
    }
}