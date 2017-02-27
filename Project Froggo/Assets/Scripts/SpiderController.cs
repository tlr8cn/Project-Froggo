using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderController : MonoBehaviour {

	EnemyScript enemyScript;

	public bool inAttackRange;

	public GameObject[] webs;

	public bool readyToMove;

	public float moveSpeed;
	public float moveProgress;
	float moveMax = 2.56f;
	public bool moving;
	public bool doneMoving;


	// Use this for initialization
	void Start () {
		moving = false;

		inAttackRange = false;

		GameObject enemyCommunicator = GameObject.Find("Enemy Communicator");
		enemyScript = enemyCommunicator.GetComponent<EnemyScript>();

		enemyScript.enemies.Add(gameObject);

		readyToMove = false;

		doneMoving = false;
	}
	
	// Update is called once per frame
	void Update () {

		if(moving) {
			continueMoving();
		}

		if(readyToMove) {
			startMove();
			readyToMove = false;
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.tag == "Frog") {
			inAttackRange = true;
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if(col.tag == "Frog") {
			inAttackRange = false;
		}
	}

	void startMove() {
    	moving = true;
		moveProgress = 0f;
    }

    void continueMoving() {
    	transform.Translate(moveSpeed*Vector3.up);
		moveProgress += moveSpeed;
		if(moveProgress == moveMax) {
			moving = false;
			doneMoving = true;

		}
    }

}
