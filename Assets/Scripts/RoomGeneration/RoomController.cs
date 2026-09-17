using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Locks a room's doors and spawns an enemy pack the first time the player enters it,
// then reopens the doors once every spawned enemy has been killed.
public class RoomController : MonoBehaviour
{
    [Header("Enemies")]
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private int minEnemies = 2;
    [SerializeField] private int maxEnemies = 4;
    [SerializeField] private float enemySpawnRadius = 3f;

    [Header("Door lock")]
    [SerializeField] private GameObject doorBlockerPrefab;
    // Closed Wall.prefab is authored at room-cell size (~10x10) with a blank placeholder sprite,
    // so it has to be shrunk to just the doorway gap. There's no real "closed door" art yet, so
    // it's tinted dark instead of the default blank-white look, to at least read as a solid wall.
    [SerializeField] private Vector2 doorBlockerSize = new Vector2(2.5f, 2.5f);
    [SerializeField] private Color doorBlockerColor = new Color(0.15f, 0.13f, 0.12f);

    [Header("Room bounds (trigger used to detect the player entering)")]
    // Kept comfortably smaller than the ~9-unit room-to-room spacing (rooms overlap by
    // RoomSpawner.wallOverlap) so the lock only triggers once the player is clearly inside,
    // not while still standing on the doorway threshold.
    [SerializeField] private Vector2 roomTriggerSize = new Vector2(6f, 6f);
    [SerializeField] private Vector2 roomTriggerOffset = Vector2.zero;

    private readonly List<GameObject> spawnedEnemies = new List<GameObject>();
    private readonly List<GameObject> doorBlockers = new List<GameObject>();
    private readonly List<Vector3> doorPositions = new List<Vector3>();
    private bool activated;
    private bool cleared;

    private BoxCollider2D roomTrigger;

    private void Awake()
    {
        roomTrigger = gameObject.AddComponent<BoxCollider2D>();
        roomTrigger.isTrigger = true;
        roomTrigger.size = roomTriggerSize;
        roomTrigger.offset = roomTriggerOffset;
        // Kept disabled until the whole level has finished generating (see Start): while rooms
        // are still being procedurally placed, RoomTemplates.CheckDoorsCorrectness() scans the
        // area around every door and fails generation if it finds any collider that isn't
        // tagged "SpawnPoint" - which this trigger would be mistaken for otherwise.
        roomTrigger.enabled = false;

        // RoomSpawner markers self-destruct a couple seconds after the level finishes generating
        // (their job during generation is done), so their door positions must be cached now,
        // before the player ever gets a chance to walk in and trigger a lock.
        foreach (RoomSpawner spawnPoint in GetComponentsInChildren<RoomSpawner>())
        {
            doorPositions.Add(spawnPoint.transform.position);
        }
    }

    private void Start()
    {
        StartCoroutine(EnableTriggerAfterLevelIsGenerated());
    }

    private IEnumerator EnableTriggerAfterLevelIsGenerated()
    {
        yield return new WaitUntil(() => GameManager.Instance != null && GameManager.Instance.playerInitialized);
        roomTrigger.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (activated || !other.CompareTag("Player")) return;

        activated = true;
        SpawnEnemies();

        if (spawnedEnemies.Count > 0)
        {
            LockDoors();
        }
        else
        {
            cleared = true;
        }
    }

    private void SpawnEnemies()
    {
        if (enemyPrefabs == null || enemyPrefabs.Length == 0) return;

        int count = Random.Range(minEnemies, maxEnemies + 1);
        for (int i = 0; i < count; i++)
        {
            GameObject prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            Vector2 offset = Random.insideUnitCircle * enemySpawnRadius;
            GameObject enemy = Instantiate(prefab, transform.position + (Vector3)offset, Quaternion.identity, transform);
            spawnedEnemies.Add(enemy);
        }
    }

    private void LockDoors()
    {
        if (doorBlockerPrefab == null) return;

        foreach (Vector3 doorPosition in doorPositions)
        {
            GameObject blocker = Instantiate(doorBlockerPrefab, doorPosition, Quaternion.identity, transform);
            blocker.transform.localScale = Vector3.one;

            if (blocker.TryGetComponent(out BoxCollider2D blockerCollider))
            {
                blockerCollider.size = doorBlockerSize;
            }

            if (blocker.TryGetComponent(out SpriteRenderer spriteRenderer))
            {
                spriteRenderer.color = doorBlockerColor;
            }

            doorBlockers.Add(blocker);
        }
    }

    private void Update()
    {
        if (!activated || cleared) return;

        spawnedEnemies.RemoveAll(enemy => enemy == null);

        if (spawnedEnemies.Count == 0)
        {
            cleared = true;
            UnlockDoors();
        }
    }

    private void UnlockDoors()
    {
        foreach (GameObject blocker in doorBlockers)
        {
            if (blocker != null) Destroy(blocker);
        }
        doorBlockers.Clear();
    }
}
