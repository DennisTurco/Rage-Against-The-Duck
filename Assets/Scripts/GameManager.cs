using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // inventory
    public int coins;
    public int bombs;

    // resources
    public FloatingTextManager floatingTextManager;
    public CameraShake cameraShake;

    private void Awake()
    {
        Instance = this;
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


    // Save state
    public void SaveState() { }
    public void LoadState(LoadSceneMode mode) { }
}


// game state
public enum GameState
{

}
