using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject player;
    public GameObject bulletPrefab;
    public float speed;
    public float bulletSpeed;
    public float distance;
    private float err = 0.1f;
    private Rigidbody2D rb;
    private float time = 0.5f;
    private Vector2 pos0, pos1, p3;
    private bool wait = false;
    private bool got = false;
    private bool end = false;

    //fire rate
    public float fireRate;
    private float nextFire;

    //health
    public float maxHealth;
    public float health;

    [SerializeField] private FlickerEffect flashEffect;


    void Start() {
        rb = GetComponent<Rigidbody2D>();

        // initialize heath
        health = maxHealth;

        // istantiate fire rate
        nextFire = fireRate;
    }

    void Update() {
        if(got == false) {
            pos0 = new Vector2(player.transform.position.x, player.transform.position.y);
            //Debug.Log("Pos0: " + pos0);
            got = true;
        }

        if(wait == false) {
            StartCoroutine(timer());
        }

        if(end == true) {
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

                Debug.Log(nextFire);
                if (nextFire <= 0)
                {
                    shoot();
                    nextFire = fireRate;
                }
            }

            wait = false;
            got = false;
            end = false;
        }

        move();

        if (nextFire > 0)
            nextFire -= Time.deltaTime;
    }

    IEnumerator timer() {
        wait = true;
        yield return new WaitForSeconds(time);
        end = true;
    }

    void shoot()
    {
        Vector2 shootPoint = Vector2.MoveTowards(transform.position, p3, 0.3f);
        //Debug.Log("SP: " + shootPoint);
        // Istanzia la munizione nella posizione del firePoint e nella direzione di sparo
        GameObject newBullet = Instantiate(bulletPrefab, new Vector3(shootPoint.x, shootPoint.y, 0.0f), Quaternion.identity);

        // Calcola l'angolo di rotazione della munizione basato sulla direzione di sparo
        Vector2 direction = p3 - shootPoint;
        direction.Normalize();
        float angle =  Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //Debug.Log(angle);
        newBullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void move() {
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float dist = Mathf.Abs(Mathf.Sqrt(Mathf.Pow( player.transform.position.x - transform.position.x, 2.0f) + Mathf.Pow(player.transform.position.y - transform.position.y, 2.0f)) - distance);
        //Debug.Log(dist);
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
        Debug.Log("ENEMY HEALTH: " + health);

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