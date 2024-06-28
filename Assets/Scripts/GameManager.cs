using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // inventory
    [Header("Inventory")]
    [SerializeField] public int coins;
    [SerializeField] public int bombs;
    [SerializeField] public int keys;
    [SerializeField] public int minions;

    // resources
    [Header("Resources")]
    [SerializeField] public GameObject minionOrbiter;
    [SerializeField] public GameObject minionFollower;
    [SerializeField] private FloatingTextManager floatingTextManager;
    [SerializeField] private CameraShake cameraShake;

    // states
    [Header("States")]
    public bool GameisOver;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Time.timeScale = 1f;
        GameisOver = false;
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


    // Save state
    public void SaveState() { }
    public void LoadState(LoadSceneMode mode) { }

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
        if (minionOrbiter == null)
            Debug.LogError("Minion Orbiter prefab is not assigned in the Inspector!");

        if (minionFollower == null)
            Debug.LogError("Minion Follower prefab is not assigned in the Inspector!");

        if (minionOrbiter == null || minionFollower == null)
            throw new Exception("One or more minion prefabs are null");

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
    }
}