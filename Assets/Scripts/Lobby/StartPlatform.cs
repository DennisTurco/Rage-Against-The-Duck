using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPlatform : MonoBehaviour
{
    [SerializeField] private Object gameScene;
    private bool playerInRange = false;

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            LoadGameScene();
        }
    }

    private void LoadGameScene()
    { 
        if (gameScene != null)
        {
            GameManager.Instance.SaveGameData();
            SceneManager.LoadScene(gameScene.name);
        }
        else
        {
            Debug.LogError("Game scene not assigned in the Inspector.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
