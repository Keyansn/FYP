using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Linq;
using NUnit.Framework;
using UnityEngine.UI;
using UnityEditor;
using System;

public class FDG : MonoBehaviour {
	public bool Enabled;
	public Button yourButton;
	private bool debug = true; // Template ->   if (debug) {print("n: " + n);}
	public GameObject[] nodelist,linklist;
	GameObject centre;
	public float stiffness = 1;
	public float naturalLength = 1;
	int max;
	int count;
	float x,force;
	public float gravityPull = 2;
    public float gravityPlane = 5;//20
    public float CoulombsConstant = 10; //100
	private float sphRadius;
	private float sphRadiusSqr;

	// Use this for initialization
	void Start () {
		UpdateLists();
		//Button btn = yourButton.GetComponent<Button>();
		//btn.onClick.AddListener(UpdateLists);
	}

	public void UpdateLists(){
        Enabled = false;
        Array.Clear(nodelist,0,nodelist.Length);

        nodelist = GameObject.FindGameObjectsWithTag("Node");
		linklist = GameObject.FindGameObjectsWithTag("link");
		max = nodelist.Length;
        if (nodelist == null)
        {
            Enabled = false;

        }
        else
        {
            Enabled = true;

        }

	}

    public void Disable()
    {
        Enabled = false;
    }
	
	// Update is called once per frame
	void Update () {

        if (Enabled == true) {

            foreach (GameObject node in nodelist)
            {
                if (node == null)
                {
                    return;
                }
                Gravity(node);
            }
                //foreach (GameObject list in linklist) {
					//Attract(list.GetComponent<Link>().source, list.GetComponent<Link>().target);
				//}					
			
		}
	}


    void Gravity(GameObject A) {
        Vector3 dirToCentre = -A.transform.position;
        Vector3 impulse = (dirToCentre.normalized + new Vector3(0, 0.5f, 0)) * gravityPull;
        A.GetComponent<Rigidbody>().AddForce(impulse);

        Vector3 GravPlane = new Vector3(0, -A.transform.position.y,0).normalized*5f;
        A.GetComponent<Rigidbody>().AddForce(GravPlane);



	}


	void Attract(GameObject A, GameObject B){
		// f = k * x
		// f = force, k = stiffness, x = extension	x = distance - set length
		x = Vector3.Distance(A.transform.position, B.transform.position) - naturalLength;
		force = stiffness * x ;

		Vector3 direction = A.transform.position - B.transform.position;
		float distsqr = direction.sqrMagnitude;

		A.GetComponent<Rigidbody>().AddForce(direction.normalized*-force);
		B.GetComponent<Rigidbody>().AddForce(direction.normalized*force);
		//print("Attractive Force:" + force);
	}

	void Repel(GameObject A, GameObject B){
		/*
		 * 	    k * q1 * q2
		 * F = ------------
		 * 		    d^2
		 * 
		 *  f = force, k = Coulomb's constant, q1 & q2 respective charges, d = distance between nodes
		*/
		x = Vector3.Distance(A.transform.position, B.transform.position);
		x = x * x;
		force = (-CoulombsConstant)/x;
		Vector3 direction = A.transform.position - B.transform.position;
		A.GetComponent<Rigidbody>().AddForce(direction.normalized*-force);
		B.GetComponent<Rigidbody>().AddForce(direction.normalized*force);
		print("Repulsive Force:" + force);

	}

}
