using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Spawn Settings")]
    [SerializeField] private float spawnRate;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private List<EnemyGeneric> enemies;
    [SerializeField] private bool canSpawn = true;

    [Header("Enemy Spawn Limits Settings")]
    [SerializeField] private bool enemyToSpawnLimit;
    [SerializeField] private int enemyToSpawn;
    [SerializeField] private int enemySpawned;

    // Start is called before the first frame update
    void Start()
    {
        enemySpawned = 0;
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);

        while (canSpawn && enemies.Count > 0)
        {
            if (enemyToSpawnLimit && enemySpawned >= enemyToSpawn)
            {
                yield break;
            }

            yield return wait;
            
            int rand = Random.Range(0, enemies.Count);
            GameObject enemyObject = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            enemyObject.GetComponent<SpriteRenderer>().sprite = enemies[rand].entitySprite;
            Debug.Log(enemies[rand].entitySprite.name);
            Debug.Log(enemyObject.GetComponent<SpriteRenderer>().sprite.name);
            enemySpawned++;

            // Ensure the object is assigned to the correct layer
            enemyObject.layer = LayerMask.NameToLayer("Enemy");

            Debug.Log("Spawned An Enemy");
        }
    }
}
