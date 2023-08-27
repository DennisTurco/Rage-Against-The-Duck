using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private float fireRate;
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
        if (Input.GetMouseButton(0) && nextFire <= 0)
        {
            Shoot();
            nextFire = fireRate;
        }
        if (Input.GetButtonDown("Fire2") && nextFire <= 0 && GameManager.Instance.bombs > 0)
        {
            ThrowBomb();
            nextFire = fireRate;
        }

        if (nextFire > 0)
            nextFire -= Time.deltaTime;
    }

    private void Shoot()
    {
        // Istanzia la munizione nella posizione del firePoint e nella direzione di sparo
        GameObject newBullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        newBullet.name = "PlayerBullet";

        // Calcola l'angolo di rotazione della munizione basato sulla direzione di sparo
        float angle = Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg;
        newBullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
    private void ThrowBomb()
    {
        // Istanzia la munizione nella posizione del firePoint e nella direzione di sparo
        GameObject newBomb = Instantiate(bombPrefab, firePoint.position, Quaternion.identity);
        newBomb.name = "PlayerBomb";

        // Calcola l'angolo di rotazione della munizione basato sulla direzione di sparo
        float angle = Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg;
        newBomb.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        GameManager.Instance.bombs--;
    }

}
