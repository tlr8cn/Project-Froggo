using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour {

	public GameObject[] rooms;
	public GameObject currentRoom;
	public GameObject nextRoom;
	public bool fadeOutComplete;
	public bool fadeInComplete;
	public bool changingRooms;
	public GameObject playerFrog;
	FrogController frogController;
	public float fadeSpeed;

	// Use this for initialization
	void Start () {
		changingRooms = false;

		hideUnactiveRooms();

		frogController = playerFrog.GetComponent<FrogController>();

	}
	
	// Update is called once per frame
	void Update () {
		//GameObject room = rooms[0];

		if(changingRooms) {
			frogController.movementDisabled = true;
			fadeOut(currentRoom);
			fadeIn(nextRoom);

			if(fadeOutComplete && fadeInComplete) {
				changingRooms = false;
				currentRoom.tag = "Untagged";
				nextRoom.tag = "Current Room";
				currentRoom = nextRoom;
				nextRoom = null;
				frogController.movementDisabled = false;
			}
		}


	}

	void fadeOut(GameObject room) {
		bool stillFadingOut = false;
		foreach (Transform child in room.transform) {
			Color tmp = child.GetComponent<SpriteRenderer>().color;
			tmp.a = tmp.a - fadeSpeed;
			child.GetComponent<SpriteRenderer>().color = tmp;
			if(tmp.a > 0f)
				stillFadingOut = true;
		}

		fadeOutComplete = !stillFadingOut;

	}

	void fadeIn(GameObject room) {
		bool stillFadingIn = false;
		foreach (Transform child in room.transform) {
			Color tmp = child.GetComponent<SpriteRenderer>().color;
			tmp.a = tmp.a + fadeSpeed;
			child.GetComponent<SpriteRenderer>().color = tmp;
			if(tmp.a < 1f)
				stillFadingIn = true;
		}

		fadeInComplete = !stillFadingIn;

	}

	void hideUnactiveRooms() {
		for(int i = 0; i < rooms.Length; i++) {
			if(rooms[i].tag != "Current Room") {
				makeRoomInvisible(rooms[i]);
			}
		}
	}

	void makeRoomInvisible(GameObject room) {
		foreach (Transform child in room.transform) {
			Color tmp = child.GetComponent<SpriteRenderer>().color;
			tmp.a = 0f;
			child.GetComponent<SpriteRenderer>().color = tmp;
		}
	}

}
