using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleNames : MonoBehaviour
{

    private bool toggledOn = true;
    bool changed = false;
    GameObject[] nodelist,nodeText;
    Transform location;

    // Use this for initialization
    void Start()
    {
        nodeText = GameObject.FindGameObjectsWithTag("Name");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("MainCamera"))
        {
            location = GameObject.FindGameObjectWithTag("MainCamera").transform;
            foreach (GameObject text in nodeText)
            {
                text.transform.LookAt(location);
                text.transform.Rotate(0, 180F, 0);
            }
            
        }

    }

    public void forceUpdate()
    {
        nodeText = GameObject.FindGameObjectsWithTag("Name");
    }


    public void toggle()
    {
        if (toggledOn)
        {
            nodelist = GameObject.FindGameObjectsWithTag("Node");
            foreach (GameObject node in nodelist)
            {
                node.transform.GetChild(0).gameObject.SetActive(false);
            }

            toggledOn = false;
        }
        else
        {
            nodelist = GameObject.FindGameObjectsWithTag("Node");
            foreach (GameObject node in nodelist)
            {
                node.transform.GetChild(0).gameObject.SetActive(true);
            }

            toggledOn = true;
        }
    }

}