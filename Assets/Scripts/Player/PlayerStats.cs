using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] public PlayerStatsGeneric playerStats;
    public float MovementSpeed { get; set; }
    public float AttackDamageMin { get; set; }
    public float AttackDamageMax { get; set; }
    public float AttackRangeMin { get; set; }
    public float AttackRangeMax { get; set; }
    public float AttackSpeed { get; set; }
    public float AttackRate { get; set; }
    public float Luck { get; set; }

    [SerializeField] private TextStatMovementSpeed textStatMovementSpeed;
    [SerializeField] private TextStatAttackDamage textStatAttackDamage;
    [SerializeField] private TextStatAttackSpeed textStatAttackSpeed;
    [SerializeField] private TextStatAttackRange textStatAttackRange;
    [SerializeField] private TextStatAttackRate textStatAttackRate;
    //[SerializeField] private TextStatLuck textStatLuck;

    private void Awake()
    {
        MovementSpeed = playerStats.movementSpeed;
        AttackDamageMin = playerStats.attackDamageMin;
        AttackDamageMax = playerStats.attackDamageMax;
        AttackRangeMin = playerStats.attackRangeMin;
        AttackRangeMax = playerStats.attackRangeMax;
        AttackSpeed = playerStats.attackSpeed;
        AttackRate = playerStats.attackRate;
        Luck = playerStats.luck;

        UpdateMovementSpeed(MovementSpeed);
        UpdateAttackDamage(AttackDamageMin, AttackDamageMax);
        UpdateAttackSpeed(AttackSpeed);
        UpdateAttackRange(AttackRangeMin, AttackDamageMax);
        UpdateAttackRate(AttackRate);
        UpdateLuck(Luck);
    }

    public void UpdateMovementSpeed(float movementSpeed)
    {
        MovementSpeed = movementSpeed;
        textStatMovementSpeed.SetText(MovementSpeed);
    }
    public void UpdateAttackDamage(float attackDamageMin, float attackDamageMax)
    {
        AttackDamageMin = attackDamageMin;
        AttackDamageMax = attackDamageMax;
        textStatAttackDamage.SetText(AttackDamageMin, AttackDamageMax);

    }
    public void UpdateAttackSpeed(float attackSpeed)
    {
        AttackSpeed = attackSpeed;
        textStatAttackSpeed.SetText(AttackSpeed);
    }
    public void UpdateAttackRange(float attackRangeMin, float attackRangeMax)
    {
        AttackRangeMin = attackRangeMin;
        AttackRangeMax = attackRangeMax;
        textStatAttackRange.SetText(AttackRangeMin, AttackRangeMax);
    }
    public void UpdateAttackRate(float attackRate)
    {
        AttackRate = attackRate;
        textStatAttackRate.SetText(AttackRate);
    }
    public void UpdateLuck(float luck)
    {
        Luck = luck;
        //TODO: add
    }
}
