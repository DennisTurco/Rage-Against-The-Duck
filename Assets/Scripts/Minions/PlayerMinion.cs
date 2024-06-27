using System;
using UnityEngine;

public abstract class PlayerMinion : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] protected float speed = 1.5f;
    [SerializeField] protected float distance = 1.6f;

    [Header("Health")]
    [SerializeField] protected FloatingHealthBar healthBar;
    [SerializeField] protected bool canDie = false;
    [SerializeField] protected float maxHealth = 3f; // initial health
    protected float currentHealth;

    protected Rigidbody2D rb;
    public GameObject target;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (canDie)
        {
            currentHealth = maxHealth;
        }
        else
        {
            healthBar.gameObject.SetActive(false);
        }

        target = GameObject.FindWithTag("Player");
    }

    protected virtual void FixedUpdate()
    {
        FollowBehavior();
    }

    protected abstract void FollowBehavior();

    public void TakeDamage(int damage)
    {
        if (canDie)
        {
            currentHealth -= damage;
            if (healthBar != null) healthBar.UpdateHealthBar(currentHealth, maxHealth);
            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (canDie && collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyAI>().TakeDamage(1);
        }
    }

    public static void CreateMinion(GameObject minionPrefab)
    {
        if (minionPrefab == null)
            throw new ArgumentException("minion prefab is null");

        var target = GameObject.FindWithTag("Player");
        GameObject newMinion = Instantiate(minionPrefab, target.transform.position, target.transform.rotation);
        newMinion.name = "PlayerMinion";
    }
}
