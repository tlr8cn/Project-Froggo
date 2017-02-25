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

	public bool wallBumped;
	bool backToOriginalTile;
	Vector3 originalPosition;

	public float shakeAmt = 0;

    public Camera mainCamera;

    public float shakeDuration;
    public float repeatTime;

	float jumpMax = 2.56f;


	Animator anim;


	// Use this for initialization
	void Start () {
		jumping = false;

		anim = GetComponent<Animator>();

		caughtInWeb = false;
		webMoveCounter = 0;

		movementDisabled = false;

		wallBumped = false;
		backToOriginalTile = false;

		mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		anim.SetBool("Jumping", jumping);

		if(backToOriginalTile) {
			transform.Translate(jumpSpeed*Vector3.down);
			if(transform.position == originalPosition) {
				backToOriginalTile = false;
				anim.enabled = true;
			}
		} else {

			if(jumping && !wallBumped) {
				transform.Translate(jumpSpeed*Vector3.up);
				jumpProgress += jumpSpeed;
				if(jumpProgress == jumpMax) {
					jumping = false;
					originalPosition = transform.position;
					if(caughtInWeb)
						anim.enabled = false;
				}
			} else if(jumping && wallBumped) {
				jumping = false;
				wallBumped = false;
				backToOriginalTile = true;
				anim.enabled = false;
			}
		}

		if(isPlayerOne && !movementDisabled) {
			//Up
			if (Input.GetKeyDown(KeyCode.W) && !jumping && !backToOriginalTile) {
				transform.eulerAngles = new Vector3(0f, 0f, 0f);
				if(caughtInWeb) {
				    InvokeRepeating("CameraShake", 0, repeatTime);
				    Invoke("StopShaking", shakeDuration);
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
			if (Input.GetKeyDown(KeyCode.S) && !jumping && !backToOriginalTile) {
				transform.eulerAngles = new Vector3(0f, 0f, 180f);
				if(caughtInWeb) {
				    InvokeRepeating("CameraShake", 0, repeatTime);
				    Invoke("StopShaking", shakeDuration);
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
			if (Input.GetKeyDown(KeyCode.A) && !jumping && !backToOriginalTile) {
				transform.eulerAngles = new Vector3(0f, 0f, 90f);
				if(caughtInWeb) {
				    InvokeRepeating("CameraShake", 0, repeatTime);
				    Invoke("StopShaking", shakeDuration);
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
			if (Input.GetKeyDown(KeyCode.D) && !jumping && !backToOriginalTile) {
				transform.eulerAngles = new Vector3(0f, 0f, -90f);	
				if(caughtInWeb) {
				    InvokeRepeating("CameraShake", 0, repeatTime);
				    Invoke("StopShaking", shakeDuration);
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

	//TODO: Clean this up--methods are in two places
	void CameraShake()
    {
        if(shakeAmt>0) 
        {
            float quakeAmt = Random.value*shakeAmt*2 - shakeAmt;
            Vector3 pp = mainCamera.transform.position;
            pp.y+= quakeAmt; // can also add to x and/or z
            mainCamera.transform.position = pp;
        }
    }

    void StopShaking()
    {
        CancelInvoke("CameraShake");
       // mainCamera.transform.position = originalCameraPosition;
    }
}
