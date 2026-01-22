using System;
using UnityEngine;

public class ItemBread : MonoBehaviour
{
    public static event Action UpdateBreadText;

    private Rigidbody2D rb;
    private bool hasTarget;
    private Vector3 targetPosition;
    [SerializeField] private float moveSpeed = 5f;

    public void CollectItemBread()
    {
        GameManager.Instance.bread++;
        UpdateBreadText?.Invoke();
    }

    public static void CollectItemBread(int bread)
    {
        GameManager.Instance.bread += bread;
        UpdateBreadText?.Invoke();
    }

    public static void UseItemBread(int bread)
    {
        GameManager.Instance.bread -= bread;
        UpdateBreadText?.Invoke();
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
            rb.linearVelocity = new Vector2(targetDirection.x, targetDirection.y) * moveSpeed;
        }
    }

    public void SetTarget(Vector3 position)
    {
        targetPosition = position;
        hasTarget = true;
    }
}
