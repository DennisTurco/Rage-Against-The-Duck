using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool IsPaused = false;

    public GameObject pMenu;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.Instance.GameisOver == false)
        {
            Debug.Log("Escape key was pressed");
            if (Merchant.IsTradeMenuOpen || Slotty.IsSlottyMenuOpen)
            {
                return;
            }

            if (IsPaused)
            {
                Resume();
            }
            else
            {
                Debug.Log("Merchant.IsTradeMenuOpen =" + Merchant.IsTradeMenuOpen + "Slotty.IsSlottyMenuOpen =" + Slotty.IsSlottyMenuOpen);
                Pause();
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
}
