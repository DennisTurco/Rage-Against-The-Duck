using System.Collections;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private float fireRate;
    private float nextFire;

    // variable to keep track of player direction
    private Vector2 shootingDirection;

    void Start()
    {
        nextFire = fireRate;
    }

    void Update()
    {
        // input for horizontal and vertical shot
        float shootHorizontal = Input.GetAxisRaw("Horizontal");
        float shootVertical = Input.GetAxisRaw("Vertical");

        // Update the firing direction only if there is input
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
        // Instantiate ammunition in firePoint position and firing direction
        GameObject newBullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        newBullet.name = "PlayerBullet";

        // Calculate the angle of rotation of the ammunition based on the firing direction
        float angle = Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg;
        newBullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
    private void ThrowBomb()
    {
        // Instantiate ammunition in firePoint position and firing direction
        GameObject newBomb = Instantiate(bombPrefab, firePoint.position, Quaternion.identity);
        newBomb.name = "PlayerBomb";

        GameManager.Instance.bombs--;
    }

}
