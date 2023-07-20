using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
 #region Gameplay properties

    // Horizontal player keyboard input
    //  -1 = Left
    //   0 = No input
    //   1 = Right
    private float playerInputX = 0;
    private float playerInputY = 0;

    // Horizontal player speed
    [SerializeField] private float speed = 250;

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
        // Detect and store horizontal player input   
        playerInputX = Input.GetAxisRaw("Horizontal");
        playerInputY = Input.GetAxisRaw("Vertical");

        // NB: Here, you might want to set the player's animation,
        // e.g. idle or walking

        // Swap the player sprite scale to face the movement direction
        SwapSprite();
    }

    // Swap the player sprite scale to face the movement direction
    void SwapSprite()
    {
        // Right
        if (playerInputX > 0)
        {
            transform.localScale = new Vector3(
                Mathf.Abs(transform.localScale.x),
                transform.localScale.y,
                transform.localScale.z
            );
        }
        // Left
        else if (playerInputX < 0)
        {
            transform.localScale = new Vector3(
                -1 * Mathf.Abs(transform.localScale.x),
                transform.localScale.y,
                transform.localScale.z
            );
        }
        // Up
        else if (playerInputY > 0)
        {
            transform.localScale = new Vector3(
                Mathf.Abs(transform.localScale.y),
                transform.localScale.y,
                transform.localScale.z
            );
        }
        // Down
        else if (playerInputY < 0)
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
        rb.velocity = new Vector2(
            playerInputX * speed * Time.fixedDeltaTime,
            playerInputY * speed * Time.fixedDeltaTime
        );
    }

    #endregion
}
