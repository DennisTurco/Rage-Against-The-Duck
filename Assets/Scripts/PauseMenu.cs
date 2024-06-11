using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool IsPaused = false;
    public static bool IsInTradeOrSlottyMenu = false;
    private bool ignoreNextEscapePress = false;

    public GameObject pMenu;

    void Update()
    {
        if (ignoreNextEscapePress)
        {
            ignoreNextEscapePress = false;
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.Instance.GameisOver == false)
        {
            Debug.Log("Escape key was pressed");
            if (IsInTradeOrSlottyMenu)
            {
                return;
            }
            else
            {
                if (IsPaused)
                {
                    Resume();
                }
                else
                {
                    Debug.Log("IsInTradeOrSlottyMenu =" + IsInTradeOrSlottyMenu);
                    Pause();
                }
            }
        }
    }

    public void Resume()
    {
        pMenu.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
    }

    public void Pause()
    {
        pMenu.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
    }

    public void MainMenu()
    {
        Debug.Log("MainMenu button was pressed");
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Debug.Log("Quit button was pressed");
        Application.Quit();
    }

    public void IgnoreNextEscapePress()
    {
        ignoreNextEscapePress = true;
    }
}
