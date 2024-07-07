using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyProjectileBullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;
    [SerializeField] private float damage;

    [SerializeField] private GameObject destroyEffect;

    private void Start()
    {
        StartCoroutine(DeathDelay());
		GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");     
		foreach(GameObject enemy in enemys) {
			Physics2D.IgnoreCollision(enemy.GetComponent<Collider2D>(), GetComponent<Collider2D>());
		}

        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject bullet in bullets)
        {
            Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    private void Update()
    {
        transform.position += transform.right * Time.deltaTime * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Player to take damage
        if (collision.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
        {
            playerHealth.TakeDamage();
        }
        DestroyProjectile();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerMinion>(out PlayerMinion minion))
        {
            minion.TakeDamage(1);
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
