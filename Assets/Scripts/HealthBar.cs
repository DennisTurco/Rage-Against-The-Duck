using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private int numOfHearts;

    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite halfHeart;
    [SerializeField] private Sprite emptyHeart;

    [SerializeField] private FlickerEffect flashEffect;

    private void Start()
    {
        GameManager.Instance.SetHearthBarComponent(this);
    }

    private void Update()
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

    public void AddHeart()
    {
        if (health == numOfHearts)
        {
            numOfHearts++;
            health = numOfHearts;
        }
        else health++;
    }

    public void TakeDamage()
    {
        health--;

        // flicker effect
        flashEffect.WhiteFlash();

        if (health <= 0)
        {
            PlayerDied();
        }
    }

    private void PlayerDied()
    {
        hearts[0].sprite = emptyHeart; //TODO: find another way
        GameManager.Instance.GameOver();
        gameObject.SetActive(false);
    }
}
