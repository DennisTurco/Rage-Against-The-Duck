using UnityEngine;

public class StartPlatform : MonoBehaviour
{
    [SerializeField] private LevelLoader levelLoader;
    [SerializeField] private string sceneName;
    [SerializeField] private string sceneTitle;
    private bool playerInRange = false;
    private bool gameStarted = false;

    private void Update()
    {
        if (playerInRange && !gameStarted && Input.GetKeyDown(KeyCode.E))
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        gameStarted = true;
        GameManager.Instance.SaveGameData();
        levelLoader.LoadLevel(sceneName, sceneTitle);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }
}
