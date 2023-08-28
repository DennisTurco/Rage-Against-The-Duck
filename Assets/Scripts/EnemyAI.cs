using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float speed;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float distance;
	[SerializeField] private float shootTimeMin;
	[SerializeField] private float shootTimeMax;
	[SerializeField] private float moveTimeMin;
	[SerializeField] private float moveTimeMax;
	[SerializeField] private float fireMin;
	[SerializeField] private float fireMax;
    [SerializeField] private FlickerEffect flashEffect;
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

    //health
    public float maxHealth;
    public float health;

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
            Debug.Log("Pos0: " + pos0);
            got = true;
        }

        if(!fireWait) {
            float ranTime = Random.Range(fireMin, fireMax);
            StartCoroutine(fireTimer(ranTime));
        }

        if(shootEnd && fireEnd) {
            pos1 = new Vector2(player.transform.position.x, player.transform.position.y);
            Debug.Log("Pos1: " + pos1);

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
        Vector2 shootPoint = Vector2.MoveTowards(transform.position, p3, 0.8f);
        //Debug.Log("SP: " + shootPoint);
        // Istanzia la munizione nella posizione del firePoint e nella direzione di sparo
        GameObject newBullet = Instantiate(bulletPrefab, new Vector3(shootPoint.x, shootPoint.y, 0.0f), Quaternion.identity);
        newBullet.name = "EnemyBullet";


        // Calcola l'angolo di rotazione della munizione basato sulla direzione di sparo
        Vector2 direction = p3 - shootPoint;
        direction.Normalize();
        float angle =  Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //Debug.Log(angle);
        newBullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void move() {
        Vector2 direction = movePoint - transform.position;
        direction.Normalize();

        float dist = Mathf.Abs(Mathf.Sqrt(Mathf.Pow( movePoint.x - transform.position.x, 2.0f) + Mathf.Pow(movePoint.y - transform.position.y, 2.0f)) - distance);
        // Debug.Log(dist);
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
