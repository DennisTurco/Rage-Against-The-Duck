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
            Debug.Log("The player has entered the start platform area.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("The player left the start platform area.");
        }
    }
}
