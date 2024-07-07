using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Data/GameData", order = 1)]
public class GameData : ScriptableObject
{
    [Header("Player")]
    public PlayerType playerType; // last player selected
    public PlayerStatsData playerStats;

    [Header("Player Stats")]
    public float movementSpeed;
    public float attackDamageMin;
    public float attackDamageMax;
    public float attackSpeed;
    public float attackRangeMin;
    public float attackRangeMax;
    public float attackRate;
    public float luck;

    [Header("Inventory")]
    public int coins;
    public int bombs;
    public int keys;

    [Header("Minions")]
    public List<ItemName> minions;

    public void ReloadPlayerStats()
    {
        movementSpeed = playerStats.playerStats.movementSpeed;
        attackDamageMin = playerStats.playerStats.attackDamageMin;
        attackDamageMax = playerStats.playerStats.attackDamageMax;
        attackRangeMin = playerStats.playerStats.attackRangeMin;
        attackRangeMax = playerStats.playerStats.attackRangeMax;
        attackSpeed = playerStats.playerStats.attackSpeed;
        attackRate = playerStats.playerStats.attackRate;
        luck = playerStats.playerStats.luck;
    }
}