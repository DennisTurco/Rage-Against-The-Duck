using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class PlayerModelSelector : MonoBehaviour
{
    [SerializeField] private GameObject newPlayerModelObject;
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
            if (playerInRange && !interactableText.interactionMessageOpen)
            {
                interactableText.SetTextVisible();
                statsCard.SetActive(true);
                playerModelCard.SetPlayerModelCard(newPlayerStats);
            }
            if (!playerInRange && interactableText.interactionMessageOpen)
            {
                interactableText.SetTextInvisible();
                statsCard.SetActive(false);
            }

            if (interactableText.interactionMessageOpen)
            {
                interactableText.PositionInteractionMessage();
            }
        }
    }

    private void ChangePlayerModel()
    {
        GameObject currentPlayerObject = GameObject.FindGameObjectWithTag("Player");

        if (currentPlayerObject != null && newPlayerModelObject != null)
        {
            Vector3 currentPlayerPosition = currentPlayerObject.transform.position;
            newPlayerModelObject.transform.position = currentPlayerPosition;

            PlayerStats currentStats = currentPlayerObject.GetComponent<PlayerStats>();
            if (currentStats != null)
            {
                PlayerStats newStats = newPlayerModelObject.GetComponent<PlayerStats>();
                if (newStats == null)
                {
                    newStats = newPlayerModelObject.AddComponent<PlayerStats>();
                }
            }
            else
            {
                Debug.LogError("Current player does not have PlayerStats component");
            }

            currentPlayerObject.SetActive(false);

            if (!newPlayerModelObject.activeSelf)
            {
                newPlayerModelObject.SetActive(true);
            }

            GameManager.Instance.playerType = newPlayerStats.statsPlayerName;

            SmoothCameraFollow cameraFollow = Camera.main.GetComponent<SmoothCameraFollow>();
            if (cameraFollow != null)
            {
                cameraFollow.SetTarget(newPlayerModelObject.transform);
            }

            Debug.Log("Player model changed to: " + newPlayerStats.statsPlayerName);
        }
        else
        {
            Debug.LogError("Player GameObject not found or newPlayerModelObject not assigned!");
        }

        Debug.Log("playerType = " + GameManager.Instance.playerType);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log($"Player entered the range of {newPlayerStats.statsPlayerName}.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log($"Player exited the range of {newPlayerStats.statsPlayerName}.");
        }
    }
}
