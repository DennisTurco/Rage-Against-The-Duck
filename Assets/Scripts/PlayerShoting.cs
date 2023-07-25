using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Transform firePosition;
    public GameObject projectile;
    public float fireRate;

    void Update()
    {
        if (fireRate > 0)
            fireRate -= Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && fireRate <= 0) 
            Instantiate(projectile, firePosition.position, firePosition.rotation);
    }
}
