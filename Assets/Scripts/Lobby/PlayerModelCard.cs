using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerModelCard : MonoBehaviour
{
    [SerializeField] private PlayerStatsGeneric defaultPlayerStats;

    [SerializeField] private TMP_Text playerNameText;
    [SerializeField] private TMP_Text playerDescriptionText;

    // TMP_Text per ogni variabile
    [SerializeField] private TMP_Text movementSpeedText;
    [SerializeField] private TMP_Text attackDamageText;
    [SerializeField] private TMP_Text attackRangeText;
    [SerializeField] private TMP_Text attackSpeedText;
    [SerializeField] private TMP_Text attackDelayText;
    [SerializeField] private TMP_Text luckText;

    // TMP_Text per le differenze
    [SerializeField] private TMP_Text movementSpeedDiffText;
    [SerializeField] private TMP_Text attackDamageDiffText;
    [SerializeField] private TMP_Text attackRangeDiffText;
    [SerializeField] private TMP_Text attackSpeedDiffText;
    [SerializeField] private TMP_Text attackDelayDiffText;
    [SerializeField] private TMP_Text luckDiffText;

    // Componente Image per visualizzare l'immagine del player
    [SerializeField] private Image playerImage;

    public void SetPlayerModelCard(PlayerStatsGeneric playerStats)
    {
        PlayerStatsGeneric currentPlayerStats = GameManager.Instance.gameData.playerStats.playerStats;

        if (playerStats != null && currentPlayerStats != null)
        {
            playerNameText.text = playerStats.playerType.ToString();
            playerDescriptionText.text = GetPlayerDescriptionText(playerStats);

            movementSpeedText.text = $"{FormatValue(playerStats.movementSpeed, defaultPlayerStats.movementSpeed)}X";
            attackDamageText.text = $"{playerStats.attackDamageMin} - {playerStats.attackDamageMax}";
            attackRangeText.text = $"{playerStats.attackRangeMin} - {playerStats.attackRangeMax}";
            attackSpeedText.text = $"{FormatValue(playerStats.attackSpeed, defaultPlayerStats.attackSpeed)}X";
            attackDelayText.text = $"{FormatValue(playerStats.attackRate, defaultPlayerStats.attackRate)}X";
            luckText.text = playerStats.luck.ToString();

            movementSpeedDiffText.text = $"{FormatValueComparason(GetDifferenceValue(playerStats.movementSpeed, currentPlayerStats.movementSpeed), defaultPlayerStats.movementSpeed)}";
            attackDamageDiffText.text = $"{GetDifferenceText(playerStats.attackDamageMax, currentPlayerStats.attackDamageMax)}";
            attackRangeDiffText.text = $"{GetDifferenceText(playerStats.attackRangeMax, currentPlayerStats.attackRangeMax)}";
            attackSpeedDiffText.text = $"{FormatValueComparason(GetDifferenceValue(playerStats.attackSpeed, currentPlayerStats.attackSpeed), defaultPlayerStats.attackSpeed)}";
            attackDelayDiffText.text = $"{FormatValueComparason(GetDifferenceValue(playerStats.attackRate, currentPlayerStats.attackRate), defaultPlayerStats.attackRate)}";
            luckDiffText.text = $"{GetDifferenceText(playerStats.luck, currentPlayerStats.luck)}";

            if (playerStats.playerImage != null)
            {
                playerImage.sprite = currentPlayerStats.playerImage;
                playerImage.gameObject.SetActive(true);
            }
            else
            {
                playerImage.gameObject.SetActive(false);
            }

            if (currentPlayerStats.playerType == PlayerType.Sergio)
            {
                playerImage.transform.localScale = new Vector3(1.4f, 1.4f, 1);
                playerImage.transform.localPosition = new Vector3(playerImage.transform.localPosition.x, 32f, playerImage.transform.localPosition.z);
            }
            else
            {
                playerImage.transform.localScale = new Vector3(1f, 1f, 1f);
                playerImage.transform.localPosition = new Vector3(playerImage.transform.localPosition.x, 9.7f, playerImage.transform.localPosition.z);
            }
        }
        else
        {
            if (playerStats == null) { Debug.LogError("playerStats not assigned"); }
            else { Debug.LogError("CurrentPlayerStats not assigned"); }
        }
    }

    private string FormatValue(float value, float defaultValue)
    {
        float roundedValue = Mathf.Round(value / defaultValue * 100) / 100;
        return roundedValue == 1.0f ? "1" : roundedValue.ToString("F2");
    }

    private string FormatValueComparason(float value, float defaultValue)
    {
        float roundedValue = Mathf.Round(value / defaultValue * 100) / 100;
        if (roundedValue > 0)
        {
            return roundedValue == 1.0f ? "1" : $"<color=green>+{roundedValue.ToString("F2")}X</color>";
        }
        else if (roundedValue < 0)
        {
            return roundedValue == 1.0f ? "1" : $"<color=red>{roundedValue.ToString("F2")}X</color>";
        }
        else
        {
            return roundedValue == 1.0f ? "1" : $"{roundedValue.ToString("F2")}X";
        }
    }

    private string GetDifferenceText(float newValue, float currentValue)
    {
        float difference = newValue - currentValue;
        if (difference > 0)
            return $"<color=green>+{difference.ToString("F2")} </color>";
        else if (difference < 0)
            return $"<color=red>{difference.ToString("F2")} </color>";
        else
            return "0";
    }

    private float GetDifferenceValue(float newValue, float currentValue)
    {
        float difference = newValue - currentValue;
        return difference;
    }

    private string GetPlayerDescriptionText(PlayerStatsGeneric stats)
    {
        return stats.playerDescription;
    }
}
