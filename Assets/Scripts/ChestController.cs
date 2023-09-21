using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    private bool chestOpened;

    private void Start()
    {
        chestOpened = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (chestOpened) return;

        // collision with player
        if (collision.gameObject.TryGetComponent<HealthBar>(out HealthBar playerComponent))
        {
            GetComponent<LootBag>().InstantiateLootSpawn(new Vector3(transform.position.x, transform.position.y - 1, 0));
            chestOpened = true;
        }

    }
}
