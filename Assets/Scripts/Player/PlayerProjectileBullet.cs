using System.Collections;
using UnityEngine;

public class PlayerProjectileBullet : MonoBehaviour
{
    [SerializeField] private GameObject destroyEffect;
    private PlayerStats stats;

    private void Start()
    {
        GameObject obj = GameObject.FindWithTag("Player");
        if (obj == null)
        {
            Debug.LogError("Cannot obtain 'Player' tag");
            return; // Early return to avoid null reference exceptions
        }

        stats = obj.GetComponent<PlayerStats>();
        if (stats == null)
        {
            Debug.LogError("Cannot obtain 'PlayerStats' component");
            return; // Early return to avoid null reference exceptions
        }

        StartCoroutine(DeathDelay());
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject bullet in bullets)
        {
            Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    private void Update()
    {
        transform.position += transform.right * Time.deltaTime * stats.AttackSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<EnemyAI>(out EnemyAI enemyComponent))
        {
            float damage = Random.Range(stats.AttackDamageMin, stats.AttackDamageMax);
            enemyComponent.TakeDamage(damage);
        }
        else if (collision.gameObject.TryGetComponent<DestructableObject>(out DestructableObject destructableObject))
        {
            destructableObject.TakeHit();
        }

        DestroyProjectile();
    }

    private IEnumerator DeathDelay()
    {
        float lifeTime = Random.Range(stats.AttackRangeMin, stats.AttackRangeMax);
        yield return new WaitForSeconds(lifeTime);
        DestroyProjectile();
    }

    private void DestroyProjectile()
    {
        if (destroyEffect != null) Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
