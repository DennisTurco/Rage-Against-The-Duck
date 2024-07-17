using System.Collections.Generic;
using System.Linq;
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

	public float waitTime = 1f;
	public float spawnTimeInterval = 0.05f;

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

			var position = transform.position;

            if (openingDirection == 1){
				// Need to spawn a room with a BOTTOM door.
				if (!terminalRoom)
					rand = Random.Range(0, templates.bottomRooms.Length);
				else
					rand = 0;

				var room = templates.bottomRooms[rand];
                var offset = CalculateSpawnPositionOfTheRoom(room, 2);
                Instantiate(room, position + offset, room.transform.rotation);
			} 
			else if(openingDirection == 2){
                // Need to spawn a room with a TOP door.
                if (!terminalRoom)
                    rand = Random.Range(0, templates.topRooms.Length);
                else
                    rand = 0;

				var room = templates.topRooms[rand];
                var offset = CalculateSpawnPositionOfTheRoom(room, 1);
                Instantiate(room, position + offset, room.transform.rotation);
			} 
			else if(openingDirection == 3){
                // Need to spawn a room with a LEFT door.
                if (!terminalRoom)
                    rand = Random.Range(0, templates.leftRooms.Length);
                else
                    rand = 0;

				var room = templates.leftRooms[rand];
                var offset = CalculateSpawnPositionOfTheRoom(room, 4);
                Instantiate(room, position + offset, room.transform.rotation);
			} 
			else if(openingDirection == 4){
                // Need to spawn a room with a RIGHT door.
                if (!terminalRoom)
                    rand = Random.Range(0, templates.rightRooms.Length);
                else
                    rand = 0;

				var room = templates.rightRooms[rand];
                var offset = CalculateSpawnPositionOfTheRoom(room, 3);
                Instantiate(room, position + offset, room.transform.rotation);
			}
			spawned = true;
			templates.roomsSpawned++;
		}
	}

    // this function allow to calculate the distance between the center and the exit door of the targetRoom 
    private Vector3 CalculateSpawnPositionOfTheRoom(GameObject targetObject, int doorRequired)
    {
		List<RoomSpawner> kids = targetObject.GetComponentsInChildren<RoomSpawner>().ToList();
		RoomSpawner room;
        foreach (var kid in kids)
        {
            if (kid.openingDirection == doorRequired)
            {
				room = kid;
                return targetObject.transform.position - room.transform.position;
            }
        }

		return Vector3.zero;
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