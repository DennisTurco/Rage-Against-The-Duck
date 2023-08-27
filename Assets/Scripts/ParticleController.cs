using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField] private float destructionDelay; // Delay in seconds before destroying the particle system

    private ParticleSystem particleSystem;

    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        if (particleSystem == null)
        {
            Debug.LogError("No ParticleSystem component found on this GameObject!");
        }
    }

    private void Update()
    {
        if (particleSystem != null && !particleSystem.isPlaying)
        {
            destructionDelay -= Time.deltaTime;

            if (destructionDelay <= 0.0f)
            {
                Destroy(gameObject); // Destroy the GameObject (which contains the ParticleSystem) after the delay
            }
        }
    }
}
