using UnityEngine;

// CreateAssetMenu allows to create new LootSpawn object into unity editor: right click -> LootSpawn
[CreateAssetMenu(menuName = "Enemies/Base Enemy")]
public class BaseEnemy : ScriptableObject // ScriptableObject allows to work in the unity editor
{
    [Header("Script settings")]
    public Sprite entitySprite;
    //public FlickerEffect flashEffect;
    public GameObject deathEffect;

    [Header("Enemy settings")]
    [Tooltip("target object")]
    public float speed;
    [Tooltip("Distance from target")]
    public float distance;
    [Tooltip("Health")]
    public FloatingHealthBar healthBar;
    public float maxHealth;
    public float health;

    [Header("Attack settings")]
    [Tooltip("min time between target change")]
    public float targetTimeMin;
    [Tooltip("max time between target change")]
    public float targetTimeMax;
    [Tooltip("min time between target tracking")]
    public float trackTimeMin;
    [Tooltip("max time between target tracking")]
    public float trackTimeMax;

    [Header("Movement settings")]
    [Tooltip("min time between move")]
    public float moveTimeMin;
    [Tooltip("max time between move")]
    public float moveTimeMax;

    [TextArea]
    [Tooltip("Notes")]
    public string notes;
}