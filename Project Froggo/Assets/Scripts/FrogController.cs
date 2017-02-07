using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : MonoBehaviour {

	public float jumpSpeed;
	public float jumpProgress;
	public bool jumping;

	float jumpMax = 2.56f;

	Animator anim;


	// Use this for initialization
	void Start () {
		jumping = false;

		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		anim.SetBool("Jumping", jumping);

		if(jumping) {
			transform.Translate(jumpSpeed*Vector3.up);
			jumpProgress += jumpSpeed;
			if(jumpProgress == jumpMax) {
				jumping = false;
			}
		}


		//Up
		if (Input.GetKeyDown(KeyCode.W) && !jumping) {
			jumping = true;
			jumpProgress = 0f;
			transform.eulerAngles = new Vector3(0f, 0f, 0f);
		}

		//Down
		if (Input.GetKeyDown(KeyCode.S) && !jumping) {
			jumping = true;
			jumpProgress = 0f;
			transform.eulerAngles = new Vector3(0f, 0f, 180f);
		}

		//Left
		if (Input.GetKeyDown(KeyCode.A) && !jumping) {
			jumping = true;
			jumpProgress = 0f;
			transform.eulerAngles = new Vector3(0f, 0f, 90f);
		}

		//Right
		if (Input.GetKeyDown(KeyCode.D) && !jumping) {	
			jumping = true;
			jumpProgress = 0f;
			transform.eulerAngles = new Vector3(0f, 0f, -90f);
		}
	}

	void FixedUpdate() {

	}
}
