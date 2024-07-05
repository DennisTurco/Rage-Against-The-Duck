using System.Collections;
using UnityEngine;

public class GenericDialog : MonoBehaviour
{
    [SerializeField] private DialogManager dialogManager;
    [SerializeField, TextArea(1, 10)] private string[] sentences = new string[1] { "Default first sentence. Modify this in the script." };
    [SerializeField] private string speakerName;
    [SerializeField] private IteratableMessage interactableText;
    private bool playerInRange = false;
    private bool dialogStarted = false;

    private void Start()
    {
        if (sentences.Length > 0)
        {
            sentences[0] = "Default first sentence. Don't modify.";
        }

        if (dialogManager == null)
        {
            dialogManager = FindObjectOfType<DialogManager>();
        }

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
        if (dialogManager != null)
        {
            dialogManager.OnDialogEnd -= OnDialogEnd;
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !dialogStarted)
        {
            StartDialog();
        }

        if (interactableText != null)
        {
            if (playerInRange && !dialogStarted)
            {
                interactableText.SetTextVisible();
            }
            else if (!playerInRange && interactableText.interactionMessageOpen)
            {
                interactableText.SetTextInvisible();
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
            Debug.Log($"(GenericDialog) The player has entered the {speakerName} collision area.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log($"(GenericDialog) The player left the {speakerName} collision area.");
        }
    }
}
