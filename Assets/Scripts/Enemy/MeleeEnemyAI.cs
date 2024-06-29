using System.Collections;
using UnityEngine;

public class MeleeEnemyAI : EnemyAI
{
    [SerializeField] private MeleeEnemy enemy;
    [SerializeField] private EnemyAIMeleeTypeFunctions meleeTypeFunctions;

    private float meleeRange;
    private float attackDelay;
    private MeleeType meleeAttackType;
    private bool canAttack = true;

    private bool isMovingRandomly = false;
    private float randomMoveDuration = 2.0f;
    private float randomMoveCooldown = 3.0f;
    private Vector2 randomDirection;

    protected override void Start()
    {
        // init variables
        speed = enemy.speed;
        distance = enemy.distance;
        maxHealth = enemy.maxHealth;
        health = enemy.health;
        targetTimeMin = enemy.targetTimeMin;
        targetTimeMax = enemy.targetTimeMax;
        moveTimeMin = enemy.moveTimeMin;
        moveTimeMax = enemy.moveTimeMax;
        meleeRange = enemy.meleeRange;
        attackDelay = enemy.attackDelay;
        meleeAttackType = enemy.meleeAttackType;

        base.Start();

        // Start the random movement coroutine
        StartCoroutine(RandomMovementRoutine());
    }

    protected override void Update()
    {
        base.Update();

        // ######## Attack section ########
        if (canAttack)
        {
            Attack();
        }

        // ######## Move ########
        if (target != null)
        {
            if (isMovingRandomly)
            {
                MoveRandomly();
            }
            else
            {
                MoveTowardsPlayer();
            }
        }
    }

    private void Attack()
    {
        if (target != null)
        {
            switch (meleeAttackType)
            {
                case MeleeType.SimpleMelee:
                    meleeTypeFunctions.SimpleMelee(target, meleeRange/*, enemy.attackEffectPrefab*/);
                    break;
            }

            canAttack = false;
            StartCoroutine(AttackCooldown());
        }
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackDelay);
        canAttack = true;
    }

    private void MoveTowardsPlayer()
    {
        Vector2 direction = (target.transform.position - transform.position).normalized;

        if (direction.x > 0 && transform.localScale.x < 0)
        {
            Flip();
        }
        else if (direction.x < 0 && transform.localScale.x > 0)
        {
            Flip();
        }

        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }

    private void MoveRandomly()
    {
        if (randomDirection.x > 0 && transform.localScale.x < 0)
        {
            Flip();
        }
        else if (randomDirection.x < 0 && transform.localScale.x > 0)
        {
            Flip();
        }

        rb.MovePosition(rb.position + randomDirection * speed * Time.fixedDeltaTime);
    }

    private IEnumerator RandomMovementRoutine()
    {
        while (true)
        {
            isMovingRandomly = false;
            yield return new WaitForSeconds(randomMoveCooldown);

            isMovingRandomly = true;
            randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            yield return new WaitForSeconds(randomMoveDuration);
        }
    }

    protected override void Die()
    {
        if (enemy.deathEffect != null) Instantiate(enemy.deathEffect, transform.position, Quaternion.identity);
        GetComponent<SpawnBlood>().InstantiateBloodObject(transform.position);

        base.Die();
    }
}
