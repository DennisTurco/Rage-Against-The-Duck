using System.Threading;
using System.Transactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed;
    public float lifeTime;
    public float distance;
    public float damage;

    private Rigidbody2D rb;

    public GameObject destroyEffect;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        //transform.Translate(speed * Time.deltaTime * transform.right);
        transform.position += transform.right * Time.deltaTime * speed;
    }

}


