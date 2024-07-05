using System.Collections;

using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Sprite[] idleFrames;
    [SerializeField] private Sprite[] walkFrames;
    [SerializeField] private float idleFrameDelay = 0.15f;
    [SerializeField] private float walkFrameDelay = 0.15f;

    private SpriteRenderer spriteRenderer;
    private int currentFrame = 0;
    private Coroutine animationCoroutine;
    private string currentState;
    private PlayerMovement playerMovement;

    private void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerMovement = GetComponent<PlayerMovement>();

        if (spriteRenderer == null)
        {
            Debug.LogError($"SpriteRenderer non trovato su {gameObject.name}! Assicurati che il GameObject abbia un componente SpriteRenderer.");
            return;
        }

        if (idleFrames == null || idleFrames.Length == 0 || walkFrames == null || walkFrames.Length == 0)
        {
            Debug.LogError($"Animation frames non assegnati su {gameObject.name}! Assicurati di aver assegnato i frame dell'animazione nell'Inspector.");
            return;
        }

        currentState = "Idle";
        animationCoroutine = StartCoroutine(Animate(idleFrames, idleFrameDelay));
    }

    private void OnDisable()
    {
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
            animationCoroutine = null;
        }
    }

    private void Update()
    {
        if (playerMovement.IsMoving() && currentState != "Walk")
        {
            ChangeAnimationState("Walk");
        }
        else if (!playerMovement.IsMoving() && currentState != "Idle")
        {
            ChangeAnimationState("Idle");
        }
    }

    private void ChangeAnimationState(string newState)
    {
        if (newState == currentState) return;

        currentState = newState;
        currentFrame = 0; // Reset the current frame when changing animation state
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
        }

        if (newState == "Walk")
        {
            animationCoroutine = StartCoroutine(Animate(walkFrames, walkFrameDelay));
        }
        else
        {
            animationCoroutine = StartCoroutine(Animate(idleFrames, idleFrameDelay));
        }
    }

    private IEnumerator Animate(Sprite[] frames, float frameDelay)
    {
        while (true)
        {
            if (frames.Length == 0)
            {
                yield break;
            }

            spriteRenderer.sprite = frames[currentFrame];
            currentFrame = (currentFrame + 1) % frames.Length;
            yield return new WaitForSeconds(frameDelay);
        }
    }
}