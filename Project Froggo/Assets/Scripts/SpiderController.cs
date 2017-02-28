using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SpiderController : MonoBehaviour {

	EnemyScript enemyScript;

	public bool inAttackRange;

	public GameObject web;

	public GameObject[] webs;

	public bool readyToMove;

	public float moveSpeed;
	public float moveProgress;
	float moveMax = 2.56f;
	public bool moving;
	public bool doneMoving;

	//keeps track of the next web to be traversed by the spider
	public GameObject nextWeb;

	//web arrangements (exactly one should be true at a time)
	public bool horizontalLine;
	public bool loop;


	//used to navigate the horizontalLine arrangement
	public bool movingBack;

	public int webIndx;

	// Use this for initialization
	void Start () {
		moving = false;

		inAttackRange = false;

		GameObject enemyCommunicator = GameObject.Find("Enemy Communicator");
		enemyScript = enemyCommunicator.GetComponent<EnemyScript>();

		enemyScript.enemies.Add(gameObject);

		readyToMove = false;

		doneMoving = false;

		webIndx = 0;

		if(webs.Length > 1) {
			nextWeb = webs[1];
		}

		if(horizontalLine && !movingBack)
			transform.eulerAngles = new Vector3(0f, 0f, -90f);
		else if(horizontalLine && movingBack)
			transform.eulerAngles = new Vector3(0f, 0f, 90f);

		if(loop) { //default to bottom left being the starting point and moving counterclockwise
			transform.eulerAngles = new Vector3(0f, 0f, -90f);
		}

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


		if(horizontalLine && !movingBack) {
			transform.eulerAngles = new Vector3(0f, 0f, -90f);	

			if(webIndx+1 == webs.Length-1) {
				movingBack = true;
				webIndx += 1;
				nextWeb = webs[webIndx - 1];
			} else {
				webIndx += 1;
				nextWeb = webs[webIndx + 1];
			}

		} else if(horizontalLine && movingBack) {
			transform.eulerAngles = new Vector3(0f, 0f, 90f);	

			if(webIndx-1 == 0) {
				movingBack = false;
				webIndx -= 1;
				nextWeb = webs[webIndx + 1];
			} else {
				webIndx -= 1;
				nextWeb = webs[webIndx - 1];
			}
		} else if (loop) {
			if(withinError(nextWeb.transform.position.y, transform.position.y) && nextWeb.transform.position.x > transform.position.x) { // look right
				transform.eulerAngles = new Vector3(0f, 0f, -90f);	
			} else if (withinError(nextWeb.transform.position.y, transform.position.y) && nextWeb.transform.position.x < transform.position.x) { // look left
				transform.eulerAngles = new Vector3(0f, 0f, 90f);	
			} else if (withinError(nextWeb.transform.position.x, transform.position.x) && nextWeb.transform.position.y < transform.position.y) { // look down
				transform.eulerAngles = new Vector3(0f, 0f, 180f);
			} else if (withinError(nextWeb.transform.position.x, transform.position.x) && nextWeb.transform.position.y > transform.position.y) { // look up
				transform.eulerAngles = new Vector3(0f, 0f, 0f);
			}

			if(webIndx == webs.Length-1) {
				webIndx = 0;
				nextWeb = webs[1];
			} else if(webIndx == webs.Length-2){
				webIndx += 1;
				nextWeb = webs[0];
			} else {
				webIndx += 1;
				nextWeb = webs[webIndx+1];
			}

		}

    }

    void continueMoving() {
    	transform.Translate(moveSpeed*Vector3.up);
		moveProgress += moveSpeed;
		if(moveProgress == moveMax) {
			moving = false;
			doneMoving = true;
			if(webs[webIndx].GetComponent<BoxCollider2D>().enabled == false)
				replaceWeb();
		}

    }

    void replaceWeb() {
    	//GameObject newWeb = (GameObject)Instantiate(web, transform.position, Quaternion.identity);
    	Color tmp = webs[webIndx].GetComponent<SpriteRenderer>().color;
		tmp.a = 1f;
		webs[webIndx].GetComponent<SpriteRenderer>().color = tmp;
    	webs[webIndx].GetComponent<BoxCollider2D>().enabled = true;
    }

    bool withinError(float x1, float x2) {
    	if(Math.Abs(x1 - x2) < 0.0025f) {
    		return true;
    	}
    	return false;
    }

}
