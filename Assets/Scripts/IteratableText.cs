using TMPro;
using UnityEngine;

public class IteratableMessage : MonoBehaviour
{
    [SerializeField] private GameObject InteractionMessage;
    [SerializeField] private Canvas canvas; // Reference to the Canvas
    public bool interactionMessageOpen = false;

    private void Start()
    {
        InteractionMessage.SetActive(false);
    }

    public void SetTextVisible()
    {
        InteractionMessage.SetActive(true);
        PositionInteractionMessage();
        interactionMessageOpen = true;
    }

    public void SetTextInvisible()
    {
        InteractionMessage.SetActive(false);
        interactionMessageOpen = false;
    }

    public void PositionInteractionMessage()
    {
        // Adjust the Y offset as needed to position the message above the chest
        float yOffset = 30f; // Adjust based on UI requirements
        Vector3 worldPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, screenPosition, canvas.worldCamera, out Vector2 localPoint);
        InteractionMessage.transform.localPosition = localPoint + new Vector2(0, yOffset);
    }
}