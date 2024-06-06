using System;
using UnityEngine;

public class ItemMinion : MonoBehaviour
{
    public static event Action OnMinionCollected;

    public void CollectItemMinion()
    {
        OnMinionCollected?.Invoke();
        GameManager.Instance.minions++;
    }
}