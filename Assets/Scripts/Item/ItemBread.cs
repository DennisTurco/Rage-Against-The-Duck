using System;
using UnityEngine;

public class ItemBread : MonoBehaviour
{

    public void CollectItemKey()
    {
        GameManager.Instance.bread++;
    }

    public static void CollectItemKey(int bread)
    {
        GameManager.Instance.bread += bread;
    }

    public static void UseItemKey()
    {
        GameManager.Instance.bread--;
    }
}