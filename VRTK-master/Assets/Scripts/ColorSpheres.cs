using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System;


public class ColorSpheres : MonoBehaviour {

    public Material[] materials;//Allows input of material colors in a set size of array;
    public Material hidden;
    private Material materialsS;//Allows input of material colors in a set size of array;
    private Material materialsE;//Allows input of material colors in a set size of array;
    private Renderer Rend; //What are we rendering? Input object(Sphere,Cylinder,...) to render.
    GameObject[] nodelist;
    private OVRCameraRig camera;
    private Color colour;
    public bool hide = false;

    int node_length;
	//public Text txthighlighted;
	//GameObject[] highlightedlist;
	//int highlighted_length;

    private int index = 1;//Initialize at 1, otherwise you have to press the ball twice to change colors at first.

    // Use this for initialization
    void Start()
    {
		AudioSource aud = GetComponent<AudioSource>();
        Rend = GetComponent<Renderer>();//Gives functionality for the renderer
        Rend.enabled = true;//Makes the rendered 3d object visable if enabled;
        hide = false;
    }

    void OnMouseDown()
    {
		AudioSource aud = GetComponent<AudioSource>();
        if (materials.Length == 0)//If there are no materials nothing happens.
            return;

        //Debugging mouse presses
        //works
        if (Input.GetMouseButton(0))
            Debug.Log("Pressed left click.");

        //doesn't work, I know why now
		/*
		 * the OnMouseDown() event handler that you can use when hovering over GUIElements and colliders only gets triggered by the left mouse button.
		 * 
        if (Input.GetMouseButton(1))
            Debug.Log("Pressed right click.");

        if (Input.GetMouseButton(2))
            Debug.Log("Pressed middle click.");
            */



        if (Input.GetMouseButtonDown(0))
        {
            Pressed();
			

        }
    }

    public void Pressed()
    {
        AudioSource aud = GetComponent<AudioSource>();
        aud.Play();

        index += 1;//When mouse is pressed down we increment up to the next index location

        if (index == materials.Length + 1)//When it reaches the end of the materials it starts over.
            index = 1;

        print(index);//used for debugging

        //This sets the material color values inside the index

        if (index == 2)
        {
            GameObject[] nodelist;
            nodelist = GameObject.FindGameObjectsWithTag("Node");

            foreach (GameObject node in nodelist)
            {
                node.transform.GetChild(0).tag = "Not_Highlighted";

                if (hide == false)
                {
                    node.GetComponent<Renderer>().sharedMaterial = materials[0];
                }

            }
            transform.GetChild(0).tag = "Highlighted";

        }
        else
        {

            transform.GetChild(0).tag = "Not_Highlighted";
        }

        Rend.sharedMaterial = materials[index - 1];
    }




    private string id;
        public TextMesh nodeText;
        float i=0;
        bool sound = false;


        void Update()
        {

        /*
        //node text always facing camera, why didn't I put this in ages ago...
        if (GameObject.FindGameObjectWithTag("MainCamera")) 
        {
            nodeText.transform.LookAt(GameObject.FindGameObjectWithTag("MainCamera").transform);
            nodeText.transform.Rotate(0, 180F, 0);
        }
        */
        
        /*
        if (sound == true)
        {
            AudioSource aud = GetComponent<AudioSource>();
            aud.pitch = aud.pitch + 0.001f;

            if (aud.pitch > 1)
            {
                aud.pitch = 0.5f;
            }
        }
			*/

        /*
        Rend.material.SetColor("_TintColor", new Color(0, i, i, 1));
        i = i + 0.01f;
        if (i>1)
        {
            i = 0;
        }*/

    }

	void OnMouseOver(){

        //Rend.sharedMaterial = hidden;
        //hide = true;



		if (!Input.GetKey(KeyCode.LeftShift)&&(Input.GetMouseButtonDown(1)))
		{
			print("Right clicked, going to set the start node");
			RemoveOthers("Start");
			transform.GetChild(0).tag = "Start";
			Rend.sharedMaterial = materialsS;

		}
			

		if (Input.GetKey(KeyCode.LeftShift)&&(Input.GetMouseButtonDown(1)))
		{
			print("Held left shift and right clicked, going to set the end node");
			RemoveOthers("End");
			transform.GetChild(0).tag = "End";
			Rend.sharedMaterial = materialsE;
		}
			
	}

	void RemoveOthers(String tag){
		
		nodelist = GameObject.FindGameObjectsWithTag("Node");
		node_length = nodelist.Length;

		for (int i = 0; i < node_length; i++)
		{
			if (nodelist[i].transform.GetChild(0).tag == tag){
				nodelist[i].transform.GetChild(0).tag = "Not_Highlighted";
				nodelist[i].GetComponent<Renderer>().sharedMaterial = materials[0];
			}

		}

		//GameObject.FindGameObjectWithTag(tag).tag;

	}


	/*
	public void CountHighlightedNodes(){
		highlightedlist = GameObject.FindGameObjectsWithTag("Highlighted");
		highlighted_length = highlightedlist.Length;
		txthighlighted.text = "Number of highlighted: " + highlighted_length.ToString();
	}*/




    }

