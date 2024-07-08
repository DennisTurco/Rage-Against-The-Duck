using UnityEngine;

public class PlayerModelSelector : MonoBehaviour
{
    [SerializeField] private GameObject newPlayerModelPrefab;
    [SerializeField] private PlayerStatsGeneric newPlayerStats;
    private bool playerInRange = false;

    [SerializeField] private IteratableMessage interactableText;
    public GameObject statsCard;

    private PlayerModelCard playerModelCard;

    private void Start()
    {
        playerModelCard = statsCard.GetComponent<PlayerModelCard>();
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            ChangePlayerModel();
        }

        if (interactableText != null)
        {
            if (playerInRange)
            {
                interactableText.SetTextVisible();
                statsCard.SetActive(true);
                playerModelCard.SetPlayerModelCard(newPlayerStats);
            }
            else if (!playerInRange && interactableText.interactionMessageOpen)
            {
                interactableText.SetTextInvisible();
                statsCard.SetActive(false);
            }
        }
    }

    private void ChangePlayerModel()
    {
        GameObject currentPlayerObject = GameObject.FindGameObjectWithTag("Player");

        if (currentPlayerObject != null && newPlayerModelPrefab != null)
        {
            Vector3 currentPlayerPosition = currentPlayerObject.transform.position;

            GameObject newPlayerObject = Instantiate(newPlayerModelPrefab, currentPlayerPosition, Quaternion.identity);

            PlayerStats newStats = newPlayerObject.GetComponent<PlayerStats>();
            if (newStats != null)
            {
                GameManager.Instance.gameData.playerStats = newStats.playerStatsData;
            }
            else
            {
                Debug.LogError("New player does not have PlayerStats component");
            }

            SmoothCameraFollow cameraFollow = Camera.main.GetComponent<SmoothCameraFollow>();
            if (cameraFollow != null)
            {
                cameraFollow.SetTarget(newPlayerObject.transform);
            }

            Destroy(currentPlayerObject);

            Debug.Log("Player model changed to: " + newPlayerStats.playerType.ToString());
        }
        else
        {
            Debug.LogError("Player GameObject not found or newPlayerModelObject not assigned!");
        }
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
