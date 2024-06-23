using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Upgrades : MonoBehaviour, ICollectible
{
    //[SerializeField] private string upgrade;              // upgrade name
    [SerializeField] private UpgradeName upgrade;           // upgrade name
    [SerializeField] private List<UpgradeGeneric> upgrades; // whole list of upgrades
    private UpgradeGeneric currentUpgrade;                  // current upgrade selected

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
    }

    public void Collect()
    {
        Debug.Log("Upgrade collected: " + upgrade);

        GameObject obj = GameObject.FindWithTag("Player");
        if (obj == null)
        {
            Debug.LogError("Cannot obtain 'Player' tag");
            return; // Early return to avoid null reference exceptions
        }

        PlayerStats stats = obj.GetComponent<PlayerStats>();
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
                stats.UpdateMovementSpeed(stats.MovementSpeed + up.upgradeValue);
            }
            else if (up.statisticName == StatisticName.AttackDamage)
            {
                stats.UpdateAttackDamage(stats.AttackDamageMin + up.upgradeValue, stats.AttackDamageMax + up.upgradeValue);
            }
            else if (up.statisticName == StatisticName.AttackSpeed)
            {
                stats.UpdateAttackSpeed(stats.AttackSpeed + up.upgradeValue);
            }
            else if (up.statisticName == StatisticName.AttackRange)
            {
                stats.UpdateAttackRange(stats.AttackRangeMin + up.upgradeValue, stats.AttackRangeMax + up.upgradeValue);
            }
            else if (up.statisticName == StatisticName.AttackRate)
            {
                stats.UpdateAttackRate(stats.AttackRate + up.upgradeValue);
            }
            else if (up.statisticName == StatisticName.Luck)
            {
                stats.UpdateLuck(stats.Luck + up.upgradeValue);
            }
            else
            {
                throw new ArgumentException("Upgrade stat doesn't exist");
            }
        }

        GameManager.Instance.ShowFloatingText($"{upgrade}", 25, Color.green, transform.position, Vector3.up * 100, 1.5f);
        Destroy(gameObject);
    }
}
