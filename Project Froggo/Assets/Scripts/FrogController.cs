using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : MonoBehaviour {

	public float jumpSpeed;
	public float jumpProgress;
	public bool jumping;

	public bool isPlayerOne;

	public bool caughtInWeb;
	public int webMoveCounter;
	public GameObject webCaughtIn;

	public GameObject frogWeb;

	public bool movementDisabled;

	float jumpMax = 2.56f;

	bool shook;
	float shakeSpeed;

	Animator anim;


	// Use this for initialization
	void Start () {
		jumping = false;

		anim = GetComponent<Animator>();

		caughtInWeb = false;
		webMoveCounter = 0;

		shook = false;
		shakeSpeed = 0.32f;

		movementDisabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		anim.SetBool("Jumping", jumping);

		if(jumping) {
			transform.Translate(jumpSpeed*Vector3.up);
			jumpProgress += jumpSpeed;
			if(jumpProgress == jumpMax) {
				jumping = false;
				if(caughtInWeb)
					anim.enabled = false;
			}
		}

		if(shook) {
			transform.Translate(shakeSpeed*Vector3.down);
			shook = false;
		}


		if(isPlayerOne && !shook && !movementDisabled) {
			//Up
			if (Input.GetKeyDown(KeyCode.W) && !jumping) {
				transform.eulerAngles = new Vector3(0f, 0f, 0f);
				if(caughtInWeb) {
					transform.Translate(shakeSpeed*Vector3.up);
					shook = true;
					webMoveCounter -= 1;
					Color tmp = webCaughtIn.GetComponent<SpriteRenderer>().color;
					tmp.a = tmp.a - 0.2f;
					webCaughtIn.GetComponent<SpriteRenderer>().color = tmp;
					if(webMoveCounter == 0) {
						anim.enabled = true;
						frogWeb.GetComponent<Animator>().SetBool("Break", true);
						caughtInWeb = false;
						Object.Destroy(webCaughtIn);
					}
				}

				if(!caughtInWeb) {
					jumping = true;
					jumpProgress = 0f;
					
				}
			}

			//Down
			if (Input.GetKeyDown(KeyCode.S) && !jumping) {
				transform.eulerAngles = new Vector3(0f, 0f, 180f);
				if(caughtInWeb) {
					transform.Translate(shakeSpeed*Vector3.up);
					shook = true;
					webMoveCounter -= 1;
					Color tmp = webCaughtIn.GetComponent<SpriteRenderer>().color;
					tmp.a = tmp.a - 0.2f;
					webCaughtIn.GetComponent<SpriteRenderer>().color = tmp;
					if(webMoveCounter == 0) {
						anim.enabled = true;
						frogWeb.GetComponent<Animator>().SetBool("Break", true);
						caughtInWeb = false;
						Object.Destroy(webCaughtIn);
					}
				}

				if(!caughtInWeb) {
					jumping = true;
					jumpProgress = 0f;
					
				}
			}

			//Left
			if (Input.GetKeyDown(KeyCode.A) && !jumping) {
				transform.eulerAngles = new Vector3(0f, 0f, 90f);
				if(caughtInWeb) {
					transform.Translate(shakeSpeed*Vector3.up);
					shook = true;
					webMoveCounter -= 1;
					Color tmp = webCaughtIn.GetComponent<SpriteRenderer>().color;
					tmp.a = tmp.a - 0.2f;
					webCaughtIn.GetComponent<SpriteRenderer>().color = tmp;
					if(webMoveCounter == 0) {
						anim.enabled = true;
						frogWeb.GetComponent<Animator>().SetBool("Break", true);
						caughtInWeb = false;
						Object.Destroy(webCaughtIn);
					}
				}

				if(!caughtInWeb) {
					jumping = true;
					jumpProgress = 0f;
					
				}
			}

			//Right
			if (Input.GetKeyDown(KeyCode.D) && !jumping) {
				transform.eulerAngles = new Vector3(0f, 0f, -90f);	
				if(caughtInWeb) {
					transform.Translate(shakeSpeed*Vector3.up);
					shook = true;
					webMoveCounter -= 1;
					Color tmp = webCaughtIn.GetComponent<SpriteRenderer>().color;
					tmp.a = tmp.a - 0.2f;
					webCaughtIn.GetComponent<SpriteRenderer>().color = tmp;
					if(webMoveCounter == 0) {
						anim.enabled = true;
						frogWeb.GetComponent<Animator>().SetBool("Break", true);
						caughtInWeb = false;
						Object.Destroy(webCaughtIn);
					}
				}

				if(!caughtInWeb) {
					jumping = true;
					jumpProgress = 0f;
					
				}
			}
		}
	}

	void FixedUpdate() {

	}
}
