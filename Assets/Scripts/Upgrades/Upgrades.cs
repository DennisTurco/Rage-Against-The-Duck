using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Upgrades : MonoBehaviour, ICollectible
{
    //[SerializeField] private string upgrade;              // upgrade name
    [SerializeField] private UpgradeName upgrade;           // upgrade name
    [SerializeField] private List<UpgradeGeneric> upgrades; // whole list of upgrades
    private UpgradeGeneric currentUpgrade;                  // current upgrade selected

    [SerializeField] private Animator collectAnimation;

    // upgradeTitle has 2 childs: name and description
    [SerializeField] private GameObject upgradeTitle;
    [SerializeField] private float titleTimeDuration = 5f;
    private TextMeshProUGUI upgradeNameText; 
    private TextMeshProUGUI upgradeDescriptionText;

    private bool isCollected = false;
    private GameObject playerObject = null;

    private void Start()
    {
        //upgrade = this.name;

        // getting the corret upgrade from the list of the upgrades from the name
        foreach (UpgradeGeneric up in upgrades)
        {
            if (upgrade.Equals(up.upgradeName))
            {
                currentUpgrade = up;
                break;
            }
        }

        if (currentUpgrade == null)
        {
            throw new Exception("Upgrade is null");
        }

        upgradeNameText = upgradeTitle.transform.Find("UpgradeName").GetComponent<TextMeshProUGUI>();
        upgradeDescriptionText = upgradeTitle.transform.Find("UpgradeDescription").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (playerObject != null && isCollected)
        {
            MoveObjectAboveHead();
        }
    }

    public void Collect()
    {
        if (isCollected) return;

        Debug.Log("Upgrade collected: " + upgrade);

        playerObject = GameObject.FindWithTag("Player");
        if (playerObject == null)
        {
            Debug.LogError("Cannot obtain 'Player' tag");
            return; // Early return to avoid null reference exceptions
        }

        PlayerStats stats = playerObject.GetComponent<PlayerStats>();
        if (stats == null)
        {
            Debug.LogError("Cannot obtain 'PlayerStats' component");
            return; // Early return to avoid null reference exceptions
        }

        // a single upgrade may edit more than one stat
        foreach (var up in currentUpgrade.statisticUpgrades)
        {
            if (up.statisticName == StatisticName.MovementSpeed)
            {
                stats.UpdateMovementSpeed(stats.playerStatsData.MovementSpeed + up.upgradeValue);
            }
            else if (up.statisticName == StatisticName.AttackDamage)
            {
                stats.UpdateAttackDamage(stats.playerStatsData.AttackDamageMin + up.upgradeValue, stats.playerStatsData.AttackDamageMax + up.upgradeValue);
            }
            else if (up.statisticName == StatisticName.AttackSpeed)
            {
                stats.UpdateAttackSpeed(stats.playerStatsData.AttackSpeed + up.upgradeValue);
            }
            else if (up.statisticName == StatisticName.AttackRange)
            {
                stats.UpdateAttackRange(stats.playerStatsData.AttackRangeMin + up.upgradeValue, stats.playerStatsData.AttackRangeMax + up.upgradeValue);
            }
            else if (up.statisticName == StatisticName.AttackRate)
            {
                stats.UpdateAttackRate(stats.playerStatsData.AttackRate + up.upgradeValue);
            }
            else if (up.statisticName == StatisticName.Luck)
            {
                stats.UpdateLuck(stats.playerStatsData.Luck + up.upgradeValue);
            }
            else
            {
                throw new ArgumentException("Upgrade stat doesn't exist");
            }
        }

        isCollected = true;

        // Start the animation
        collectAnimation.SetTrigger("Collect");

        UpdateTextUpgradeTitle(currentUpgrade.upgradeName.ToString(), currentUpgrade.upgradeDescription);

        // make the upgrade object smaller
        Transform upgradeTransform = transform;
        upgradeTransform.parent = playerObject.transform;
        upgradeTransform.localScale = Vector3.one * 0.8f; // Adjust scale factor as needed

        StartCoroutine(WaitForDestroy());
    }

    private void MoveObjectAboveHead()
    {
        Vector3 originalPosition = gameObject.transform.position;
        Vector3 targetPosition = playerObject.transform.position + Vector3.up; // Adjust the height as needed
        float duration = 1.0f; // Adjust animation duration as needed

        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            gameObject.transform.position = Vector3.Lerp(originalPosition, targetPosition, elapsed / duration);
            elapsed += Time.deltaTime;
        }

        // Ensure it reaches the exact target position
        gameObject.transform.position = targetPosition;
    }

    private void UpdateTextUpgradeTitle(string name, string description)
    {
        if (upgradeNameText != null && upgradeDescriptionText != null)
        {
            upgradeNameText.text = name;
            upgradeDescriptionText.text = description;
        }

        upgradeTitle.gameObject.SetActive(true);
        StartCoroutine(HideTextUpgradeTitleAfterDelay());
    }

    private IEnumerator HideTextUpgradeTitleAfterDelay()
    {
        yield return new WaitForSeconds(titleTimeDuration);
        upgradeTitle.gameObject.SetActive(false);
    }

    // before destroy this game object we have to wait the animations and the title text
    private IEnumerator WaitForDestroy()
    {
        // wait and destroy it
        yield return new WaitForSeconds(titleTimeDuration);

        Destroy(gameObject);
    }
}
