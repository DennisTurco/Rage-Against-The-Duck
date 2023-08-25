using System.Threading;
using System.Transactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public float distance;
    public float damage;

    public GameObject destroyEffect;

    private void Start()
    {
        StartCoroutine(DeathDelay());
    }

    private void Update()
    {
        transform.position += transform.right * Time.deltaTime * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DestroyProjectile();
    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(lifeTime);
        DestroyProjectile();
    }

    private void DestroyProjectile()
    {
        // Instantiate the impact effect if it's assigned
        if (destroyEffect != null) Instantiate(destroyEffect, transform.position, transform.rotation);

        // Destroy the bullet GameObject
        Destroy(gameObject);
    }
}