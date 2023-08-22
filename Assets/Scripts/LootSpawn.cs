using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

// CreateAssetMenu allows to create new LootSpawn object into unity editor: right click -> LootSpawn
[CreateAssetMenu]
public class LootSpawn : ScriptableObject // ScriptableObject allows to work in the unity editor
{
    [SerializeField] public Sprite lootSprite;
    [SerializeField] public string lootName;
    [SerializeField] public int dropChance;

    public LootSpawn(string lootName, int dropChance)
    {
        this.lootName = lootName;
        this.dropChance = dropChance;
    }
}
