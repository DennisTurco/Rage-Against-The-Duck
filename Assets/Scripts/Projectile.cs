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
        // Enemies to take damage
        if (collision.gameObject.TryGetComponent<EnemyAI>(out EnemyAI enemyComponent))
        {
            Debug.Log(damage);
            enemyComponent.TakeDamage(damage);
        }

        // Player to take damage
        if (collision.gameObject.TryGetComponent<HeathBar>(out HeathBar barComponent))
        {
            Debug.Log(1);
            barComponent.TakeDamage();
        }

        DestroyProjectile();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            Debug.Log("ENTRATO");
            DestroyProjectile();
        }

        if (other.CompareTag("Enemy"))
        {
            Debug.Log("ENTRATO");
            DestroyProjectile();
        }
    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(lifeTime);
        DestroyProjectile();
    }

    private void DestroyProjectile()
    {
        // Instantiate the impact effect if it's assigned
        if (destroyEffect != null) Instantiate(destroyEffect, transform.position, Quaternion.identity);

        // Destroy the bullet GameObject
        Destroy(gameObject);
    }
}