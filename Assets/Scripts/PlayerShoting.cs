using System.Collections;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private float fireRate;
    private float nextFire;

    void Start()
    {
        GameManager.Instance.SetAttackRate(fireRate);

        nextFire = fireRate;
    }

    void Update()
    {
        if ((Input.GetButton("FireUp") || Input.GetButton("FireDown") || Input.GetButton("FireLeft") || Input.GetButton("FireRight")) && nextFire <= 0)
        {
            if (Input.GetButton("FireUp")) Shoot(new Vector2(0, 1).normalized);
            else if (Input.GetButton("FireDown")) Shoot(new Vector2(0, -1).normalized);
            else if (Input.GetButton("FireRight")) Shoot(new Vector2(1, 0).normalized);
            else if (Input.GetButton("FireLeft")) Shoot(new Vector2(-1, 0).normalized);
            nextFire = fireRate;
        }
        if (Input.GetButtonDown("ThrowBomb") && nextFire <= 0 && GameManager.Instance.bombs > 0)
        {
            ThrowBomb();
            nextFire = fireRate;
        }

        if (nextFire > 0)
            nextFire -= Time.deltaTime;
    }

    private void Shoot(Vector2 shootingDirection)
    {
        // Istanzia la munizione nella posizione del player e nella direzione di sparo
        GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        newBullet.name = "PlayerBullet";

        // Calculate the angle of rotation of the ammunition based on the firing direction
        float angle = Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg;
        newBullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
    private void ThrowBomb()
    {
        // Istanzia la munizione nella posizione del player e nella direzione di sparo
        GameObject newBomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);
        newBomb.name = "PlayerBomb";

        GameManager.Instance.bombs--;
    }

}
