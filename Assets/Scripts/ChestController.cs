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
                GameManager.Instance.ShowFloatingText("Chest locked, key is required", 20, Color.red, transform.position, Vector3.up * 100, 1.5f);
            }
        }

        if (InteractableText != null && playerInRange && !chestOpened)
        {
            InteractableText.SetTextVisible();
        }
        else if (InteractableText != null && !playerInRange && InteractableText.interactionMessageOpen)
        {
            InteractableText.SetTextInvisible();
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
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
