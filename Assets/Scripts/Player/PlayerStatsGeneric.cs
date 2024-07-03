using UnityEngine;

[CreateAssetMenu]
public class PlayerStatsGeneric : ScriptableObject
{
    [Header("Movement Stats")]
    [SerializeField] public string statsPlayerName;
    [SerializeField] public float movementSpeed;

    [Header("Projectile Attack Stats")]
    [SerializeField] public float attackDamageMin;
    [SerializeField] public float attackDamageMax;
    [SerializeField] public float attackRangeMin;
    [SerializeField] public float attackRangeMax;
    [SerializeField] public float attackSpeed;
    [Tooltip("More is low and more the attackRate is good")]
    [SerializeField] public float attackRate;

    [Header("Miscellaneous Stats")]
    //[SerializeField] public int health;
    [SerializeField] public float luck;
}