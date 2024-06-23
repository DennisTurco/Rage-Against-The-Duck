using UnityEngine;

[CreateAssetMenu]
public class PlayerStatsGeneric : ScriptableObject
{
    [Header("Movement Stats")]
    [SerializeField] public float movementSpeed;

    [Header("Projectile Attack Stats")]
    [SerializeField] public float attackDamageMin, attackDamageMax;
    [SerializeField] public float attackRangeMin, attackRangeMax;
    [SerializeField] public float attackSpeed;
    [SerializeField] public float attackRate;

    [Header("Miscellaneous Stats")]
    //[SerializeField] public int health;
    [SerializeField] public float luck;
}