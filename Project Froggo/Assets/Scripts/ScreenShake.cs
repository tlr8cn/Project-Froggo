using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour {


    Vector3 originalCameraPosition;

    public float shakeAmt = 0;

    public Camera mainCamera;

    public float shakeDuration;
    public float repeatTime;

    void Start() {
    	mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    void OnTriggerEnter2D(Collider2D coll) {
    	if(coll.tag == "Frog") {
    		coll.GetComponent<FrogController>().wallBumped = true;
	        InvokeRepeating("CameraShake", 0, repeatTime);
	        Invoke("StopShaking", shakeDuration);
	    }
    }

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
