using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class PlayerMinion : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float distance;
    private Rigidbody2D rb;
    public GameObject target;
    private float angle = 0;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb.transform.position = new Vector3(distance * Mathf.Cos(angle) + target.transform.position.x, distance * Mathf.Sin(angle) + target.transform.position.y);

        angle += speed * Time.deltaTime;
    }

}
