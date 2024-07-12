using UnityEngine;

public class AddRoom : MonoBehaviour {

	private RoomTemplates templates;

	void Start(){
		templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
		templates.AddRoom(this.gameObject);
	}
}
