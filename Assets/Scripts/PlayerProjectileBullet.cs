using System.Collections;
using UnityEngine;

public class PlayerProjectileBullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifeTimeMin;
    [SerializeField] private float lifeTimeMax;
    [SerializeField] private float damageMin;
    [SerializeField] private float damageMax;

    [SerializeField] private GameObject destroyEffect;

    private void Awake()
    {
        GameManager.Instance.SetAttackDamage(damageMin, damageMax);
        GameManager.Instance.SetAttackSpeed(speed);
        GameManager.Instance.SetAttackRange(lifeTimeMin, lifeTimeMax);
    }

    private void Start()
    {
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
        transform.position += transform.right * Time.deltaTime * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<EnemyAI>(out EnemyAI enemyComponent))
        {
            float damage = Random.Range(damageMin, damageMax);
            enemyComponent.TakeDamage(damage);
        }

        if (collision.gameObject.TryGetComponent<DestructableObject>(out DestructableObject destructableObject))
        {
            destructableObject.TakeHit();
        }

        DestroyProjectile();
    }

    IEnumerator DeathDelay()
    {
        float lifeTime = Random.Range(lifeTimeMin, lifeTimeMax);
        yield return new WaitForSeconds(lifeTime);
        DestroyProjectile();
    }

    private void DestroyProjectile()
    {
        if (destroyEffect != null) Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
