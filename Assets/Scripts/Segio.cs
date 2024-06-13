using System.Collections;
using UnityEngine;

public class Sergio : MonoBehaviour
{
    [SerializeField] private Sprite[] animationFrames; // I frame dell'animazione
    [SerializeField] private float animationFrameDelay = 0.15f; // Delay tra i frame dell'animazione

    private SpriteRenderer spriteRenderer;
    private int currentFrame = 0;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer non trovato! Assicurati che il GameObject abbia un componente SpriteRenderer.");
            return;
        }

        if (animationFrames == null || animationFrames.Length == 0)
        {
            Debug.LogError("Animation frames non assegnati! Assicurati di aver assegnato i frame dell'animazione nell'Inspector.");
            return;
        }

        StartCoroutine(Animate());
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
