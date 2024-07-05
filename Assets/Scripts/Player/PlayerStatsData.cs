using Unity.VisualScripting;

[System.Serializable]
public class PlayerStatsData
{
    public PlayerStatsGeneric playerStats;
    public float MovementSpeed { get; set; }
    public float AttackDamageMin { get; set; }
    public float AttackDamageMax { get; set; }
    public float AttackRangeMin { get; set; }
    public float AttackRangeMax { get; set; }
    public float AttackSpeed { get; set; }
    public float AttackRate { get; set; }
    public float Luck { get; set; }
}
