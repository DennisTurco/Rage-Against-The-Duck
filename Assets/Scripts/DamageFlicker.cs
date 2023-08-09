using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlicker : MonoBehaviour
{
    public Color flickerColor = Color.white; // flicking color
    public float flickerDuration = 0.2f; // flicking duration

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private bool isFlickering = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    public void TakeDamage()
    {
        if (!isFlickering)
        {
            StartCoroutine(Flicker());
        }
    }

    private IEnumerator Flicker()
    {
        isFlickering = true;
        
        // change sprite color for flicking effect
        spriteRenderer.color = flickerColor;

        yield return new WaitForSeconds(flickerDuration);

        // reset sprite color
        spriteRenderer.color = originalColor;

        isFlickering = false;
    }

}
