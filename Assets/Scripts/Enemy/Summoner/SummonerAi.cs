using System.Collections;
using UnityEngine;

public class SummonerEnemyAI : EnemyAI
{
    [Header("References")]
    [SerializeField] private SummonerEnemy enemy;
    [SerializeField] private EnemyAIShootingTypeFunctions shootingTypeFunctions;

    [Header("Summoning Settings")]
    [Tooltip("Intervallo di tempo tra le evocazioni dei minion")]
    [SerializeField] private float summonInterval;
    [Tooltip("Numero di minion evocati alla morte")]
    [SerializeField] private int numberOfMinionsOnDeath;
    [Tooltip("Prefab del Melee Minion")]
    [SerializeField] private GameObject meleeMinionPrefab;
    [Tooltip("Prefab del Range Minion")]
    [SerializeField] private GameObject rangeMinionPrefab;
    [Tooltip("Numero di Melee Minion evocati")]
    [SerializeField] private int numberOfMeleeMinions;
    [Tooltip("Numero di Range Minion evocati")]
    [SerializeField] private int numberOfRangeMinions;

    [Header("Ranged Attack Settings")]
    [Tooltip("Il Summoner può sparare")]
    [SerializeField] private bool summonerCanShoot;
    [Tooltip("Tipo di sparo")]
    [SerializeField] private ShootingType shootingType;
    [Tooltip("Prefab del proiettile")]
    [SerializeField] private GameObject bulletPrefab;
    [Tooltip("Tempo minimo tra gli attacchi a distanza")]
    [SerializeField] private float shootTimeMin;
    [Tooltip("Tempo massimo tra gli attacchi a distanza")]
    [SerializeField] private float shootTimeMax;

    [Header("Movement Settings")]
    [Tooltip("Distanza desiderata dal giocatore")]
    [SerializeField] private float desiredDistance = 5f;

    private bool shootWait = false;
    private bool shootEnd = false;

    protected override void Start()
    {
        speed = enemy.speed;
        maxHealth = enemy.maxHealth;
        health = enemy.health;
        summonInterval = enemy.summonInterval;
        numberOfMinionsOnDeath = enemy.numberOfMinionsOnDeath;
        meleeMinionPrefab = enemy.meleeMinionPrefab;
        rangeMinionPrefab = enemy.rangeMinionPrefab;
        numberOfMeleeMinions = enemy.numberOfMeleeMinions;
        numberOfRangeMinions = enemy.numberOfRangeMinions;
        summonerCanShoot = enemy.canShoot;
        shootingType = enemy.shootingType;
        bulletPrefab = enemy.bulletPrefab;
        shootTimeMin = enemy.shootTimeMin;
        shootTimeMax = enemy.shootTimeMax;

        base.Start();

        StartCoroutine(SummonMinions());
    }

    protected override void Update()
    {
        base.Update();

        MaintainDistanceFromPlayer();

        if (summonerCanShoot)
        {
            if (!shootWait)
            {
                float ranTime = Random.Range(shootTimeMin, shootTimeMax);
                StartCoroutine(shootTimer(ranTime));
            }

            if (shootEnd)
            {
                shoot();
                shootWait = false;
                shootEnd = false;
            }
        }
    }

    private void MaintainDistanceFromPlayer()
    {
        if (target != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, target.transform.position);
            Vector2 direction = (transform.position - target.transform.position).normalized;

            if (distanceToPlayer < desiredDistance)
            {
                rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
            }
            else if (distanceToPlayer > desiredDistance + err)
            {
                rb.MovePosition(rb.position - direction * speed * Time.fixedDeltaTime);
            }
        }
    }

    private IEnumerator SummonMinions()
    {
        yield return new WaitForSeconds(2f);
        SummonMinionGroup();

        while (true)
        {
            yield return new WaitForSeconds(summonInterval);
            if (target != null)
            {
                SummonMinionGroup();
            }
        }
    }

    private void SummonMinionGroup()
    {
        int totalMinions = numberOfMeleeMinions + numberOfRangeMinions;
        for (int i = 0; i < totalMinions; i++)
        {
            GameObject minionPrefab = (i < numberOfMeleeMinions) ? meleeMinionPrefab : rangeMinionPrefab;
            Vector2 summonPosition = (Vector2)transform.position + Random.insideUnitCircle.normalized * 2f;
            Instantiate(minionPrefab, summonPosition, Quaternion.identity);
        }
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
                shootingTypeFunctions.SimpleShooting(bulletPrefab, target.transform.position);
                break;
            case ShootingType.FourAxisShooting:
                shootingTypeFunctions.FourAxesShooting(bulletPrefab);
                break;
            case ShootingType.DiagonalShooting:
                shootingTypeFunctions.DiagonalShooting(bulletPrefab);
                break;
        }
    }

    protected override void Die()
    {
        int totalMinions = numberOfMeleeMinions + numberOfRangeMinions;
        for (int i = 0; i < totalMinions; i++)
        {
            GameObject minionPrefab = (i < numberOfMeleeMinions) ? meleeMinionPrefab : rangeMinionPrefab;
            Instantiate(minionPrefab, transform.position, Quaternion.identity);
        }
        base.Die();
    }
}
