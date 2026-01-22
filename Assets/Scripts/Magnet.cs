using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<ItemBread>(out ItemBread bread))
        {
            bread.SetTarget(transform.parent.position);
        }
    }
}
