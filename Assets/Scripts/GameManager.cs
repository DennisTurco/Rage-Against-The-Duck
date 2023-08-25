using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEditor.SearchService;
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