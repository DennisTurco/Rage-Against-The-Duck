using UnityEngine;

public class ChestController : MonoBehaviour
{
    [SerializeField] private IteratableMessage InteractableText;
    [SerializeField] private bool chestLocked = false;
    private bool playerInRange = false;
    private bool chestOpened;

    private void Start()
    {
        chestOpened = false;
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !chestOpened)
        {
            if (chestLocked && GameManager.Instance.keys > 0)
            {
                ItemKey.UseItemKey();
                OpenChest();
            }
            else if (!chestLocked)
            {
                OpenChest();
            }
            else
            {
                Debug.Log("Chest locked, key is required");
            }
        }

        if (InteractableText != null && playerInRange && !InteractableText.interactionMessageOpen && !chestOpened)
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
    }

    private void OpenChest()
    {
        chestOpened = true;
        GetComponent<LootBag>().InstantiateLootSpawn(new Vector3(transform.position.x, transform.position.y - 1, 0));
        InteractableText.SetTextInvisible();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("The player has entered the chest collision Area");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("The player left the chest collision Area");
        }
    }
}
