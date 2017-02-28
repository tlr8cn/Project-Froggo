using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

	public List<GameObject> enemies;

	public bool frogMoved;

	public bool enemiesMoved;

	// Use this for initialization
	void Start () {
		enemies = new List<GameObject>();

		frogMoved = false;

		enemiesMoved = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(frogMoved) {
			enemyBroadcast();
			frogMoved = false;
			enemiesMoved = false;
		}

		if(!enemiesMoved) {
			checkOnEnemies();
		}
	}


	void enemyBroadcast() {
		foreach(GameObject enemy in enemies) {
	        switch (enemy.tag)
	        {
	            case "Spider":
	                enemy.GetComponent<SpiderController>().readyToMove = true;
	                enemy.GetComponent<SpiderController>().doneMoving = false;
	                break;
	            default:
	                Debug.Log("Enemy script not registered...");
	                break;
	        }
	    }
	}

	void checkOnEnemies() {

		bool enemiesDone = true;
		foreach(GameObject enemy in enemies) {

	        switch (enemy.tag)
	        {
	            case "Spider":
	            	if(!enemy.GetComponent<SpiderController>().doneMoving) {
	            		enemiesDone = false;
	            	}
	                break;
	            default:
	                Debug.Log("Enemy script not registered...");
	                break;
	        }
	    }
	    if(enemiesDone)
	    	enemiesMoved = true;
	    
	}
}
