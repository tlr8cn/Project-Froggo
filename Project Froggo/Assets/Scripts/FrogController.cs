using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : MonoBehaviour {



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() {
		//Up
		if (Input.GetKeyDown(KeyCode.W)) {
			transform.eulerAngles = new Vector3(0f, 0f, 0f);
			transform.Translate(2.56f*Vector3.up);
		}

		//Down
		if (Input.GetKeyDown(KeyCode.S)) {
			transform.eulerAngles = new Vector3(0f, 0f, 180f);
			transform.Translate(2.56f*Vector3.up);
		}

		//Left
		if (Input.GetKeyDown(KeyCode.A)) {
			transform.eulerAngles = new Vector3(0f, 0f, 90f);
			transform.Translate(2.56f*Vector3.up);
		}

		//Right
		if (Input.GetKeyDown(KeyCode.D)) {
			transform.eulerAngles = new Vector3(0f, 0f, -90f);
			transform.Translate(2.56f*Vector3.up);
		}
	}
}
