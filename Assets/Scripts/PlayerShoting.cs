using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float fireRate;
    private float nextFire;

    // Aggiungi una variabile per tenere traccia della direzione del giocatore
    private Vector2 shootingDirection;

    void Start()
    {
        nextFire = fireRate;
    }

    void Update()
    {
        // Ottieni l'input per sparo orizzontale e verticale (assumi che le frecce siano usate per il movimento)
        float shootHorizontal = Input.GetAxisRaw("Horizontal");
        float shootVertical = Input.GetAxisRaw("Vertical");

        // Aggiorna la direzione di sparo solo se c'è input
        if (Mathf.Abs(shootHorizontal) > 0.1f || Mathf.Abs(shootVertical) > 0.1f)
        {
            shootingDirection = new Vector2(shootHorizontal, shootVertical).normalized;
        }
        if (Input.GetMouseButtonDown(0) && nextFire <= 0)
        {
            Shoot();
            nextFire = fireRate;
        }

        if (nextFire > 0)
            nextFire -= Time.deltaTime;
    }

    void Shoot()
    {
        // Istanzia la munizione nella posizione del firePoint e nella direzione di sparo
        GameObject newBullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // Calcola l'angolo di rotazione della munizione basato sulla direzione di sparo
        float angle = Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg;
        newBullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
