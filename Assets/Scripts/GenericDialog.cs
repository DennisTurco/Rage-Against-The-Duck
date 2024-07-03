using System.Collections;
using UnityEngine;

public class GenericDialog : MonoBehaviour
{
    [SerializeField] private DialogManager dialogManager;  // Reference to the DialogManager
    [SerializeField, TextArea(1, 10)] private string[] sentences = new string[1] { "Default first sentence. Modify this in the script." };  // Array of sentences for the dialog
    [SerializeField] private string speakerName;  // Name of the speaker
    [SerializeField] private IteratableMessage interactableText; // Reference to the IteratableMessage
    private bool playerInRange = false;  // Flag to check if the player is in the collider
    private bool dialogStarted = false;  // Flag to check if the dialog has started

    private void Start()
    {
        // Ensure the first sentence is set to a default value
        if (sentences.Length > 0)
        {
            sentences[0] = "Default first sentence. Modify this in the script.";
        }

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
            Debug.LogError("Reference to DialogManager not assigned in GenericDialog.");
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
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !dialogStarted)
        {
            StartDialog();
        }

        // Manage IteratableMessage visibility and positioning
        if (interactableText != null)
        {
            if (playerInRange && !interactableText.interactionMessageOpen && !dialogStarted)
            {
                interactableText.SetTextVisible();
            }

            if (!playerInRange && interactableText.interactionMessageOpen)
            {
                interactableText.SetTextInvisible();
            }

            if (interactableText.interactionMessageOpen)
            {
                interactableText.PositionInteractionMessage();
            }
        }
    }

    private void StartDialog()
    {
        if (interactableText != null)
        {
            interactableText.SetTextInvisible();
        }

        if (dialogManager != null)
        {
            Debug.Log("Starting dialog with speaker: " + speakerName);
            foreach (var sentence in sentences)
            {
                Debug.Log("Sentence: " + sentence);
            }
            dialogManager.StartDialog(sentences, speakerName);
            dialogStarted = true;
        }
        else
        {
            Debug.LogError("Reference to DialogManager not assigned in GenericDialog.");
        }
    }

    // Method called when the dialog ends
    private void OnDialogEnd()
    {
        dialogStarted = false;
        Debug.Log("Dialog ended.");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("The player has entered the NPC collision area.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("The player left the NPC collision area.");
        }
    }
}
