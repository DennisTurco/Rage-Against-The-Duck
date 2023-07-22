using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeathBar : MonoBehaviour
{
    [SerializeField] public int health;
    [SerializeField] public int numOfHearts;

    [SerializeField] public Image[] hearts;
    [SerializeField] public Sprite fullHeart;
    [SerializeField] public Sprite emptyHeart;

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
}
