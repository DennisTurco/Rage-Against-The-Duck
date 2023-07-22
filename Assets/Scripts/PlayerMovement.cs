using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Gameplay properties

    // player keyboard input
    private Vector2 moveDirection;
    // player speed
    [SerializeField] public float speed = 200;

    #endregion

    #region Component references

    private Rigidbody2D rb;

    #endregion

    #region Initialisation methods

    // Initialises this component
    // (NB: Is called automatically before the first frame update)
    void Start()
    {
        // Get component references
        rb = GetComponent<Rigidbody2D>();
    }

    #endregion

    #region Gameplay methods

    // Is called automatically every graphics frame
    void Update()
    {
        ProcessInputs();

        // NB: Here, you might want to set the player's animation,
        // e.g. idle or walking

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
        rb.velocity = new Vector2(
           moveDirection.x * speed * Time.fixedDeltaTime,
           moveDirection.y * speed * Time.fixedDeltaTime
       );
    }

    #endregion
}
