using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private ParticleSystem dust;
    [SerializeField] private PlayerStats stats;
    private Vector2 moveDirection;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Is called automatically every graphics frame
    private void Update()
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
    private void SwapSprite()
    {
        // Right
        if (moveDirection.x > 0)
        {
            transform.localScale = new Vector2(
                Mathf.Abs(transform.localScale.x),
                transform.localScale.y
            );
        }
        // Left
        else if (moveDirection.x < 0)
        {
            transform.localScale = new Vector2(
                -1 * Mathf.Abs(transform.localScale.x),
                transform.localScale.y
            );
        }
        // Up
        else if (moveDirection.y > 0)
        {
            transform.localScale = new Vector2(
                transform.localScale.x,
                Mathf.Abs(transform.localScale.y)
            );
        }
        // Down
        else if (moveDirection.y < 0)
        {
            transform.localScale = new Vector2(
                transform.localScale.x,
                Mathf.Abs(transform.localScale.y)
            );
        }
    }

    // Is called automatically every physics step
    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        // play dust particle effect on move
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical")) dust.Play();

        rb.MovePosition(rb.position + moveDirection * stats.MovementSpeed * Time.fixedDeltaTime);
    }
}
