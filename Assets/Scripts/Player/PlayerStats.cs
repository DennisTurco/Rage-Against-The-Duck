using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public PlayerStatsData playerStatsData;
    private TextStatMovementSpeed textStatMovementSpeed;
    private TextStatAttackDamage textStatAttackDamage;
    private TextStatAttackSpeed textStatAttackSpeed;
    private TextStatAttackRange textStatAttackRange;
    private TextStatAttackRate textStatAttackRate;
    //private TextStatLuck textStatLuck;
    private void Start()
    {
        // if globalStats != null it means the player has chosen the player and so I have to get them, otherwise the player is choosing the player inside the lobby
        var globalStats = GameManager.Instance.gameData.playerStats;
        if (globalStats != null)
        {
            playerStatsData = globalStats;
        }
        else
        {
            GameManager.Instance.gameData.playerStats = this.playerStatsData;
        }
        playerStatsData.MovementSpeed = playerStatsData.playerStats.movementSpeed;
        playerStatsData.AttackDamageMin = playerStatsData.playerStats.attackDamageMin;
        playerStatsData.AttackDamageMax = playerStatsData.playerStats.attackDamageMax;
        playerStatsData.AttackRangeMin = playerStatsData.playerStats.attackRangeMin;
        playerStatsData.AttackRangeMax = playerStatsData.playerStats.attackRangeMax;
        playerStatsData.AttackSpeed = playerStatsData.playerStats.attackSpeed;
        playerStatsData.AttackRate = playerStatsData.playerStats.attackRate;
        playerStatsData.Luck = playerStatsData.playerStats.luck;

        textStatMovementSpeed = GameManager.Instance.textStatMovementSpeed;
        textStatAttackDamage = GameManager.Instance.textStatAttackDamage;
        textStatAttackSpeed = GameManager.Instance.textStatAttackSpeed;
        textStatAttackRange = GameManager.Instance.textStatAttackRange;
        textStatAttackRate = GameManager.Instance.textStatAttackRate;
        
        UpdateMovementSpeed(playerStatsData.MovementSpeed);
        UpdateAttackDamage(playerStatsData.AttackDamageMin, playerStatsData.AttackDamageMax);
        UpdateAttackSpeed(playerStatsData.AttackSpeed);
        UpdateAttackRange(playerStatsData.AttackRangeMin, playerStatsData.AttackRangeMax);
        UpdateAttackRate(playerStatsData.AttackRate);
        UpdateLuck(playerStatsData.Luck);
    }

    public void UpdateMovementSpeed(float movementSpeed)
    {
        playerStatsData.MovementSpeed = movementSpeed;
        textStatMovementSpeed.SetText(playerStatsData.MovementSpeed);
    }

    public void UpdateAttackDamage(float attackDamageMin, float attackDamageMax)
    {
        playerStatsData.AttackDamageMin = attackDamageMin;
        playerStatsData.AttackDamageMax = attackDamageMax;
        textStatAttackDamage.SetText(playerStatsData.AttackDamageMin, playerStatsData.AttackDamageMax);
    }

    public void UpdateAttackSpeed(float attackSpeed)
    {
        playerStatsData.AttackSpeed = attackSpeed;
        textStatAttackSpeed.SetText(playerStatsData.AttackSpeed);
    }

    public void UpdateAttackRange(float attackRangeMin, float attackRangeMax)
    {
        playerStatsData.AttackRangeMin = attackRangeMin;
        playerStatsData.AttackRangeMax = attackRangeMax;
        textStatAttackRange.SetText(playerStatsData.AttackRangeMin, playerStatsData.AttackRangeMax);
    }

    public void UpdateAttackRate(float attackRate)
    {
        playerStatsData.AttackRate = attackRate;
        textStatAttackRate.SetText(playerStatsData.AttackRate);
    }

    public void UpdateLuck(float luck)
    {
        playerStatsData.Luck = luck;
        //TODO: add
    }
}
