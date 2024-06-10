using System;
using UnityEngine;

public class ItemCoin : MonoBehaviour
{
    public static event Action UpdateCoinText;

    private Rigidbody2D rb;
    private bool hasTarget;
    private Vector3 targetPosition;
    [SerializeField] private float moveSpeed = 5f;

    public void CollectItemCoin()
    {
        GameManager.Instance.coins++;
        UpdateCoinText?.Invoke();
    }

    public static void CollectItemCoin(int coins)
    {
        GameManager.Instance.coins += coins;
        UpdateCoinText?.Invoke();
    }

    public static void UseItemCoin(int coins)
    {
        GameManager.Instance.coins -= coins;
        UpdateCoinText?.Invoke();
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
