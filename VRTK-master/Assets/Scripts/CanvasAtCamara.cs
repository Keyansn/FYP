using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasAtCamara : MonoBehaviour {
    Transform location;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (GameObject.FindGameObjectWithTag("MainCamera"))
        {
            location = GameObject.FindGameObjectWithTag("MainCamera").transform;
            gameObject.transform.LookAt(location);
            gameObject.transform.Rotate(0, 180F, 0);
        }
    }
}
