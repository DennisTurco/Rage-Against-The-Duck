
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private GameObject SMenu;
    [SerializeField] private PauseMenu pauseMenu;   // optional
    [SerializeField] private KeybindsReadOnlyUI keybindsUI; // optional

    [SerializeField] private KeyCode toggleKey = KeyCode.O;
    [SerializeField] private bool pauseTimeWhenOpenedFromGame = true;

    private bool isOpen;
    private bool pausedByMe;
    private bool wasPausedBeforeOpen;

    private void Awake()
    {
        if (SMenu != null) SMenu.SetActive(false);
        isOpen = false;
        pausedByMe = false;
    }
    private void Update()
    {

        if (GameManager.Instance != null && GameManager.Instance.GameisOver) return;

        if (Input.GetKeyDown(toggleKey))
        {
            
            if (PauseMenu.IsInTradeOrSlottyMenu) return;

            if (isOpen) Close();
            else
            {
                if (pauseMenu != null && PauseMenu.IsPaused)
                    OpenFromPause();
                else
                    OpenFromGameOrTitle();
            }
        }


        if (isOpen && Input.GetKeyDown(KeyCode.Escape))
            Close();
    }

    public void OpenFromPause()
    {
        wasPausedBeforeOpen = true;
        pausedByMe = false;
        OpenInternal();
        pauseMenu?.IgnoreNextEscapePress();
    }


    public void OpenFromGameOrTitle()
    {
        wasPausedBeforeOpen = (Time.timeScale == 0f) || PauseMenu.IsPaused;

        pausedByMe = pauseTimeWhenOpenedFromGame && !wasPausedBeforeOpen;
        if (pausedByMe)
        {
            Time.timeScale = 0f;
            PauseMenu.IsPaused = true;
        }

        OpenInternal();
        pauseMenu?.IgnoreNextEscapePress();
    }


    public void Close()
    {
        isOpen = false;
        if (SMenu != null) SMenu.SetActive(false);

        PauseMenu.IsInTradeOrSlottyMenu = false;

        if (pausedByMe && !wasPausedBeforeOpen)
        {
            Time.timeScale = 1f;
            PauseMenu.IsPaused = false;
        }

        pausedByMe = false;
        wasPausedBeforeOpen = false;

        pauseMenu?.IgnoreNextEscapePress();
    }


    private void OpenInternal()
    {
        isOpen = true;

        if (SMenu != null)
        {
            SMenu.SetActive(true);
            SMenu.transform.SetAsLastSibling();
        }

        PauseMenu.IsInTradeOrSlottyMenu = true;
        if (keybindsUI != null)
            keybindsUI.Rebuild();
    }

}
