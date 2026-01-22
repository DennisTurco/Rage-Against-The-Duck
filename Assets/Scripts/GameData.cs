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
    public int bread;
    public int bombs;
    public int keys;

    [Header("Player Values")]
    public int currentHealth;
    public int maxHealth;

    [Header("Minions")]
    public List<ItemName> minions;

    public void ReloadGameDataValues()
    {
        ReloadPlayerStats();
        ReloadPlayerInventory();
        ReloadPlayerHealth();
    }

    private void ReloadPlayerStats()
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

    private void ReloadPlayerInventory()
    {
        bread = 0;
        bombs = 0;
        minions = new List<ItemName>();
    }

    private void ReloadPlayerHealth()
    {
        maxHealth = playerStats.playerStats.health;
        currentHealth = maxHealth;
    }
}