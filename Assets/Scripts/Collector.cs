using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// the idea of this class: we want to know if the player collides with something and
// is a collectible item

public class Collector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ICollectible collectible = collision.GetComponent<ICollectible>();
         
        if(collectible != null)
        {
            collectible.Collect();
        }
    }
}
