using System.Collections;
using UnityEngine;

public class RangeEnemyAI : EnemyAI
{
    [SerializeField] private RangeEnemy enemy;
    [SerializeField] private EnemyAIShootingTypeFunctions shootingTypeFunctions;

    private float bulletSpeed;
    private ShootingType shootingType;
    private float shootTimeMin;
    private float shootTimeMax;
    private float trackTimeMin;
    private float trackTimeMax;
    private bool fireWait = false;
    private bool fireEnd = false;
    private bool shootWait = false;
    private bool shootEnd = false;
    private bool got = false;
    private Vector2 pos0, pos1, p3;

    protected override void Start()
    {
        // init variables
        speed = enemy.speed;
        bulletSpeed = enemy.bulletSpeed;
        distance = enemy.distance;
        maxHealth = enemy.maxHealth;
        health = enemy.health;
        canShoot = enemy.canShoot;
        shootingType = enemy.shootingType;
        targetTimeMin = enemy.targetTimeMin;
        targetTimeMax = enemy.targetTimeMax;
        shootTimeMin = enemy.shootTimeMin;
        shootTimeMax = enemy.shootTimeMax;
        trackTimeMin = enemy.trackTimeMin;
        trackTimeMax = enemy.trackTimeMax;
        moveTimeMin = enemy.moveTimeMin;
        moveTimeMax = enemy.moveTimeMax;

        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        // ######## Shooting section ########
        if (canShoot)
        {
            if (!shootWait)
            {
                float ranTime = Random.Range(shootTimeMin, shootTimeMax);
                StartCoroutine(shootTimer(ranTime));
            }

            if (shootEnd && !got)
            {
                pos0 = new Vector2(target.transform.position.x, target.transform.position.y);
                got = true;
            }

            if (!fireWait)
            {
                float ranTime = Random.Range(trackTimeMin, trackTimeMax);
                StartCoroutine(fireTimer(ranTime));
            }

            if (shootEnd && fireEnd)
            {
                pos1 = new Vector2(target.transform.position.x, target.transform.position.y);

                float len = Mathf.Sqrt(Mathf.Pow(pos1.x - pos0.x, 2.0f) + Mathf.Pow(pos1.y - pos0.y, 2.0f));

                if (len > 0.0)
                {
                    Vector2 d = new Vector2((pos1.x - pos0.x) / len, (pos1.y - pos0.y) / len);
                    float dist = Vector2.Distance(pos0, pos1) + bulletSpeed;
                    p3 = new Vector2(pos1.x + dist * d.x, pos1.y + dist * d.y);
                }
                else
                {
                    p3 = new Vector2(target.transform.position.x, target.transform.position.y);
                }

                shoot();

                shootWait = false;
                shootEnd = false;
                fireWait = false;
                got = false;
                fireEnd = false;
            }
        }
    }

    private IEnumerator fireTimer(float time)
    {
        fireWait = true;
        yield return new WaitForSeconds(time);
        fireEnd = true;
    }

    private IEnumerator shootTimer(float time)
    {
        shootWait = true;
        yield return new WaitForSeconds(time);
        shootEnd = true;
    }

    private void shoot()
    {
        switch (shootingType)
        {
            case ShootingType.SimpleShooting:
                shootingTypeFunctions.SimpleShooting(enemy.bulletPrefab, p3);
                break;
            case ShootingType.FourAxisShooting:
                shootingTypeFunctions.FourAxesShooting(enemy.bulletPrefab);
                break;
            case ShootingType.DiagonalShooting:
                shootingTypeFunctions.DiagonalShooting(enemy.bulletPrefab);
                break;
        }
    }

    protected override void Die()
    {
        if (enemy.deathEffect != null) Instantiate(enemy.deathEffect, transform.position, Quaternion.identity);
        GetComponent<SpawnBlood>().InstantiateBloodObject(transform.position);

        base.Die();
    }
}
