using UnityEngine;

public class Slotty : MonoBehaviour
{
    [SerializeField] private DialogManager dialogManager;  // Reference to the DialogManager
    [SerializeField] private GameObject slottyMenu;  // Reference to the slotty menu
    [SerializeField] private IteratableMessage InteractableText;
    private bool playerInRange = false;  // Flag to check if the player is in the collider
    private bool dialogStarted = false;  // Flag to check if the dialog has started
    private bool slottyMenuOpen = false;  // Flag to check if the slotty menu is open
    private void Awake()
    {
        PauseMenu.IsInTradeOrSlottyMenu = false;
    }

    // Start is called before the first frame update
    private void Start()
    {
        // Find the DialogManager in the scene if it's not assigned manually
        if (dialogManager == null)
        {
            dialogManager = FindFirstObjectByType<DialogManager>();
        }

        // Make sure to subscribe to the end of dialog event
        if (dialogManager != null)
        {
            dialogManager.OnDialogEnd += OnDialogEnd;
        }
        else
        {
            Debug.LogError("Reference to DialogManager not assigned in Slotty.");
        }
    }

    // Update is called once per frame
    private void Update()
    {
        // If the player is in range and presses the E key, start the dialog
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !dialogStarted && !slottyMenuOpen)
        {
            StartDialog();
        }

        if (InteractableText != null && playerInRange && !InteractableText.interactionMessageOpen && !slottyMenuOpen && !dialogStarted)
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

        // Check if the slotty menu is open and if the Escape key is pressed, close it
        if (slottyMenuOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseSlottyMenu();
            Time.timeScale = 1f;
            Debug.Log("Escape key pressed to close Slotty menu.");
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

    private void StartDialog()
    {
        if (InteractableText != null)
        {
            InteractableText.SetTextInvisible();
        }

        // Execute the dialog using the DialogManager
        string[] sentences = new string[] {
            "Hello there!",
            "I'm Slotty, the slot machine master.",
            "Try your luck and see what you win!"
        };
        string speakerName = "SLOTTY";

        if (dialogManager != null)
        {
            dialogManager.StartDialog(sentences, speakerName);
            dialogStarted = true;
        }
        else
        {
            Debug.LogError("Reference to DialogManager not assigned in Slotty.");
        }
    }

    // Method called when the dialog ends
    private void OnDialogEnd()
    {
        dialogStarted = false;

        if (playerInRange)  // Check if the player is still in the merchant's area
        {
            Time.timeScale = 0f;
            OpenSlottyMenu();
        }
    }

    // Method to open the slotty menu
    public void OpenSlottyMenu()
    {
        if (slottyMenu != null)
        {
            slottyMenu.SetActive(true);
            slottyMenuOpen = true;
            PauseMenu.IsInTradeOrSlottyMenu = true;  // Set the static variable to true
            Debug.Log("Slotty menu opened.");
        }
    }

    // Method to close the slotty menu
    public void CloseSlottyMenu()
    {
        if (slottyMenu != null)
        {
            slottyMenu.SetActive(false);
            slottyMenuOpen = false;
            PauseMenu.IsInTradeOrSlottyMenu = false;  // Set the static variable to false
            Debug.Log("Slotty menu closed.");

            PauseMenu pauseMenu = FindFirstObjectByType<PauseMenu>();
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
            Debug.Log("The player has entered Slotty collision area.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("The player left Slotty collision area.");
        }
    }
}
