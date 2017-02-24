using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebScript : MonoBehaviour {

	GameObject player;
	FrogController playerScript;

	public GameObject frogWeb;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Frog");
		playerScript = player.GetComponent<FrogController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.tag == "Frog") {
			playerScript.caughtInWeb = true;
			playerScript.webMoveCounter = 4;
			playerScript.webCaughtIn = gameObject;
			GameObject temp = (GameObject)Instantiate(frogWeb, player.transform.position, Quaternion.identity);
			playerScript.frogWeb = temp;
			playerScript.frogWeb.transform.SetParent(player.transform);
			
		}
	}
}
