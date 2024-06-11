using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/Range Enemy")]
public class RangeEnemy : BaseEnemy
{
    [Header("Script settings")]
    public GameObject bulletPrefab;

    [Header("Enemy settings")]
    public float bulletSpeed;

    [Header("Attack settings")]
    public bool canShoot;
    public ShootingType shootingType;
    [Tooltip("min time between shots")]
    public float shootTimeMin;
    [Tooltip("max time between shots")]
    public float shootTimeMax;
}

public enum ShootingType
{
    SimpleShooting,
    DiagonalShooting,
    FourAxisShooting
};