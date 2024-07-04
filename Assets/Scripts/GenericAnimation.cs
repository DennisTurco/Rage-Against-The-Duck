using System.Collections;
using UnityEngine;

public class GenericAnimation : MonoBehaviour
{
    [SerializeField] private Sprite[] animationFrames;
    [SerializeField] private float animationFrameDelay = 0.15f;

    private SpriteRenderer spriteRenderer;
    private int currentFrame = 0;
    private Coroutine animationCoroutine;

    private void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError($"SpriteRenderer non trovato su {gameObject.name}! Assicurati che il GameObject abbia un componente SpriteRenderer.");
            return;
        }

        if (animationFrames == null || animationFrames.Length == 0)
        {
            Debug.LogError($"Animation frames non assegnati su {gameObject.name}! Assicurati di aver assegnato i frame dell'animazione nell'Inspector.");
            return;
        }

        animationCoroutine = StartCoroutine(Animate());
    }

    private void OnDisable()
    {
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
            animationCoroutine = null;
        }
    }

    private IEnumerator Animate()
    {
        while (true)
        {
            spriteRenderer.sprite = animationFrames[currentFrame];
            currentFrame = (currentFrame + 1) % animationFrames.Length;
            yield return new WaitForSeconds(animationFrameDelay);
        }
    }
}
