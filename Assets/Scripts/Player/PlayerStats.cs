using System;
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

    private void Start()
    {
        // if globalStats != null it means the player has chosen the player and so I have to get them, otherwise the player is choosing the player inside the lobby
        var globalStats = GameManager.Instance.playerStats;
        if (globalStats != null)
        {
            playerStats = globalStats.playerStats;
        }
        else
        {
            GameManager.Instance.playerStats = this;
        }

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
        UpdateAttackRange(AttackRangeMin, AttackRangeMax);
        UpdateAttackRate(AttackRate);
        UpdateLuck(Luck);
    }

    //public void CopyFrom(PlayerStats other)
    //{
    //    MovementSpeed = other.MovementSpeed;
    //    AttackDamageMin = other.AttackDamageMin;
    //    AttackDamageMax = other.AttackDamageMax;
    //    AttackRangeMin = other.AttackRangeMin;
    //    AttackRangeMax = other.AttackRangeMax;
    //    AttackSpeed = other.AttackSpeed;
    //    AttackRate = other.AttackRate;
    //    Luck = other.Luck;

    //    UpdateMovementSpeed(MovementSpeed);
    //    UpdateAttackDamage(AttackDamageMin, AttackDamageMax);
    //    UpdateAttackSpeed(AttackSpeed);
    //    UpdateAttackRange(AttackRangeMin, AttackRangeMax);
    //    UpdateAttackRate(AttackRate);
    //    UpdateLuck(Luck);
    //}


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
