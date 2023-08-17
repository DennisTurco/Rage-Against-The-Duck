using System;
using UnityEngine;

public class ItemCoin : MonoBehaviour, ICollectible
{
    public static event Action OnCoinCollected;
    private Rigidbody2D rb;

    private bool hasTarget;
    private Vector3 targetPosition;
    [SerializeField] private float moveSpeed = 5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Collect()
    {
        Debug.Log("Coin Collected");
        Destroy(gameObject);
        OnCoinCollected?.Invoke();
    }

    private void FixedUpdate()
    {
        if (hasTarget)
        {
            Vector2 targetDirection = (targetPosition - transform.position).normalized;
            rb.velocity = new Vector2(targetDirection.x, targetDirection.y) * moveSpeed;
        }
    }

    public void SetTarget(Vector3 position)
    {
        targetPosition = position;
        hasTarget = true;
    }
}
