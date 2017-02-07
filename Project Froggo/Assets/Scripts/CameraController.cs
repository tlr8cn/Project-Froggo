using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {


	public Transform Player;

	public Vector2 Margin, Smoothing;

	public BoxCollider2D Bounds;

	private Vector3 _min, _max;

	public bool isFollowing{ get; set; }




	// Use this for initialization
	void Start () {

		//Player = (GameObject.Find("Character")).transform;

		_min = Bounds.bounds.min;
		_max = Bounds.bounds.max;
		isFollowing = true;
		Destroy (Bounds.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
		var x = transform.position.x;
		var y = transform.position.y;

		if (isFollowing) {
			if(Mathf.Abs (x - Player.position.x) > Margin.x){ // if past margin
				x = Mathf.Lerp (x, Player.position.x, Smoothing.x * Time.deltaTime);
			}

			if(Mathf.Abs(y - Player.position.y) > Margin.y){
				y = Mathf.Lerp (y, Player.position.y, Smoothing.y * Time.deltaTime);
			}
		}

		var cameraHalfWidth = GetComponent<Camera>().orthographicSize * ((float) Screen.width / Screen.height);

		x = Mathf.Clamp (x, _min.x + cameraHalfWidth, _max.x - cameraHalfWidth); // constrain x axis
		y = Mathf.Clamp (y, _min.y + GetComponent<Camera>().orthographicSize, 50*_max.y - GetComponent<Camera>().orthographicSize); //constrain y axis

		transform.position = new Vector3 (x, y, transform.position.z);

	}
}
