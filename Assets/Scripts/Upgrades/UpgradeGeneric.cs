using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class UpgradeGeneric : ScriptableObject
{
    [SerializeField] public Sprite upgradeSprite;
    [SerializeField] public UpgradeName upgradeName; // upgrade title
    [SerializeField] public string upgradeDescription;

    [SerializeField] public List<StatisticUpgrade> statisticUpgrades; // combined list of stats and values

    [Range(0f, 100f)]
    [SerializeField] public float dropChance;
}

[Serializable]
public class StatisticUpgrade
{
    public StatisticName statisticName;
    public float upgradeValue;
}