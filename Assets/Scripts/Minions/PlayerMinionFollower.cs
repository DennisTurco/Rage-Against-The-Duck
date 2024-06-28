using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMinionFollower : PlayerMinion
{
    private int facingDirection = 1;
    private Animator animator;
    private static readonly int WalkAnim = Animator.StringToHash("Walk");
    private static readonly int ShootAnim = Animator.StringToHash("Shoot");

    private void OnEnable()
    {
        PlayerShooting.OnPlayerShooting += Shoot;
    }

    private void OnDisable()
    {
        PlayerShooting.OnPlayerShooting -= Shoot;
    }

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }

    protected override void FollowBehavior()
    {
        if (target != null)
        {
            Vector3 direction = (target.transform.position - transform.position).normalized;
            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

            if (direction.x > 0 && transform.localScale.x < 0)
            {
                Flip();
            }
            else if (direction.x < 0 && transform.localScale.x > 0)
            {
                Flip();
            }

            bool isWalking = distanceToTarget > distance;
            if (isWalking)
            {
                animator.SetTrigger(WalkAnim);
                rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
            }
            else
            {
                animator.ResetTrigger(WalkAnim);
            }
        }
    }

    private void Shoot(GameObject bullet, Vector2 shootingDirection)
    {
        animator.SetTrigger(ShootAnim);

        GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        newBullet.name = "PlayerBullet";

        float angle = Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg;
        newBullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    // flip sprite on change direction
    private void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);

        if (healthBar != null)
        {
            healthBar.transform.localScale = new Vector3(healthBar.transform.localScale.x * -1, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
        }
    }

}
