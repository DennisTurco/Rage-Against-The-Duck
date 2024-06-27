using System;
using UnityEngine;

public class PlayerMinionOrbiter : PlayerMinion
{
    private float angle = 0;

    protected override void Start()
    {
        base.Start();
        rb.isKinematic = true; // no physics
    }

    protected override void FollowBehavior()
    {
        if (target != null)
        {
            rb.transform.position = new Vector3(
                distance * Mathf.Cos(angle) + target.transform.position.x,
                distance * Mathf.Sin(angle) + target.transform.position.y,
                0);

            angle += speed * Time.deltaTime;
        }
    }
}
