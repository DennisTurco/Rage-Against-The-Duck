using System;
using UnityEngine;

public class ItemMinion : MonoBehaviour
{
    public static event Action OnMinionCollected;

    public void CollectItemMinion()
    {
        GameManager.Instance.minions++;
        OnMinionCollected?.Invoke();
    }

    public static void CollectItemMinion(int minions)
    {
        GameManager.Instance.minions += minions;
        OnMinionCollected?.Invoke();
    }
}