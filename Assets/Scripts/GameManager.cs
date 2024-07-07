using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool isInitialized = false;
    public bool playerInitialized = false;
    public bool gameDataInitialized = false;

    // inventory
    [Header("Inventory")]
    [SerializeField] public int coins;
    [SerializeField] public int bombs;
    [SerializeField] public int keys;
    [SerializeField] public List<ItemName> minions = new List<ItemName>();

    // resources
    [Header("Resources")]
    [SerializeField] public GameData gameData; // I use this object to propagate data across scenes
    [SerializeField] public GameObject minionOrbiter;
    [SerializeField] public GameObject minionFollower;
    [SerializeField] private FloatingTextManager floatingTextManager;
    [SerializeField] private CameraShake cameraShake;

    // stats - UI
    [SerializeField] public TextStatMovementSpeed textStatMovementSpeed;
    [SerializeField] public TextStatAttackDamage textStatAttackDamage;
    [SerializeField] public TextStatAttackSpeed textStatAttackSpeed;
    [SerializeField] public TextStatAttackRange textStatAttackRange;
    [SerializeField] public TextStatAttackRate textStatAttackRate;
    //[SerializeField] public TextStatLuck textStatLuck;

    // states
    [Header("States")]
    public bool GameisOver;

    private void Awake()
    {
        Initialize();
    }

    private void Start()
    {
        Time.timeScale = 1f;
        GameisOver = false;
    }

    public void Initialize()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        isInitialized = true;
    }

    // floating text on pick up items
    public void ShowFloatingText(string message, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(message, fontSize, color, position, motion, duration);
    }

    // shake camera
    public void ShakeCamera(float duration, float magnitude)
    {
        cameraShake.StartShake(duration, magnitude);
    }

    // #############################

    public void LoadGameData()
    {
        coins = gameData.coins;
        bombs = gameData.bombs;
        keys = gameData.keys;
        minions = gameData.minions;
        gameData.playerStats.MovementSpeed = gameData.movementSpeed;
        gameData.playerStats.AttackDamageMin = gameData.attackDamageMin;
        gameData.playerStats.AttackRangeMin = gameData.attackRangeMin;
        gameData.playerStats.AttackRangeMax = gameData.attackRangeMax;
        gameData.playerStats.AttackRate = gameData.attackRate;
        gameData.playerStats.AttackSpeed = gameData.attackSpeed;
        gameData.playerStats.Luck = gameData.luck;

        gameDataInitialized = true;

        Debug.Log("The game data has been successfully loaded (I hope).");
    }

    public void SaveGameData()
    {
        gameData.coins = coins;
        gameData.bombs = bombs;
        gameData.keys = keys;
        gameData.minions = minions;
        gameData.movementSpeed = gameData.playerStats.MovementSpeed;
        gameData.attackDamageMin = gameData.playerStats.AttackDamageMin;
        gameData.attackRangeMin = gameData.playerStats.AttackDamageMax;
        gameData.attackRangeMax = gameData.playerStats.AttackRangeMax;
        gameData.attackRate = gameData.playerStats.AttackRate;
        gameData.attackSpeed = gameData.playerStats.AttackSpeed;
        gameData.luck = gameData.playerStats.Luck;

        Debug.Log("The game data has been successfully saved (I hope).");
    }

    public void ClearGameData()
    {
        gameData.coins = 0;
        gameData.bombs = 0;
        gameData.keys = 0;
        gameData.minions = new List<ItemName>();
        gameData.ReloadPlayerStats();

        Debug.Log("The game data has been successfully restored (I hope).");
    }

    //Gameover panel
    public void GameOver()
    {
        UIManager _ui = GetComponent<UIManager>();
        if (_ui != null)
        {
            _ui.ToggleDeathPanel();
        }
        GameisOver = true;
    }

    public void SpawnMinion(ItemName minionName)
    {
        Debug.Log($"Spawning minion: {minionName}");

        if (minionOrbiter == null)
            Debug.LogError("Minion Orbiter prefab is not assigned in the Inspector!");

        if (minionFollower == null)
            Debug.LogError("Minion Follower prefab is not assigned in the Inspector!");

        switch (minionName)
        {
            case ItemName.MinionOrbiter:
                PlayerMinion.CreateMinion(minionOrbiter);
                break;
            case ItemName.MinionFollower:
                PlayerMinion.CreateMinion(minionFollower);
                break;
            default:
                Debug.LogError($"Minion name '{minionName}' doesn't exist");
                break;
        }

        minions.Add(minionName);
    }

    public void SpawnMinions()
    {
        List<ItemName> minionsCopy = new List<ItemName>(minions); // copy of the minions list (to avoid the error: InvalidOperationException: Collection was modified; enumeration operation may not execute.)

        foreach (var minion in minionsCopy)
        {
            SpawnMinion(minion);
        }
    }
}
