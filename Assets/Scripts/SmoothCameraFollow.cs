using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    public Vector3 offset = new Vector3(0f, 0f, -10f);
    public float smoothTime = 0.25f;

    private Vector3 velocity = Vector3.zero;
    private Transform target; // player to follow

    private void Start()
    {
        StartCoroutine(InitializeAfterPlayerInitialized());
    }

    private IEnumerator InitializeAfterPlayerInitialized()
    {
        yield return new WaitUntil(() => GameManager.Instance != null && GameManager.Instance.isInitialized && GameManager.Instance.playerInitialized);
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
