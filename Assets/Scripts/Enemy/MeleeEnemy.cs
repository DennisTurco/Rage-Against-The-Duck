using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/Melee Enemy")]
public class MeleeEnemy : BaseEnemy
{
    [Header("Attack settings")]
    [Tooltip("Melee attack range")]
    public float meleeRange;
    [Tooltip("Time between attacks")]
    public float attackDelay;
    [Tooltip("Attack type")]
    public MeleeType meleeAttackType;
}

public enum MeleeType 
{
    SimpleMelee
};