using System.Collections;
using UnityEngine;

public class PlayerProjectileBomb : MonoBehaviour
{
    [Header("Bomb Settings")]
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;
    [SerializeField] private float damage;
    [SerializeField] private float rangeDamage;

    [Header("Explosion Animation Settings")]
    [SerializeField] private GameObject destroyEffect;
    [SerializeField] private Sprite[] explosionFrames;
    [SerializeField] private float explosionFrameDelay = 0.15f;
    [SerializeField] private Vector3 explosionScale = new Vector3(1, 1, 1);

    private SpriteRenderer spriteRenderer;
    private Coroutine explosionCoroutine;

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
                var player = hitCollider.GetComponent<PlayerHealth>();
                var friableRock = hitCollider.GetComponent<FriableRock>();
                var destructableObject = hitCollider.GetComponent<DestructableObject>();
                if (enemy)
                {
                    var closestPoint = hitCollider.ClosestPoint(transform.position);
                    var distance = Vector3.Distance(closestPoint, transform.position);

                    var damagePercent = Mathf.InverseLerp(rangeDamage, 0, distance);
                    enemy.TakeDamage(damagePercent * damage);
                }
                else if (player)
                {
                    player.TakeDamage();
                }
                else if (friableRock)
                {
                    friableRock.DestroyRock();
                }
                else if (destructableObject)
                {
                    destructableObject.DestroyObject();
                }
            }
        }

        if (explosionFrames != null && explosionFrames.Length > 0)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
            {
                spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
            }
            transform.localScale = explosionScale;
            explosionCoroutine = StartCoroutine(PlayExplosionAnimation());
        }
        else
        {
            DestroyBomb();
        }
    }

    private IEnumerator PlayExplosionAnimation()
    {
        for (int i = 0; i < explosionFrames.Length; i++)
        {
            spriteRenderer.sprite = explosionFrames[i];
            yield return new WaitForSeconds(explosionFrameDelay);
        }
        DestroyBomb();
    }

    private void DestroyBomb()
    {
        if (destroyEffect != null) Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        GameManager.Instance.ShakeCamera(.15f, .4f);
    }
}
