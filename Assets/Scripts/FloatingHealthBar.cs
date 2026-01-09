using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Camera healthBarCamera;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue;
        gameObject.SetActive(currentValue < maxValue); // set floating bar active if the enemy is damaged
    }

    private void Update()
    {
        if (target != null && healthBarCamera != null) {
            transform.rotation = healthBarCamera.transform.rotation;
            transform.position = target.position + offset;
        }
    }
}
