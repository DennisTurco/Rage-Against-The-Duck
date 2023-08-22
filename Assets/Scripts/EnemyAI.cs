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
    private float shootTime = 0.5f;
    private float moveTime = 1.0f;
    private Vector2 pos0, pos1, p3;
    private bool shootWait = false;
    private bool shootEnd = false;
    private bool moveWait = false;
    private bool moveEnd = false;
    private int moveIdx = 10;
    private bool got = false;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if(got == false) {
            pos0 = new Vector2(player.transform.position.x, player.transform.position.y);
            //Debug.Log("Pos0: " + pos0);
            got = true;
        }

        if(shootWait == false) {
            StartCoroutine(shootTimer(shootTime));
        }

        if(shootEnd == true) {
            pos1 = new Vector2(player.transform.position.x, player.transform.position.y);
            //Debug.Log("Pos1: " + pos1);

            float len = Mathf.Sqrt(Mathf.Pow(pos1.x - pos0.x, 2.0f) + Mathf.Pow(pos1.y - pos0.y, 2.0f));
            Debug.Log("len: " + len);

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
            got = false;
            shootEnd = false;
        }

        if(moveWait == false) {
            StartCoroutine(moveTimer(moveTime));
        }

        if(moveEnd == true) {
            move();
            --moveIdx;
        }

        if(moveIdx == 0) {
            moveIdx = 10;
            moveWait = false;
            moveEnd = false;
        }
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
        Debug.Log(dist);
        if(dist > distance + err) {
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
        }
        else if(dist < distance - err) {
            rb.MovePosition(rb.position - direction * speed * Time.fixedDeltaTime);
        }
    }
}