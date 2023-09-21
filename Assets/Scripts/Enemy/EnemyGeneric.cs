using UnityEngine;

// CreateAssetMenu allows to create new LootSpawn object into unity editor: right click -> LootSpawn
[CreateAssetMenu]
public class EnemyGeneric : ScriptableObject // ScriptableObject allows to work in the unity editor
{
    [Header("Script settings")]
    [SerializeField] public Sprite entitySprite;
    [SerializeField] public GameObject bulletPrefab;
    //[SerializeField] public FlickerEffect flashEffect;
    [SerializeField] public GameObject deathBloodEffect;
    [SerializeField] public GameObject deathEffect;

    [Header("Enemy settings")]
    [Tooltip("target object")]
    [SerializeField] public float speed;
    [SerializeField] public float bulletSpeed;
    [Tooltip("Distance from target")]
    [SerializeField] public float distance;
    [Tooltip("Health")]
    [SerializeField] public FloatingHealthBar healthBar;
    [SerializeField] public float maxHealth;
    [SerializeField] public float health;

    [Header("Shooting settings")]
    [SerializeField] public bool canShoot;
    [SerializeField] public ShootingType shootingType;
    [Tooltip("min time between target change")]
    [SerializeField] public float targetTimeMin;
    [Tooltip("max time between target change")]
    [SerializeField] public float targetTimeMax;
    [Tooltip("min time between shots")]
    [SerializeField] public float shootTimeMin;
    [Tooltip("max time between shots")]
    [SerializeField] public float shootTimeMax;
    [Tooltip("min time between target tracking")]
    [SerializeField] public float trackTimeMin;
    [Tooltip("max time between target tracking")]
    [SerializeField] public float trackTimeMax;

    [Header("Movement settings")]
    [Tooltip("min time between move")]
    [SerializeField] public float moveTimeMin;
    [Tooltip("max time between move")]
    [SerializeField] public float moveTimeMax;

    [TextArea]
    [Tooltip("Notes")]
    [SerializeField] public string notes;

    public EnemyGeneric(Sprite entitySprite, GameObject bulletPrefab, GameObject deathBloodEffect, GameObject deathEffect, float speed, float bulletSpeed, float distance, FloatingHealthBar healthBar, float maxHealth, float health, bool canShoot, ShootingType shootingType, float targetTimeMin, float targetTimeMax, float shootTimeMin, float shootTimeMax, float trackTimeMin, float trackTimeMax, float moveTimeMin, float moveTimeMax, string notes)
    {
        this.entitySprite = entitySprite;
        this.bulletPrefab = bulletPrefab;
        this.deathBloodEffect = deathBloodEffect;
        this.deathEffect = deathEffect;
        this.speed = speed;
        this.bulletSpeed = bulletSpeed;
        this.distance = distance;
        this.healthBar = healthBar;
        this.maxHealth = maxHealth;
        this.health = health;
        this.canShoot = canShoot;
        this.shootingType = shootingType;
        this.targetTimeMin = targetTimeMin;
        this.targetTimeMax = targetTimeMax;
        this.shootTimeMin = shootTimeMin;
        this.shootTimeMax = shootTimeMax;
        this.trackTimeMin = trackTimeMin;
        this.trackTimeMax = trackTimeMax;
        this.moveTimeMin = moveTimeMin;
        this.moveTimeMax = moveTimeMax;
        this.notes = notes;
    }
}

public enum ShootingType
{
    SimpleShooting,
    DiagonalShooting,
    FourAxisShooting
};