using System.Collections;
using UnityEngine;

public class MinionEnemyAI : EnemyAI
{
    [SerializeField] private MinionEnemy enemy;
    [SerializeField] private EnemyAIShootingTypeFunctions shootingTypeFunctions;
    [SerializeField] private EnemyAIMeleeTypeFunctions meleeTypeFunctions;

    private float meleeRange;
    private bool minionCanShoot;
    private ShootingType shootingType;
    private float shootTimeMin;
    private float shootTimeMax;
    private bool isAttacking = false;
    private bool isShooting = false;
    private bool isRetreating = false;
    private bool isWaitingToMelee = false;
    private float retreatDistance = 5f;

    protected override void Start()
    {
        speed = enemy.speed;
        maxHealth = enemy.maxHealth;
        health = enemy.health;
        meleeRange = enemy.meleeRange;
        minionCanShoot = enemy.canShoot;
        shootingType = enemy.shootingType;
        shootTimeMin = enemy.shootTimeMin;
        shootTimeMax = enemy.shootTimeMax;

        targetTimeMin = enemy.targetTimeMin;
        targetTimeMax = enemy.targetTimeMax;
        moveTimeMin = enemy.moveTimeMin;
        moveTimeMax = enemy.moveTimeMax;

        base.Start();

        StartCoroutine(SwitchAttackMode());
    }

    protected override void Update()
    {
        base.Update();

        if (isShooting)
        {
            RangedAttack();
        }
        else if (isWaitingToMelee)
        {
            StartCoroutine(WaitToMeleeAttack());
        }
        else if (isAttacking)
        {
            MeleeAttack();
        }
        else if (isRetreating)
        {
            RetreatFromPlayer();
        }
    }

    private void MeleeAttack()
    {
        if (target != null)
        {
            meleeTypeFunctions.SimpleMelee(target, meleeRange);
            isAttacking = false;
            isRetreating = true;
        }
    }

    private IEnumerator WaitToMeleeAttack()
    {
        isWaitingToMelee = true;
        yield return new WaitForSeconds(1f); 
        isWaitingToMelee = false;
        isAttacking = true;
    }

    private void RangedAttack()
    {
        if (minionCanShoot && target != null)
        {
            if (!isShooting)
            {
                float ranTime = Random.Range(shootTimeMin, shootTimeMax);
                StartCoroutine(shootTimer(ranTime));
            }

            if (isShooting)
            {
                shoot();
                isShooting = false;
                isWaitingToMelee = true;
            }
        }
    }

    private void RetreatFromPlayer()
    {
        if (target != null)
        {
            Vector2 direction = (transform.position - target.transform.position).normalized;
            Vector2 retreatPosition = (Vector2)transform.position + direction * retreatDistance;

            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, retreatPosition, step);

            if (Vector2.Distance(transform.position, target.transform.position) >= retreatDistance)
            {
                isRetreating = false;
                isShooting = true;
            }
        }
    }

    private IEnumerator shootTimer(float time)
    {
        isShooting = true;
        yield return new WaitForSeconds(time);
        isShooting = false;
    }

    private void shoot()
    {
        switch (shootingType)
        {
            case ShootingType.SimpleShooting:
                shootingTypeFunctions.SimpleShooting(enemy.bulletPrefab, target.transform.position);
                break;
            case ShootingType.FourAxisShooting:
                shootingTypeFunctions.FourAxesShooting(enemy.bulletPrefab);
                break;
            case ShootingType.DiagonalShooting:
                shootingTypeFunctions.DiagonalShooting(enemy.bulletPrefab);
                break;
        }
    }

    private IEnumerator SwitchAttackMode()
    {
        while (true)
        {
            isShooting = true;
            yield return new WaitForSeconds(shootTimeMax);

            isShooting = false;
            isWaitingToMelee = true;
            yield return new WaitForSeconds(1f);

            isAttacking = true;
            yield return new WaitUntil(() => !isAttacking);

            isRetreating = true;
            yield return new WaitUntil(() => !isRetreating);
        }
    }
}
