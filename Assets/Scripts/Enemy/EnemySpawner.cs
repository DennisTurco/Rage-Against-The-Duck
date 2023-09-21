using System.Collections;
using Unity.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Spawn Settings")]
    [SerializeField] private float spawnRate;
    [SerializeField] private GameObject[] enemyPrefabs;
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

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);

        while (canSpawn && enemyPrefabs.Length > 0)
        {
            if (enemyToSpawnLimit && enemySpawned >= enemyToSpawn)
            {
                Debug.Log("DASIUGDIYJASGDIOYGASIOYDGASJHDGJHASGDJASHGDJYASG");
                yield break;
            }

            yield return wait;
            
            int rand = Random.Range(0, enemyPrefabs.Length);
            GameObject enemy = enemyPrefabs[rand];
            Instantiate(enemy, transform.position, Quaternion.identity);
            enemySpawned++;

            Debug.Log("Spawned An Enemy");
        }
    }
}
