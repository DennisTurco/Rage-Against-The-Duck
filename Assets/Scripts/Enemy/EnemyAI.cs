using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private EnemyGeneric enemy;
    [SerializeField] private FlickerEffect flickerEffect;

    //TODO: sarebbe bello non includerlo, ma richimare normalmente le funzioni di questa classe e far diventare la classe EnemyAIShootingTypeFunctions una classe normale, senza estendere MonoBehaviour 
    [SerializeField] private EnemyAIShootingTypeFunctions shootingTypeFunctions;

    // variables from EnemyGeneric (ScriptableObject)
    private float speed;
    private float bulletSpeed;
    private float distance;
    private FloatingHealthBar healthBar;
    private float maxHealth;
    private float health;
    private bool canShoot;
    private ShootingType shootingType;
    private float targetTimeMin;
    private float targetTimeMax;
    public float shootTimeMin;
    public float shootTimeMax;
    public float trackTimeMin;
    public float trackTimeMax;
    public float moveTimeMin;
    public float moveTimeMax;

    private GameObject target;
    private float err = 0.1f;
    private Rigidbody2D rb;
    private Vector2 pos0, pos1, p3;
    private Vector3 movePoint;
    private bool targetWait = false;
    private bool targetEnd = false;
    private bool fireWait = false;
    private bool fireEnd = false;
    private bool shootWait = false;
    private bool shootEnd = false;
    private bool moveWait = false;
    private bool moveEnd = false;
    private bool got = false;

    private void Awake()
    {
        try
        {
            healthBar = enemy.healthBar;
            healthBar = GetComponentInChildren<FloatingHealthBar>();
        } catch(UnassignedReferenceException exception)
        {
            Debug.Log(exception.Message);
        }
    }

    void Start()
    {
        // init variables
        speed = enemy.speed;
        bulletSpeed = enemy.bulletSpeed;
        distance = enemy.distance;
        maxHealth = enemy.maxHealth;
        health = enemy.health;
        canShoot = enemy.canShoot;
        shootingType = enemy.shootingType;
        targetTimeMin = enemy.targetTimeMin;
        targetTimeMax = enemy.targetTimeMax;
        shootTimeMin = enemy.shootTimeMin;
        shootTimeMax = enemy.shootTimeMax;
        trackTimeMin = enemy.trackTimeMin;
        trackTimeMax = enemy.trackTimeMax;
        moveTimeMin = enemy.moveTimeMin;
        moveTimeMax = enemy.moveTimeMax;

        rb = GetComponent<Rigidbody2D>();

        // initialize heath
        health = maxHealth;
        if (healthBar != null) healthBar.UpdateHealthBar(health, maxHealth);

        getNearTarget();
    }

    void Update()
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
        // ######## Shooting section ########

        if (canShoot)
        {
            if (!shootWait)
            {
                float ranTime = Random.Range(shootTimeMin, shootTimeMax);
                StartCoroutine(shootTimer(ranTime));
            }

            if (shootEnd && !got)
            {
                pos0 = new Vector2(target.transform.position.x, target.transform.position.y);
                //Debug.Log("Pos0: " + pos0);
                got = true;
            }

            if (!fireWait)
            {
                float ranTime = Random.Range(trackTimeMin, trackTimeMax);
                StartCoroutine(fireTimer(ranTime));
            }

            if (shootEnd && fireEnd)
            {
                pos1 = new Vector2(target.transform.position.x, target.transform.position.y);

                //Debug.Log("Pos1: " + pos1);

                float len = Mathf.Sqrt(Mathf.Pow(pos1.x - pos0.x, 2.0f) + Mathf.Pow(pos1.y - pos0.y, 2.0f));
                //Debug.Log("len: " + len);

                if (len > 0.0)
                {
                    Vector2 d = new Vector2((pos1.x - pos0.x) / len, (pos1.y - pos0.y) / len);
                    //Debug.Log("d: " + d);
                    float dist = Vector2.Distance(pos0, pos1) + bulletSpeed;
                    p3 = new Vector2(pos1.x + dist * d.x, pos1.y + dist * d.y);
                    //Debug.Log("p3: " + p3);
                }
                else
                {
                    p3 = new Vector2(target.transform.position.x, target.transform.position.y);
                }
            
                shoot();

                shootWait = false;
                shootEnd = false;
                fireWait = false;
                got = false;
                fireEnd = false;
            }
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

    IEnumerator targetTimer(float time)
    {
        targetWait = true;
        yield return new WaitForSeconds(time);
        targetEnd = true;
    }

    IEnumerator fireTimer(float time)
    {
        fireWait = true;
        yield return new WaitForSeconds(time);
        fireEnd = true;
    }

    IEnumerator shootTimer(float time)
    {
        shootWait = true;
        yield return new WaitForSeconds(time);
        shootEnd = true;
    }

    IEnumerator moveTimer(float time)
    {
        moveWait = true;
        yield return new WaitForSeconds(time);
        moveEnd = true;
    }

    void shoot()
    {
        switch (shootingType)
        {
            case ShootingType.SimpleShooting:
                shootingTypeFunctions.SimpleShooting(enemy.bulletPrefab, p3);
                break;
            case ShootingType.FourAxisShooting:
                shootingTypeFunctions.FourAxesShooting(enemy.bulletPrefab);
                break;
            case ShootingType.DiagonalShooting:
                shootingTypeFunctions.DiagonalShooting(enemy.bulletPrefab);
                break;
        }
    }

    void move()
    {
        Vector2 direction = movePoint - transform.position;
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

    void getNearTarget()
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

        if (healthBar != null) healthBar.UpdateHealthBar(health, maxHealth);

        // flicker effect
        flickerEffect.RedFlash();

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GetComponent<LootBag>().InstantiateLootSpawn(transform.position);

        // Instantiate the death effect if it's assigned
        if (enemy.deathEffect != null) Instantiate(enemy.deathEffect, transform.position, Quaternion.identity);
        if (enemy.deathBloodEffect != null) Instantiate(enemy.deathBloodEffect, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}