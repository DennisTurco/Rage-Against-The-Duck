using UnityEngine;

// Generic scale pulse (works for any UI/GameObject)
public class GenericScaleAnimation : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float amount = 0.15f;
    [SerializeField] private bool useUnscaledTime = true;

    private Vector3 baseScale;

    private void Awake()
    {
        baseScale = transform.localScale;
    }

    private void OnEnable()
    {
        baseScale = transform.localScale; // refresh if changed in editor
    }

    private void Update()
    {
        float t = useUnscaledTime ? Time.unscaledTime : Time.time;
        float s = 1f + Mathf.Sin(t * speed) * amount;
        transform.localScale = baseScale * s;
    }
}
