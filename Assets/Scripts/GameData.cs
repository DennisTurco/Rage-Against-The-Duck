using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Data/GameData", order = 1)]
public class GameData : ScriptableObject
{
    [Header("Player")]
    [SerializeField] public PlayerType playerType = PlayerType.Duck; // last player selected
    [SerializeField] public PlayerStatsData playerStats;

    [Header("Inventory")]
    [SerializeField] public int coins;
    [SerializeField] public int bombs;
    [SerializeField] public int keys;

    [Header("Minions")]
    [SerializeField] public List<ItemName> minions;
}