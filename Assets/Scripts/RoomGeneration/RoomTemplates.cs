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
    public List<GameObject> rooms = new List<GameObject>();
    public int roomsSpawned = 0;
    public int maxRoomsToSpawn = 10;
    public int minRoomsToSpawn = 5;

    // for boss room
    public float waitTime;
    private float currentWaitTime;
    private bool spawnedBoss;
    public GameObject boss;
    private GameObject spawnedBossInstance; // boss instance

    private void Update()
    {
        if (currentWaitTime <= 0 && !spawnedBoss)
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                if (i == rooms.Count - 1)
                {
                    spawnedBossInstance = Instantiate(boss, rooms[i].transform.position, Quaternion.identity);
                    spawnedBoss = true;
                }
            }
        }
        else
        {
            currentWaitTime -= Time.deltaTime;
        }
    }

    public void StartRoomsSpawning()
    {
        ClearRooms();
        currentWaitTime = waitTime;
        Instantiate(entryRoom, transform.position, entryRoom.transform.rotation);
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
}