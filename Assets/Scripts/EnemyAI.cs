using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
	[Header("Script settings")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject bulletPrefab;
	[SerializeField] private FlickerEffect flashEffect;

	[Header("Enemy settings")]
	[Tooltip("Player object")]
    [SerializeField] private float speed;
    [SerializeField] private float bulletSpeed;
	[Tooltip("Distance from player")]
    [SerializeField] private float distance;
	[SerializeField] public float maxHealth;
    [SerializeField] public float health;

	[Header("Shooting settings")]
	[Tooltip("min time between shots")]
	[SerializeField] private float shootTimeMin;
	[Tooltip("max time between shots")]
	[SerializeField] private float shootTimeMax;
	[Tooltip("min time between player tracking")]
	[SerializeField] private float trackTimeMin;
	[Tooltip("max time between player tracking")]
	[SerializeField] private float trackTimeMax;

	[Header("Movement settings")]
	[Tooltip("min time between move")]
   	[SerializeField] private float moveTimeMin;
	[Tooltip("max time between move")]
	[SerializeField] private float moveTimeMax;
    
    private float err = 0.1f;
    private Rigidbody2D rb;
    private Vector2 pos0, pos1, p3;
    private Vector3 movePoint;
	private bool fireWait = false;
    private bool fireEnd = false;
	private bool shootWait = false;
	private bool shootEnd = false;
    private bool moveWait = false;
    private bool moveEnd = false;
    private bool got = false;

    void Start() {
        rb = GetComponent<Rigidbody2D>();

        // initialize heath
        health = maxHealth;
    }

    void Update() {
		if(!shootWait) {
			float ranTime = Random.Range(shootTimeMin, shootTimeMax);
            StartCoroutine(shootTimer(ranTime));
		}

        if(shootEnd && !got) {
            pos0 = new Vector2(player.transform.position.x, player.transform.position.y);
            //Debug.Log("Pos0: " + pos0);
            got = true;
        }

        if(!fireWait) {
            float ranTime = Random.Range(trackTimeMin, trackTimeMax);
            StartCoroutine(fireTimer(ranTime));
        }

        if(shootEnd && fireEnd) {
            pos1 = new Vector2(player.transform.position.x, player.transform.position.y);
            //Debug.Log("Pos1: " + pos1);

            float len = Mathf.Sqrt(Mathf.Pow(pos1.x - pos0.x, 2.0f) + Mathf.Pow(pos1.y - pos0.y, 2.0f));
            //Debug.Log("len: " + len);

            if(len > 0.0) {
                Vector2 d = new Vector2((pos1.x - pos0.x) / len, (pos1.y - pos0.y) / len);
                //Debug.Log("d: " + d);
                float dist = Vector2.Distance(pos0, pos1) + bulletSpeed;
                p3 = new Vector2(pos1.x + dist * d.x, pos1.y + dist * d.y);
                //Debug.Log("p3: " + p3);
            }
            else {
                p3 = new Vector2(player.transform.position.x, player.transform.position.y);
            }

            shoot();
			
			shootWait = false;
			shootEnd = false;
            fireWait = false;
            got = false;
            fireEnd = false;
        }

        if(!moveWait) {
            float ranTime = Random.Range(moveTimeMin, moveTimeMax);
            StartCoroutine(moveTimer(ranTime));
        }

        if(moveEnd == true) {
            movePoint = player.transform.position;
            moveWait = false;
            moveEnd = false;
        }

        move();
    }

    IEnumerator fireTimer(float time) {
        fireWait = true;
        yield return new WaitForSeconds(time);
        fireEnd = true;

    }
    IEnumerator shootTimer(float time) {
        shootWait = true;
        yield return new WaitForSeconds(time);
        shootEnd = true;
    }

    IEnumerator moveTimer(float time) {
        moveWait = true;
        yield return new WaitForSeconds(time);
        moveEnd = true;
    }

    void shoot()
    {
        // Istanzia la munizione nella posizione del firePoint e nella direzione di sparo
        GameObject newBullet = Instantiate(bulletPrefab, new Vector3(transform.position.x, transform.position.y, 0.0f), Quaternion.identity);
        newBullet.name = "EnemyBullet";


        // Calcola l'angolo di rotazione della munizione basato sulla direzione di sparo
        Vector2 direction = p3 - new Vector2(transform.position.x, transform.position.y);
        direction.Normalize();
        float angle =  Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //Debug.Log(angle);
        newBullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void move() {
        Vector2 direction = movePoint - transform.position;
        direction.Normalize();

        float dist = Mathf.Abs(Mathf.Sqrt(Mathf.Pow( movePoint.x - transform.position.x, 2.0f) + Mathf.Pow(movePoint.y - transform.position.y, 2.0f)) - distance);

        if(dist > distance + err) {
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
        }
        else if(dist < distance - err) {
            rb.MovePosition(rb.position - direction * speed * Time.fixedDeltaTime);
        }
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        // Debug.Log("ENEMY HEALTH: " + health);

        // flicker effect
        flashEffect.Flash();

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GetComponent<LootBag>().InstantiateLootSpawn(transform.position);
        Destroy(gameObject);
    }
}
