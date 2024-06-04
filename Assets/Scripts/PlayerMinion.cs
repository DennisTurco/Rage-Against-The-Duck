using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.GraphicsBuffer;

public class PlayerMinion : MonoBehaviour
{
    [SerializeField] private FloatingHealthBar healthBar;
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private float distance = 1.6f;
    [SerializeField] private float maxHealth = 3f;
    [SerializeField] private float health;

    private Rigidbody2D rb;
    public GameObject target;
    private float angle = 0;


    private void Start()
    {
        health = maxHealth;

        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true; // no phisic
    }

    private void Update()
    {
        rb.transform.position = new Vector3(
            distance * Mathf.Cos(angle) + target.transform.position.x, 
            distance * Mathf.Sin(angle) + target.transform.position.y,
            0);

        angle += speed * Time.deltaTime;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (healthBar != null) healthBar.UpdateHealthBar(health, maxHealth);
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyAI>().TakeDamage(1);
        }
    }
}