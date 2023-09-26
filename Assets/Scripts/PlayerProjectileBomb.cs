using System.Collections;
using UnityEngine;

public class PlayerProjectileBomb : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;
    [SerializeField] private float damage;
    [SerializeField] private float rangeDamage;

    [SerializeField] private GameObject destroyEffect;

    private void Start()
    {
        StartCoroutine(DeathDelay());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerProjectileBullet>(out PlayerProjectileBullet projectile))
        {
            StartExplosion();
        }
    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(lifeTime);
        StartExplosion();
    }

    private void StartExplosion()
    {
        if (rangeDamage > 0)
        {
            var hitColliders = Physics2D.OverlapCircleAll(transform.position, rangeDamage);
            foreach (var hitCollider in hitColliders)
            {
                var enemy = hitCollider.GetComponent<EnemyAI>();
                var player = hitCollider.GetComponent<HealthBar>();
                if (enemy)
                {
                    var closestPoint = hitCollider.ClosestPoint(transform.position);
                    var distance = Vector3.Distance(closestPoint, transform.position);

                    var damagePercent = Mathf.InverseLerp(rangeDamage, 0, distance);
                    enemy.TakeDamage(damagePercent * damage);
                    Debug.Log("Bomb damage: " + damagePercent * damage);
                }
                else if (player)
                {
                    player.TakeDamage();
                }
            }
        }

        DestroyBomb();
    }

    private void DestroyBomb()
    {
        // Instantiate the impact effect if it's assigned
        if (destroyEffect != null) Instantiate(destroyEffect, transform.position, Quaternion.identity);

        // Destroy the bullet GameObject
        Destroy(gameObject);

        GameManager.Instance.ShakeCamera(.15f, .4f);
    }
}