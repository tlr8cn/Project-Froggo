using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomChange : MonoBehaviour {

	public GameObject mapController;
	MapController mapScript;

	public GameObject nextRoom;

	// Use this for initialization
	void Start () {
		mapScript = mapController.GetComponent<MapController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnTriggerEnter2D(Collider2D col) {
		if(col.tag == "Frog" && nextRoom.tag != "Current Room") {
			mapScript.nextRoom = nextRoom;
			mapScript.changingRooms = true;
		}
	}
}
