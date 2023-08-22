using System;
using UnityEngine;

public class ItemBomb : MonoBehaviour
{
    public static event Action OnBombCollected;

    public void CollectItemBomb()
    {
        OnBombCollected?.Invoke();
    }
}