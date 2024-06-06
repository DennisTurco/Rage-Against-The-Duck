using UnityEngine;

public class ChestController : MonoBehaviour
{
    [SerializeField] private GameObject InteractionMessage;
    private bool playerInRange = false;
    private bool interactionMessageOpen = false;
    private bool chestOpened;

    private void Start()
    {
        chestOpened = false;
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !chestOpened)
        {
            if (!chestOpened) 
            {
                OpenChest();
                chestOpened = true;
                InteractionMessage.SetActive(false);
                interactionMessageOpen = false;
            }
            else
            {
                Debug.Log("The chest har altready been opened");
                return;
            }
                
        }

        if (playerInRange && !interactionMessageOpen && !chestOpened)
        {
            InteractionMessage.SetActive(true);
            interactionMessageOpen = true;
        }

        if (!playerInRange && interactionMessageOpen)
        {
            InteractionMessage.SetActive(false);
            interactionMessageOpen = false;
        }
    }

    private void OpenChest()
    {
            GetComponent<LootBag>().InstantiateLootSpawn(new Vector3(transform.position.x, transform.position.y - 1, 0));
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
