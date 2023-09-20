using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Vector3 originalPosition;

    public void StartShake(float shakeDuration, float shakeIntensity)
    {
        originalPosition = transform.localPosition;
        StartCoroutine(Shake(shakeDuration, shakeIntensity));
    }

    private IEnumerator Shake(float shakeDuration, float shakeIntensity)
    {
        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            // Generate a random position within a circle and apply the shake effect
            Vector3 randomOffset = Random.insideUnitCircle * shakeIntensity;
            transform.localPosition = originalPosition + randomOffset;

            elapsed += Time.deltaTime;

            yield return null;
        }

        // Restore original position after shake effect
        transform.localPosition = originalPosition;
    }
}
