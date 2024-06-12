using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructablePot : MonoBehaviour
{
    [SerializeField] private int hitsToDestroy = 3;
    [SerializeField] private float animationFrameDelay = 0.1f; // Delay tra i frame dell'animazione
    [SerializeField] private Sprite[] hitAnimationFrames; // I frame dell'animazione del colpo
    [SerializeField] private Sprite[] destructionAnimationFrames; // I frame dell'animazione di distruzione
    [SerializeField] private GameObject droppedItemPrefab;
    [SerializeField] private List<LootSpawn> lootList = new List<LootSpawn>(); // Lista degli oggetti droppabili
    [SerializeField] private int maxNumberOfDrops = 3; // Numero massimo di oggetti droppabili
    [SerializeField] private Collider2D playerCollider; // Collider per la collisione con il player
    [SerializeField] private Collider2D destructionCollider; // Collider per la distruzione

    private SpriteRenderer spriteRenderer;
    private bool isDestroyed = false;
    private int currentHits = 0;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerCollider.enabled = true;
        destructionCollider.enabled = true;
    }

    private IEnumerator PlayHitAnimation()
    {
        // Interrompi eventuali animazioni in corso
        StopCoroutine(PlayDestructionAnimation());

        // Riproduci l'animazione del colpo frame per frame
        foreach (var frame in hitAnimationFrames)
        {
            spriteRenderer.sprite = frame;
            yield return new WaitForSeconds(animationFrameDelay);
        }

        // Mantieni l'ultimo frame dell'animazione del colpo per un breve periodo
        yield return new WaitForSeconds(animationFrameDelay);

        // Ripristina lo sprite originale (facoltativo)
        // spriteRenderer.sprite = hitAnimationFrames[0];
    }

    public void TakeHit()
    {
        if (isDestroyed) return;

        currentHits++;

        if (currentHits < hitsToDestroy)
        {
            // Gioca l'animazione del colpo
            StartCoroutine(PlayHitAnimation());
        }
        else
        {
            // Distruggi il vaso
            DestroyPot();
        }
    }

    public void DestroyPot() // Modificato a 'public'
    {
        if (!isDestroyed)
        {
            isDestroyed = true;
            StartCoroutine(PlayDestructionAnimation());
        }
    }

    private IEnumerator PlayDestructionAnimation()
    {
        // Riproduci l'animazione di distruzione frame per frame
        foreach (var frame in destructionAnimationFrames)
        {
            spriteRenderer.sprite = frame;
            yield return new WaitForSeconds(animationFrameDelay);
        }

        // Mantieni l'ultimo frame dell'animazione
        spriteRenderer.sprite = destructionAnimationFrames[destructionAnimationFrames.Length - 1];

        // Disabilita i collider
        playerCollider.enabled = false;
        destructionCollider.enabled = false;

        // Droppa il loot
        InstantiateLootSpawn(transform.position);
    }

    private void InstantiateLootSpawn(Vector3 spawnPosition)
    {
        List<LootSpawn> droppedItems = GetDroppedItems();

        if (droppedItems == null || droppedItems.Count == 0) return;

        foreach (var droppedItem in droppedItems)
        {
            // Aggiungi un offset casuale alla posizione di spawn
            Vector3 randomOffset = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
            Vector3 itemPosition = spawnPosition + randomOffset;
            GameObject lootGameObject = Instantiate(droppedItemPrefab, itemPosition, Quaternion.identity);
            lootGameObject.GetComponent<SpriteRenderer>().sprite = droppedItem.lootSprite;

            // Imposta il nome personalizzato al prefab
            lootGameObject.name = droppedItem.lootName.ToString();

            // Applica una forza iniziale all'oggetto per "lanciarlo"
            Rigidbody2D rb = lootGameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 launchDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(0.5f, 1f)).normalized;
                float launchForce = Random.Range(2f, 5f);
                rb.AddForce(launchDirection * launchForce, ForceMode2D.Impulse);
            }

            // Assicurati che l'oggetto sia assegnato al layer corretto
            lootGameObject.layer = LayerMask.NameToLayer("Item");
        }
    }

    private List<LootSpawn> GetDroppedItems()
    {
        List<LootSpawn> droppedItems = new List<LootSpawn>();

        for (int i = 0; i < maxNumberOfDrops; i++)
        {
            int random = Random.Range(1, 101);  // random tra 1 e 100
            foreach (LootSpawn item in lootList)
            {
                if (random <= item.dropChance)
                {
                    droppedItems.Add(item); // Cambiato 'add' a 'Add'
                    break; // Drop solo un item per iterazione
                }
            }
        }

        if (droppedItems.Count > 0)
        {
            return droppedItems;
        }

        Debug.Log("No loot dropped");
        return null;
    }
}
