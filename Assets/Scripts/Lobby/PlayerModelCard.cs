using TMPro;
using UnityEngine;

public class PlayerModelCard : MonoBehaviour
{
    [SerializeField] private TMP_Text playerNameText;
    [SerializeField] private TMP_Text playerDescriptionText;
    [SerializeField] private TMP_Text playerStatsText;

    public void SetPlayerModelCard(PlayerStatsGeneric playerStats)
    {
        if (playerStats != null)
        {
            playerNameText.text = playerStats.playerType.ToString();

            playerDescriptionText.text = GetPlayerDescriptionText(playerStats);

            playerStatsText.text = GetPlayerStatsText(playerStats);
        }
        else
        {
            Debug.LogError("PlayerStatsGeneric not assigned.");
        }
    }

    private string GetPlayerStatsText(PlayerStatsGeneric stats)
    {
        return $"Movement Speed: {stats.movementSpeed}\n" +
               $"Attack Damage: {stats.attackDamageMin} - {stats.attackDamageMax}\n" +
               $"Attack Range: {stats.attackRangeMin} - {stats.attackRangeMax}\n" +
               $"Attack Speed: {stats.attackSpeed}\n" +
               $"Attack Delay: {stats.attackRate}\n" +
               $"Luck: {stats.luck}";
    }

    private string GetPlayerDescriptionText(PlayerStatsGeneric stats)
    {
        return stats.playerDescription;
    }
}
