using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/Summoner Enemy")]
public class SummonerEnemy : BaseEnemy
{
    [Header("Summoning Settings")]
    [Tooltip("Prefab del Melee Minion")]
    public GameObject meleeMinionPrefab;
    [Tooltip("Prefab del Range Minion")]
    public GameObject rangeMinionPrefab;
    [Tooltip("Intervallo di tempo tra le evocazioni dei minion")]
    public float summonInterval;
    [Tooltip("Numero di Melee Minion evocati")]
    public int numberOfMeleeMinions;
    [Tooltip("Numero di Range Minion evocati")]
    public int numberOfRangeMinions;
    [Tooltip("Numero di minion evocati alla morte")]
    public int numberOfMinionsOnDeath;

    [Header("Ranged Attack Settings")]
    [Tooltip("Il Summoner può sparare")]
    public bool canShoot;
    [Tooltip("Tipo di sparo")]
    public ShootingType shootingType;
    [Tooltip("Prefab del proiettile")]
    public GameObject bulletPrefab;
    [Tooltip("Tempo minimo tra gli attacchi a distanza")]
    public float shootTimeMin;
    [Tooltip("Tempo massimo tra gli attacchi a distanza")]
    public float shootTimeMax;

    [Header("Movement Settings")]
    [Tooltip("Distanza desiderata dal giocatore")]
    public float desiredDistance = 5f;
}
