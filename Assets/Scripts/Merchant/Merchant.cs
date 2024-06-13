using System;
using UnityEngine;

public class Merchant : MonoBehaviour
{
    [SerializeField] private DialogManager dialogManager;  // Reference to the DialogManager
    [SerializeField] private GameObject tradeMenu;  // Reference to the trade menu
    [SerializeField] private IteratableMessage InteractableText;
    private bool playerInRange = false;  // Flag to check if the player is in the collider
    private bool dialogStarted = false;  // Flag to check if the dialog has started
    private bool tradeMenuOpen = false;  // Flag to check if the trade menu is open
    public static event Action UpdateTrade;

    private void Awake()
    {
        PauseMenu.IsInTradeOrSlottyMenu = false;
    }

    private void Start()
    {
        // Find the DialogManager in the scene if it's not assigned manually
        if (dialogManager == null)
        {
            dialogManager = FindObjectOfType<DialogManager>();
        }

        // Make sure to subscribe to the end of dialog event
        if (dialogManager != null)
        {
            dialogManager.OnDialogEnd += OnDialogEnd;
        }
        else
        {
            Debug.LogError("Reference to DialogManager not assigned in Merchant.");
        }
    }

    private void OnDestroy()
    {
        // Remember to unsubscribe from the event when the object is destroyed to avoid memory leaks
        if (dialogManager != null)
        {
            dialogManager.OnDialogEnd -= OnDialogEnd;
        }
    }

    private void Update()
    {
        // If the player is in range and presses the E key, start the dialog
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !dialogStarted && !tradeMenuOpen)
        {
            StartDialog();
        }

        if (InteractableText != null && playerInRange && !InteractableText.interactionMessageOpen && !tradeMenuOpen && !dialogStarted)
        {
            InteractableText.SetTextVisible();
        }

        if (InteractableText != null && !playerInRange && InteractableText.interactionMessageOpen)
        {
            InteractableText.SetTextInvisible();
        }

        if (InteractableText != null && InteractableText.interactionMessageOpen)
        {
            InteractableText.PositionInteractionMessage();
        }

        // Check if the trade menu is open and if the Escape key is pressed, close it
        if (tradeMenuOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseTradeMenu();
            Time.timeScale = 1f;
        }
    }

    private void StartDialog()
    {
        if (InteractableText != null)
        {
            InteractableText.SetTextInvisible();
        }

        // Execute the dialog using the DialogManager
        string[] sentences = new string[] {
            "Welcome my friend!",
            "How are you? I am Sergio.",
            "See what I sell or I'll stab you!"
        };
        string speakerName = "SERGIO";

        if (dialogManager != null)
        {
            dialogManager.StartDialog(sentences, speakerName);
            dialogStarted = true;
        }
        else
        {
            Debug.LogError("Reference to DialogManager not assigned in Merchant.");
        }
    }

    // Method called when the dialog ends
    private void OnDialogEnd()
    {
        dialogStarted = false;

        if (playerInRange)  // Check if the player is still in the merchant's area
        {
            Time.timeScale = 0f;
            OpenTradeMenu();
            UpdateTrade?.Invoke();
        }
    }

    // Method to open the trade menu
    public void OpenTradeMenu()
    {
        if (tradeMenu != null)
        {
            tradeMenu.SetActive(true);
            tradeMenuOpen = true;
            PauseMenu.IsInTradeOrSlottyMenu = true;  // Set the static variable to true
            Debug.Log("Merchant menu opened.");
        }
    }

    // Method to close the trade menu
    public void CloseTradeMenu()
    {
        if (tradeMenu != null)
        {
            tradeMenu.SetActive(false);
            tradeMenuOpen = false;
            PauseMenu.IsInTradeOrSlottyMenu = false;  // Set the static variable to false
            Debug.Log("Merchant menu closed.");

            PauseMenu pauseMenu = FindObjectOfType<PauseMenu>();
            if (pauseMenu != null)
            {
                pauseMenu.IgnoreNextEscapePress();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("The player has entered the merchant collision area.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("The player left the merchant collision area.");
        }
    }
}
