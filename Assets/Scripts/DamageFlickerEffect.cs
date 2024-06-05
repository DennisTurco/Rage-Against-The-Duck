using System.Collections;
using UnityEngine;

public class DamageFlickerEffect : MonoBehaviour
{
    [Tooltip("Material to switch to during the flash.")]
    [SerializeField] private Material flashMaterial;

    [Tooltip("Duration of the flicker interval.")]
    [SerializeField] private float flickerInterval = 0.25f;
    [SerializeField] private float totalDuration = 3.0f;

    // The SpriteRenderer that should flash.
    private SpriteRenderer spriteRenderer;

    // The material that was in use, when the script started.
    private Material originalMaterial;

    // The currently running coroutine.
    private Coroutine flashRoutine;

    private bool flashing = false;

    void Start()
    {
        // Get the SpriteRenderer to be used,
        // alternatively you could set it from the inspector.
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Get the material that the SpriteRenderer uses,
        // so we can switch back to it after the flash ended.
        originalMaterial = spriteRenderer.material;
    }

    public void StartFlicker()
    {
        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }
        flashing = true;
        flashRoutine = StartCoroutine(FlickerRoutine());
    }

    private IEnumerator FlickerRoutine()
    {
        float endTime = Time.time + totalDuration;
        while (Time.time < endTime)
        {
            spriteRenderer.material = flashMaterial;
            yield return new WaitForSeconds(flickerInterval);
            spriteRenderer.material = originalMaterial;
            yield return new WaitForSeconds(flickerInterval);
        }
        spriteRenderer.material = originalMaterial;
        flashRoutine = null;
        flashing = false;
    }

    public float GetTotalDuration()
    {
        return totalDuration;
    }

    public bool IsFlashing()
    {
        return flashing;
    }
}
