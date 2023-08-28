using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // inventory
    [SerializeField] public int coins;
    [SerializeField] public int bombs;

    // resources
    [SerializeField] private FloatingTextManager floatingTextManager;
    [SerializeField] private CameraShake cameraShake;
    private HealthBar healthBar;

    // states
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