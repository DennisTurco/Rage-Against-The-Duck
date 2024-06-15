using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour {

	public int openingDirection;
	// 0 --> no doors nedded
	// 1 --> need bottom door
	// 2 --> need top door
	// 3 --> need left door
	// 4 --> need right door

	private RoomTemplates templates;
	private int rand;
	public bool spawned = false;

	public float waitTime = 2f;
	public float spawnTimeInterval = 0.1f;


    void Start(){
		Destroy(gameObject, waitTime);
		templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
		templates.SetTimeLimitRoomSpawned(spawnTimeInterval);
        Invoke("Spawn", spawnTimeInterval);
	}

	void Spawn(){
		if(spawned == false){
			bool terminalRoom = false;
			if (templates.roomsSpawned >= templates.maxRoomsToSpawn-1)
			{
				terminalRoom = true;
			}

			if(openingDirection == 1){
				// Need to spawn a room with a BOTTOM door.
				if (!terminalRoom)
					rand = Random.Range(0, templates.bottomRooms.Length);
				else
					rand = 0;
				Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);
			} else if(openingDirection == 2){
                // Need to spawn a room with a TOP door.
                if (!terminalRoom)
                    rand = Random.Range(0, templates.topRooms.Length);
                else
                    rand = 0;
                Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
			} else if(openingDirection == 3){
                // Need to spawn a room with a LEFT door.
                if (!terminalRoom)
                    rand = Random.Range(0, templates.leftRooms.Length);
                else
                    rand = 0;
                Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
			} else if(openingDirection == 4){
                // Need to spawn a room with a RIGHT door.
                if (!terminalRoom)
                    rand = Random.Range(0, templates.rightRooms.Length);
                else
                    rand = 0;
                Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
			}
			spawned = true;
			templates.roomsSpawned++;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("SpawnPoint")){
			if(other.GetComponent<RoomSpawner>().spawned == false && spawned == false){
				templates.closedRoomInstance.Add(Instantiate(templates.closedRoom, transform.position, Quaternion.identity));
				Destroy(gameObject);
			} 
			spawned = true;
		}
	}
}