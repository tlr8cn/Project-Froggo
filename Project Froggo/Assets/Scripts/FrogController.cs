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

    public EnemyScript enemyScript;

    public GameObject frogTongue;
    public bool attacking;
    public float attackCounter;
    public float attackReturnCounter;
    public bool attackReturning;



	float jumpMax = 2.56f;
	public float attackCap = 0.2f;
	public float attackReturnCap = 0.13f;


	//public string attackKey;


	public Dictionary < string, List<GameObject> > keyToEnemies;


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

		GameObject enemyCommunicator = GameObject.Find("Enemy Communicator");
		enemyScript = enemyCommunicator.GetComponent<EnemyScript>();

		frogTongue = transform.GetChild(0).gameObject;

		keyToEnemies = new Dictionary< string, List<GameObject> >();

		keyToEnemies.Add(KeyCode.D.ToString(), new List<GameObject>());
		keyToEnemies.Add(KeyCode.S.ToString(), new List<GameObject>());
		keyToEnemies.Add(KeyCode.A.ToString(), new List<GameObject>());
		keyToEnemies.Add(KeyCode.W.ToString(), new List<GameObject>());

	}
	
	// Update is called once per frame
	void Update () {
		anim.SetBool("Jumping", jumping);

		if(backToOriginalTile) {

			goBackToOriginalTile();

		} else {

			if(jumping && !wallBumped) {

				continueJumping();

			} else if(jumping && wallBumped) {

				cancelJump();

			} else if(attacking) {
				attackCounter += Time.deltaTime;
				if(attackCounter >= attackCap) {
					//attacking = false;
					anim.SetBool("Attack", false);
					frogTongue.GetComponent<Animator>().SetBool("Out", false);
					//enemyScript.frogMoved = true;
					attackReturning = true;
					InvokeRepeating("CameraShake", 0, repeatTime);
					Invoke("StopShaking", shakeDuration);
				}

				if(attackReturning) {
					attackReturnCounter += Time.deltaTime;
					if(attackCounter >= attackCap) {
						//enemyScript.frogMoved = true;
						attacking = false;
						attackReturning = false;
					}
				}
			}
		}

		if(isPlayerOne && !movementDisabled /*&& enemyScript.enemiesMoved*/) {
/*
			if (Input.GetKeyDown("space") && !jumping && !backToOriginalTile && !attacking) {
				anim.SetBool("Attack", true);
				frogTongue.GetComponent<Animator>().SetBool("Out", true);
				attackCounter = 0f;
				attacking = true;
			}
*/
			//Up
			if (Input.GetKeyDown(KeyCode.W) && !jumping && !backToOriginalTile && !attacking) {
				transform.eulerAngles = new Vector3(0f, 0f, 0f);

				struggleInWeb();

				if(!caughtInWeb) {

					if(keyToEnemies[KeyCode.W.ToString()].Count <= 0)
						startJump();
					else {
						anim.SetBool("Attack", true);
						frogTongue.GetComponent<Animator>().SetBool("Out", true);
						attackCounter = 0f;
						attacking = true;

						foreach(GameObject enemy in keyToEnemies[KeyCode.W.ToString()]) {
							enemyScript.sendDamage(enemy);
						}
					}
				}
			}
			//Down
			else if (Input.GetKeyDown(KeyCode.S) && !jumping && !backToOriginalTile && !attacking) {
				transform.eulerAngles = new Vector3(0f, 0f, 180f);
				
				struggleInWeb();

				if(!caughtInWeb) {

					if(keyToEnemies[KeyCode.S.ToString()].Count <= 0)
						startJump();
					else {
						anim.SetBool("Attack", true);
						frogTongue.GetComponent<Animator>().SetBool("Out", true);
						attackCounter = 0f;
						attacking = true;

						foreach(GameObject enemy in keyToEnemies[KeyCode.S.ToString()]) {
							enemyScript.sendDamage(enemy);
						}
					}	
				}
			}
			//Left
			else if (Input.GetKeyDown(KeyCode.A) && !jumping && !backToOriginalTile && !attacking) {
				transform.eulerAngles = new Vector3(0f, 0f, 90f);

				struggleInWeb();

				if(!caughtInWeb) {

					if(keyToEnemies[KeyCode.A.ToString()].Count <= 0)
						startJump();
					else {
						anim.SetBool("Attack", true);
						frogTongue.GetComponent<Animator>().SetBool("Out", true);
						attackCounter = 0f;
						attacking = true;

						foreach(GameObject enemy in keyToEnemies[KeyCode.A.ToString()]) {
							enemyScript.sendDamage(enemy);
						}
					}
				}
			}
			//Right
			else if (Input.GetKeyDown(KeyCode.D) && !jumping && !backToOriginalTile && !attacking) {
				transform.eulerAngles = new Vector3(0f, 0f, -90f);	

				struggleInWeb();

				if(!caughtInWeb) {

					if(keyToEnemies[KeyCode.D.ToString()].Count <= 0)
						startJump();
					else {
						anim.SetBool("Attack", true);
						frogTongue.GetComponent<Animator>().SetBool("Out", true);
						attackCounter = 0f;
						attacking = true;

						foreach(GameObject enemy in keyToEnemies[KeyCode.D.ToString()]) {
							enemyScript.sendDamage(enemy);
						}
					}
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
    }

    void struggleInWeb() {
    	if(caughtInWeb) {
			InvokeRepeating("CameraShake", 0, repeatTime);
			Invoke("StopShaking", shakeDuration);
			webMoveCounter -= 1;
			Color tmp = webCaughtIn.GetComponent<SpriteRenderer>().color;
			tmp.a = tmp.a - 0.2f;
			webCaughtIn.GetComponent<SpriteRenderer>().color = tmp;

			/*
			if(webMoveCounter > 0) {
				enemyScript.frogMoved = true;
			} else {
			*/
			if(webMoveCounter == 0) {
				anim.enabled = true;
				frogWeb.GetComponent<Animator>().SetBool("Break", true);
				caughtInWeb = false;
				
				//create illusion that web is destroyed
				tmp.a = 0f;
				webCaughtIn.GetComponent<SpriteRenderer>().color = tmp;
				webCaughtIn.GetComponent<BoxCollider2D>().enabled = false;
				webCaughtIn.GetComponent<WebScript>().webDestroyed = true;
			}
			//}
		}
    }


    void goBackToOriginalTile() {
    	transform.Translate(jumpSpeed*Vector3.down);
		if(transform.position == originalPosition) {
			backToOriginalTile = false;
			anim.enabled = true;
		//	enemyScript.frogMoved = true;
		}
    }

    void cancelJump() {
    	jumping = false;
		wallBumped = false;
		backToOriginalTile = true;
		anim.enabled = false;
    }

    void continueJumping() {
    	transform.Translate(jumpSpeed*Vector3.up);
		jumpProgress += jumpSpeed;
		if(jumpProgress == jumpMax) {
			jumping = false;
		//	enemyScript.frogMoved = true;
			originalPosition = transform.position;
			if(caughtInWeb)
				anim.enabled = false;
		}
    }

    void startJump() {
    	jumping = true;
		jumpProgress = 0f;
    }

}
