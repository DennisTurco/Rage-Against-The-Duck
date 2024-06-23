using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static event Action UpdateHealthHeartBar;

    [SerializeField] private FlickerEffect flashEffect;
    [SerializeField] private DamageFlickerEffect damageFlashEffect;
    public float health, maxHealth;

    private void OnEnable()
    {
        Heart.CanCollectHearts += CanCollectHearts;
        ItemHeart.OnHeartCollected += IncrementHealth;
    }

    public void OnDisable()
    {
        Heart.CanCollectHearts -= CanCollectHearts;
        ItemHeart.OnHeartCollected -= IncrementHealth;
    }

    private void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage()
    {
        if (damageFlashEffect.IsFlashing())
        {
            return;
        }

        health--;
        UpdateHealthHeartBar?.Invoke();

        // flicker effect
        flashEffect.WhiteFlash();
        damageFlashEffect.StartFlicker();

        if (health <= 0)
        {
            PlayerDied();
        }
    }

    public void IncrementHealth()
    {
        health++;
        UpdateHealthHeartBar?.Invoke();
    }

    public void IncrementMaxHealth()
    {
        maxHealth++;
        UpdateHealthHeartBar?.Invoke();
    }

    private void PlayerDied()
    {
        GameManager.Instance.GameOver();
        gameObject.SetActive(false);
    }

    private bool CanCollectHearts()
    {
        return health < maxHealth;
    }
}

public static class Heart
{
    public static Func<bool> CanCollectHearts;
}
