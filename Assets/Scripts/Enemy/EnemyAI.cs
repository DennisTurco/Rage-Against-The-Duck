using System.Collections;
using UnityEngine;

public abstract class EnemyAI : MonoBehaviour
{
    [SerializeField] protected FlickerEffect flickerEffect;

    // variables from EnemyGeneric (ScriptableObject)
    protected float speed;
    protected float distance;
    protected FloatingHealthBar healthBar;
    protected float maxHealth;
    protected float health;
    protected bool canShoot;
    protected float targetTimeMin;
    protected float targetTimeMax;
    protected float moveTimeMin;
    protected float moveTimeMax;

    protected GameObject target;
    protected float err = 0.1f;
    protected Rigidbody2D rb;
    protected Vector2 movePoint;
    protected bool targetWait = false;
    protected bool targetEnd = false;
    protected bool moveWait = false;
    protected bool moveEnd = false;

    protected void Awake()
    {
        try
        {
            healthBar = GetComponentInChildren<FloatingHealthBar>();
        }
        catch (UnassignedReferenceException exception)
        {
            Debug.Log(exception.Message);
        }
    }

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // initialize health
        health = maxHealth;
        if (healthBar != null)
        {
            Debug.Log($"UpdateHealthBar({health}, {maxHealth})");
            healthBar.UpdateHealthBar(health, maxHealth);
        }
        getNearTarget();
    }

    protected virtual void Update()
    {
        // ######## Find target section ########
        if (!targetWait)
        {
            float ranTime = Random.Range(targetTimeMin, targetTimeMax);
            StartCoroutine(targetTimer(ranTime));
        }

        if (targetEnd)
        {
            getNearTarget();
            targetWait = false;
            targetEnd = false;
        }

        // ######## Moving section ########
        if (!moveWait)
        {
            float ranTime = Random.Range(moveTimeMin, moveTimeMax);
            StartCoroutine(moveTimer(ranTime));
        }

        if (moveEnd == true)
        {
            movePoint = target.transform.position;
            moveWait = false;
            moveEnd = false;
        }

        move();
    }

    protected IEnumerator targetTimer(float time)
    {
        targetWait = true;
        yield return new WaitForSeconds(time);
        targetEnd = true;
    }

    protected IEnumerator moveTimer(float time)
    {
        moveWait = true;
        yield return new WaitForSeconds(time);
        moveEnd = true;
    }

    protected virtual void move()
    {
        Vector2 direction = movePoint - (Vector2)transform.position;
        direction.Normalize();

        float dist = Mathf.Abs(Mathf.Sqrt(Mathf.Pow(movePoint.x - transform.position.x, 2.0f) + Mathf.Pow(movePoint.y - transform.position.y, 2.0f)) - distance);

        if (dist > distance + err)
        {
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
        }
        else if (dist < distance - err)
        {
            rb.MovePosition(rb.position - direction * speed * Time.fixedDeltaTime);
        }
    }

    protected void getNearTarget()
    {
        float minDistance = 1000;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            float newDistance = Vector3.Distance(transform.position, player.transform.position);
            if (minDistance > newDistance)
            {
                minDistance = newDistance;
                target = player;
            }
        }
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        // Debug.Log("ENEMY HEALTH: " + health);

        if (healthBar != null)
        {
            healthBar.UpdateHealthBar(health, maxHealth);
        }

        // flicker effect
        flickerEffect.RedFlash();

        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        GetComponent<LootBag>().InstantiateLootSpawn(transform.position);

        // Instantiate the death effect if it's assigned
        Destroy(gameObject);
    }
}
