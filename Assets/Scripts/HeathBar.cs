using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeathBar : MonoBehaviour
{
    [SerializeField] public int health = 100;
    [SerializeField] public int numOfHearts;

    [SerializeField] public Image[] hearts;
    [SerializeField] public Sprite fullHeart;
    [SerializeField] public Sprite emptyHeart;

    [SerializeField] private FlickerEffect flashEffect;

    void Update()
    {
        if (health > numOfHearts) 
        {
            health = numOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            // set a fullHeart or a emptyHeart
            if (i < health) 
            {
                hearts[i].sprite = fullHeart;
            } else 
            {
                hearts[i].sprite = emptyHeart;
            }

            // in this way you can show the number (hearts.Lenght) of hearts visible
            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            } else 
            {
                hearts[i].enabled = false;
            }
        }
    }

    public void TakeDamage()
    {
        numOfHearts--;

        // flicker effect
        flashEffect.Flash();

        if (numOfHearts <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        PlayerMovement.Destroy(gameObject);
    }
}
