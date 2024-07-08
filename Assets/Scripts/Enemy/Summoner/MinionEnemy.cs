using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/Minion Enemy")]
public class MinionEnemy : BaseEnemy
{
    [Header("Melee Attack Settings")]
    [Tooltip("Distanza massima per attaccare corpo a corpo")]
    public float meleeRange;

    [Tooltip("Tempo di attesa tra un attacco corpo a corpo e l'altro in secondi")]
    public float attackDelay;

    [Header("Ranged Attack Settings")]
    [Tooltip("Indica se il minion può sparare proiettili")]
    public bool canShoot;

    [Tooltip("Prefab del proiettile sparato dal minion")]
    public GameObject bulletPrefab;

    [Tooltip("Tipo di sparo del minion")]
    public ShootingType shootingType;

    [Tooltip("Tempo minimo tra gli attacchi a distanza")]
    public float shootTimeMin;

    [Tooltip("Tempo massimo tra gli attacchi a distanza")]
    public float shootTimeMax;

    [Tooltip("Durata dell'attacco corpo a corpo in secondi")]
    public float meleeAttackDuration;

    [Tooltip("Durata dell'attacco a distanza in secondi")]
    public float shootingDuration;
}
