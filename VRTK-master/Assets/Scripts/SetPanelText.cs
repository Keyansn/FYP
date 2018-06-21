using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetPanelText : MonoBehaviour {
    public string name;
    string output = "";

    // Use this for initialization
    void Start () {
        UpdateText(output);
        gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void UpdateText(string outputStr)
    {
        name = transform.parent.parent.parent.parent.name;
        GetComponent<Text>().text = "Name: " + name + outputStr;
    }

    public void GetInfo()
    {
        string textout = "\n\n Degree: ";
        textout += transform.parent.parent.parent.parent.gameObject.GetComponent<EigenvectorCentrality>().degree.ToString();
        textout += "\n Eigenvector Centrality: ";
        textout += transform.parent.parent.parent.parent.gameObject.GetComponent<EigenvectorCentrality>().ec.ToString();
        textout += "\n Data: ";
        textout += transform.parent.parent.parent.parent.gameObject.GetComponent<EigenvectorCentrality>().data;

        UpdateText(textout);
        //GameObject.Find("GameController").GetComponent<GraphController>().LinkLength;
    }

}
