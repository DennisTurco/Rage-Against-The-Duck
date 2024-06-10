using System;
using UnityEngine;

public class ItemBomb : MonoBehaviour
{
    public static event Action UpdateBombText;

    public void CollectItemBomb()
    {
        GameManager.Instance.bombs++;
        UpdateBombText?.Invoke();
    }

    public static void CollectItemBomb(int bombs)
    {
        GameManager.Instance.bombs += bombs;
        UpdateBombText?.Invoke();
    }

    public static void UseItemBomb()
    {
        GameManager.Instance.bombs--;
        UpdateBombText?.Invoke();
    }
}