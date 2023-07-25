using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Vector2 moveDirection;
    [SerializeField] public float speed = 200;
    [SerializeField] public Rigidbody2D rb;

    // Is called automatically every graphics frame
    void Update()
    {
        ProcessInputs();

        // Swap the player sprite scale to face the movement direction
        SwapSprite();
    }

    private void ProcessInputs()
    {
        // Detect and store horizontal player input   
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.y = Input.GetAxisRaw("Vertical");

        // with normalized in any direction the speed will be the same
        moveDirection.Normalize();
    }


    // Swap the player sprite scale to face the movement direction
    void SwapSprite()
    {
        // Right
        if (moveDirection.x > 0)
        {
            transform.localScale = new Vector3(
                Mathf.Abs(transform.localScale.x),
                transform.localScale.y,
                transform.localScale.z
            );
        }
        // Left
        else if (moveDirection.x < 0)
        {
            transform.localScale = new Vector3(
                -1 * Mathf.Abs(transform.localScale.x),
                transform.localScale.y,
                transform.localScale.z
            );
        }
        // Up
        else if (moveDirection.y > 0)
        {
            transform.localScale = new Vector3(
                Mathf.Abs(transform.localScale.y),
                transform.localScale.y,
                transform.localScale.z
            );
        }
        // Down
        else if (moveDirection.y < 0)
        {
            transform.localScale = new Vector3(
                -1 * Mathf.Abs(transform.localScale.y),
                transform.localScale.y,
                transform.localScale.z
            );
        }
    }

    // Is called automatically every physics step
    void FixedUpdate()
    {
        Move();
    }


    private void Move()
    {
        rb.MovePosition(rb.position + moveDirection * speed * Time.fixedDeltaTime);
    }

}
