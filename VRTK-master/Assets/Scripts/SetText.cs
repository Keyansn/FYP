using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SetText : MonoBehaviour {

    public string name;
    public TextMesh nodeText;

    // Use this for initialization
    void Start () {
		//name = GetComponentsInParent<name> ();

		name = transform.parent.name;
        GetComponent<TextMesh>().text = name;


    }

    void Update()
    {
        /*
        if (GameObject.FindGameObjectWithTag("MainCamera"))
        {
            nodeText.transform.LookAt(GameObject.FindGameObjectWithTag("MainCamera").transform);
            nodeText.transform.Rotate(0, 180F, 0);
        }
        */
    }
	

}
