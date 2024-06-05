using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    // Lista di sprite per la trappola
    [SerializeField] private List<Sprite> sprites;

    // Intervallo di tempo tra il cambio di sprite
    [SerializeField] private float swapinterval = 0.15f;

    // Stato della trappola (attiva o non attiva)
    private bool trapActive;

    // Tempo trascorso dall'ultimo cambio di sprite
    private float timeSinceLastSwap;

    // Indice dello sprite corrente
    private int currentSpriteIndex = 0;

    // Componente SpriteRenderer dell'oggetto
    private SpriteRenderer spriteRenderer;

    // Lista dei collider all'interno del box di collisione della trappola
    private List<Collider2D> collidersInTrap = new List<Collider2D>();

    // Lista temporanea dei collider da rimuovere
    private List<Collider2D> collidersToRemove = new List<Collider2D>();

    private void Start()
    {
        // Inizializza la trappola come non attiva
        trapActive = false;

        // Ottieni il componente SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Imposta lo sprite iniziale se la lista degli sprite non è vuota
        if (sprites.Count > 0)
        {
            spriteRenderer.sprite = sprites[0];
        }
    }

    private void Update()
    {
        // Incrementa il tempo trascorso dall'ultimo cambio di sprite
        timeSinceLastSwap += Time.deltaTime;

        // Mantieni lo stato precedente della trappola
        bool wasTrapActive = trapActive;

        // Aggiorna lo stato della trappola in base all'indice dello sprite
        if (currentSpriteIndex == 2 || currentSpriteIndex == 3 || currentSpriteIndex == 4 || currentSpriteIndex == 5)
        {
            trapActive = true;
        }
        else
        {
            trapActive = false;
        }

        // Se la trappola diventa attiva, infliggi danno ai collider all'interno del box
        if (trapActive && !wasTrapActive)
        {
            foreach (var collider in collidersInTrap)
            {
                // Se il collider è null, aggiungilo alla lista dei collider da rimuovere
                if (collider == null)
                {
                    collidersToRemove.Add(collider);
                    continue;
                }

                // Infliggi danno se il collider ha un componente HealthBar
                if (collider.gameObject.TryGetComponent<HealthBar>(out HealthBar barComponent))
                {
                    barComponent.TakeDamage();
                }
                // Infliggi danno se il collider ha un componente EnemyAI
                else if (collider.gameObject.TryGetComponent<EnemyAI>(out EnemyAI AIbarComponent))
                {
                    AIbarComponent.TakeDamage(1);
                }
            }

            // Rimuovi i collider nulli dalla lista dopo l'iterazione
            foreach (var collider in collidersToRemove)
            {
                collidersInTrap.Remove(collider);
            }
            collidersToRemove.Clear();
        }

        // Cambia lo sprite se è trascorso l'intervallo di swap
        if (timeSinceLastSwap >= swapinterval)
        {
            timeSinceLastSwap = 0f;
            SwapSprite();
        }
    }

    // Metodo per cambiare lo sprite della trappola
    private void SwapSprite()
    {
        if (sprites.Count > 0)
        {
            // Aggiorna l'indice dello sprite e imposta il nuovo sprite
            currentSpriteIndex = (currentSpriteIndex + 1) % sprites.Count;
            spriteRenderer.sprite = sprites[currentSpriteIndex];
        }
    }

    // Metodo chiamato quando un oggetto entra nel box di collisione
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Aggiungi il collider alla lista se non è già presente
        if (!collidersInTrap.Contains(collision))
        {
            collidersInTrap.Add(collision);
        }

        // Infliggi danno immediatamente se la trappola è attiva
        if (collision.gameObject.TryGetComponent<HealthBar>(out HealthBar barComponent) && trapActive)
        {
            barComponent.TakeDamage();
        }

        // Infliggi danno all'enemy immediatamente se la trappola è attiva
        else if (collision.gameObject.TryGetComponent<EnemyAI>(out EnemyAI AIbarComponent) && trapActive)
        {
            AIbarComponent.TakeDamage(1);
        }
    }

    // Metodo chiamato quando un oggetto esce dal box di collisione
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Rimuovi il collider dalla lista quando esce dal box di collisione
        if (collidersInTrap.Contains(collision))
        {
            collidersInTrap.Remove(collision);
        }
    }
}
