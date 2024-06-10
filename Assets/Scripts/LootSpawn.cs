using UnityEngine;

// CreateAssetMenu allows to create new LootSpawn object into unity editor: right click -> LootSpawn
[CreateAssetMenu]
public class LootSpawn : ScriptableObject // ScriptableObject allows to work in the unity editor
{
    [SerializeField] public Sprite lootSprite;
    [SerializeField] public ItemName lootName;
    [SerializeField] public int dropChance;

    public LootSpawn(ItemName lootName, int dropChance)
    {
        this.lootName = lootName;
        this.dropChance = dropChance;
    }
}
