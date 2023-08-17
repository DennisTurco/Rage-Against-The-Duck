using System;
using UnityEngine;

public class ItemBomb : MonoBehaviour, ICollectible
{
    public static event Action OnBombCollected;

    public void Collect()
    {
        Debug.Log("Bomb Collected");
        Destroy(gameObject);
        OnBombCollected?.Invoke();
    }
}
