using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Data/GameData", order = 1)]
public class GameData : ScriptableObject
{
    //[SerializeField] public PlayerType playerType; // last player selected
    [SerializeField] public PlayerStatsData playerStats;
}