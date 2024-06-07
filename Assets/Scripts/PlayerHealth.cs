using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static event Action OnPlayerDamage;

    [SerializeField] private FlickerEffect flashEffect;
    [SerializeField] private DamageFlickerEffect damageFlashEffect;
    public float health, maxHealth;

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
        OnPlayerDamage?.Invoke();

        // flicker effect
        flashEffect.WhiteFlash();
        damageFlashEffect.StartFlicker();

        if (health <= 0)
        {
            PlayerDied();
        }
    }

    private void PlayerDied()
    {
        GameManager.Instance.GameOver();
        gameObject.SetActive(false);
    }
}
