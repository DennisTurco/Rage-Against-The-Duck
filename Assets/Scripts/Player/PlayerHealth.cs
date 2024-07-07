using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static event Action UpdateHealthHeartBar;

    [SerializeField] private FlickerEffect flashEffect;
    [SerializeField] private DamageFlickerEffect damageFlashEffect;
    public int Health { get; set; }
    public int MaxHealth { get; set; }

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
        MaxHealth = GameManager.Instance.maxHealth;
        Health = GameManager.Instance.health;
    }

    public void TakeDamage()
    {
        if (damageFlashEffect.IsFlashing())
        {
            return;
        }

        Health--;
        GameManager.Instance.health = Health;
        UpdateHealthHeartBar?.Invoke();

        // flicker effect
        flashEffect.WhiteFlash();
        damageFlashEffect.StartFlicker();

        if (Health <= 0)
        {
            PlayerDied();
        }
    }

    public void IncrementHealth()
    {
        Health++;
        GameManager.Instance.health = Health;
        UpdateHealthHeartBar?.Invoke();
    }

    public void IncrementMaxHealth()
    {
        MaxHealth++;
        GameManager.Instance.health = MaxHealth;
        UpdateHealthHeartBar?.Invoke();
    }

    private void PlayerDied()
    {
        GameManager.Instance.GameOver();
        gameObject.SetActive(false);
    }

    private bool CanCollectHearts()
    {
        return Health < MaxHealth;
    }
}

public static class Heart
{
    public static Func<bool> CanCollectHearts;
}
