using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // inventory
    [Header("Inventory")]
    [SerializeField] public int coins;
    [SerializeField] public int bombs;
    [SerializeField] public int minions;

    // resources
    [Header("Resources")]
    [SerializeField] private FloatingTextManager floatingTextManager;
    [SerializeField] private TextStatMovementSpeed textStatMovementSpeed;
    [SerializeField] private TextStatAttackDamage textStatAttackDamage;
    [SerializeField] private TextStatAttackSpeed textStatAttackSpeed;
    [SerializeField] private TextStatAttackRange textStatAttackRange;
    [SerializeField] private TextStatAttackRate textStatAttackRate;
    //[SerializeField] private TextStatLuck textStatLuck;
    [SerializeField] private CameraShake cameraShake;
    private HealthBar healthBar;

    // stats
    [Header("Player Stats")]
    [SerializeField] private float attackDamageMin;
    [SerializeField] private float attackDamageMax;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float attackRangeMin;
    [SerializeField] private float attackRangeMax;
    [SerializeField] private float attackRate;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float luck;

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

    public void SetHearthBarComponent(HealthBar healthBar)
    {
        this.healthBar = healthBar;
    }

    // add new heart
    public void AddHeart()
    {
        healthBar.AddHeart();
    }

    // ############################# Stats

    public void SetMovementsSpeed(float movementSpeed)
    {
        this.movementSpeed = movementSpeed;
        textStatMovementSpeed.SetText(movementSpeed);
    }

    public void SetAttackDamage(float attackDamageMin, float attackDamageMax)
    {
        this.attackDamageMin = attackDamageMin;
        this.attackDamageMax = attackDamageMax;
        textStatAttackDamage.SetText(attackDamageMin, attackDamageMax);
    }

    public void SetAttackSpeed(float attackSpeed)
    {
        this.attackSpeed = attackSpeed;
        textStatAttackSpeed.SetText(attackSpeed);
    }

    public void SetAttackRange(float attackRangeMin, float attackRangeMax)
    {
        this.attackRangeMin = attackRangeMin;
        this.attackRangeMax = attackRangeMax;
        textStatAttackRange.SetText(attackRangeMin, attackRangeMax);
    }

    public void SetAttackRate(float attackRate)
    {
        this.attackRate = attackRate;
        textStatAttackRate.SetText(attackRate);
    }

    public void SetLuck(float luck)
    {
        this.luck = luck;
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
}