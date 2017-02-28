using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyScript : MonoBehaviour {

	public List<GameObject> enemies;

	public bool frogMoved;

	public bool enemiesMoved;

	public GameObject playerFrog;
	FrogController frogScript;

	// Use this for initialization
	void Start () {
		//enemies = new List<GameObject>();

		frogMoved = false;

		enemiesMoved = true;

		frogScript = playerFrog.GetComponent<FrogController>();
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


	public void sendAttackPotential(GameObject enemy) {

		//checks if enemy is directly adjacent to frog
		if(withinError(enemy.transform.position.x, playerFrog.transform.position.x) ||
		   withinError(enemy.transform.position.y, playerFrog.transform.position.y)) {

			//figure out where the enemy is
			if(withinError(enemy.transform.position.y, playerFrog.transform.position.y) && enemy.transform.position.x > playerFrog.transform.position.x) { // right
				//Debug.Log("1");
				List<GameObject> temp = frogScript.keyToEnemies[KeyCode.D.ToString()];
				if(!temp.Contains(enemy))
					temp.Add(enemy);

				frogScript.keyToEnemies.Remove(KeyCode.D.ToString());
				frogScript.keyToEnemies.Add(KeyCode.D.ToString(), temp);	

			} else if (withinError(enemy.transform.position.y, playerFrog.transform.position.y) && enemy.transform.position.x < playerFrog.transform.position.x) { // left

				List<GameObject> temp = frogScript.keyToEnemies[KeyCode.A.ToString()];
				if(!temp.Contains(enemy))
					temp.Add(enemy);

				frogScript.keyToEnemies.Remove(KeyCode.A.ToString());
				frogScript.keyToEnemies.Add(KeyCode.A.ToString(), temp);	

			} else if (withinError(enemy.transform.position.x, playerFrog.transform.position.x) && enemy.transform.position.y < playerFrog.transform.position.y) { // below
				//Debug.Log("3");
				List<GameObject> temp = frogScript.keyToEnemies[KeyCode.S.ToString()];
				if(!temp.Contains(enemy))
					temp.Add(enemy);

				frogScript.keyToEnemies.Remove(KeyCode.S.ToString());
				frogScript.keyToEnemies.Add(KeyCode.S.ToString(), temp);	

			} else if (withinError(enemy.transform.position.x, playerFrog.transform.position.x) && enemy.transform.position.y > playerFrog.transform.position.y) { // above
				//Debug.Log("4");
				List<GameObject> temp = frogScript.keyToEnemies[KeyCode.W.ToString()];
				if(!temp.Contains(enemy))
					temp.Add(enemy);

				frogScript.keyToEnemies.Remove(KeyCode.W.ToString());
				frogScript.keyToEnemies.Add(KeyCode.W.ToString(), temp);

			}
		}
	}

	public void removeAttackPotential(GameObject enemy) {
		foreach(KeyValuePair<string, List<GameObject> > entry in frogScript.keyToEnemies) {
			List<GameObject> temp = entry.Value;
			if(temp.Contains(enemy))
				temp.Remove(enemy);
		}
	}

	public void sendDamage(GameObject enemy) {
	        switch (enemy.tag)
	        {
	            case "Spider":
	            	enemy.GetComponent<SpiderController>().dealDamage();
	                break;
	            default:
	                Debug.Log("Enemy script not registered...");
	                break;
	        }
	}

	public bool enemyOutsideAttackRange(GameObject enemy) {
		if(!withinError(enemy.transform.position.x, playerFrog.transform.position.x) &&
		   !withinError(enemy.transform.position.y, playerFrog.transform.position.y)) {
			return true;
		 }
		 return false;
	}


	bool withinError(float x1, float x2) {
    	if(Math.Abs(x1 - x2) < 0.0025f) {
    		return true;
    	}
    	return false;
    }

    public void killEnemy(GameObject enemy) {
    	removeAttackPotential(enemy);
    	enemies.Remove(enemy);
    	Destroy(enemy);
    }
}
