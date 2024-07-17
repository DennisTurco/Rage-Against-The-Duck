using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public GameObject entryRoom;
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    public GameObject closedRoom;
    public List<GameObject> closedRoomInstance = new List<GameObject>();
    private List<GameObject> rooms = new List<GameObject>();
    public int roomsSpawned = 0;
    public int maxRoomsToSpawn = 12;
    public int minRoomsToSpawn = 8;

    // for boss room
    private bool spawnedBoss;
    public GameObject boss;
    private GameObject spawnedBossInstance; // boss instance
    private GameObject entryRoomInstance;    // entry room instance

    private float timeAfterLastRoomSpawned;
    private float timeLimitRoomSpawned;
    private bool spawningInProgress;

    private void Start()
    {
        spawnedBoss = false;
        spawningInProgress = false;
    }

    private void Update()
    {
        if (spawningInProgress && timeAfterLastRoomSpawned >= timeLimitRoomSpawned && !spawnedBoss)
        {
            if (!checkCorrectness())
            {
                StartRoomsSpawning();
            }
            else
            {
                SpawnBossRoom();
                spawningInProgress = false;
                SpawnPlayer();
                return;
            }
        }
        else if (spawningInProgress)
        {
            timeAfterLastRoomSpawned += Time.deltaTime;
        }
    }

    public bool checkCorrectness()
    {
        return roomsSpawned >= minRoomsToSpawn && roomsSpawned <= maxRoomsToSpawn;
    }

    public void StartRoomsSpawning()
    {
        ClearRooms();
        entryRoomInstance = Instantiate(entryRoom, transform.position, entryRoom.transform.rotation); // entry room
    }

    public void ClearRooms()
    {
        foreach (var room in rooms)
        {
            Destroy(room);
        }
        rooms.Clear();
        roomsSpawned = 0;

        foreach (var closedRooms in closedRoomInstance)
        {
            Destroy(closedRooms);
        }
        closedRoomInstance.Clear();

        if (spawnedBoss)
        {
            Destroy(spawnedBossInstance);
            spawnedBoss = false;
        }
    }

    public void AddRoom(GameObject room)
    {
        rooms.Add(room);
        spawningInProgress = true;
        timeAfterLastRoomSpawned = 0;
    }

    public void SetTimeLimitRoomSpawned(float time)
    {
        timeLimitRoomSpawned = time;
    }

    private void SpawnBossRoom()
    {
        spawnedBossInstance = Instantiate(boss, rooms[rooms.Count - 1].transform.position, Quaternion.identity); // boss room
        spawnedBoss = true;
    }
    private void SpawnPlayer()
    {
        var playerSpawner = entryRoomInstance.GetComponentInChildren<PlayerSpawner>();
        if (playerSpawner != null)
        {
            playerSpawner.SpawnEverything();
        }
    }
}