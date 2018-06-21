using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphController : MonoBehaviour {

    public float sphRadius = 5;
    public float LinkLength = 2;
    public float forceStrength = 1;
    public float repulse=4;
    public int MaxDegree;
    public int Mode=5;
    /*What different selection nodes are needed:
 * 
 *  1) Select Node
 *  2) Highlight Node
 *  3) Start Node
 *  4) End Node
 *  5) Delete Node
 * 
 */


    GameObject[] nodelist;
    GameObject[] linklist;

    // Use this for initialization
    void Awake () {

        sphRadius = 25;
        LinkLength = 6;
        forceStrength = 5;
        repulse = 4;
    }
	
	// Update is called once per frame
	void Update () {
        //GameObject.Find("GlobalController").GetComponent<GraphController>.sphRadius;

        //NEED TO FIGURE OUT WHEN TO CALL THIS... I.E. UPDATE FUNCTION
        //MaxDegree = FindDegree();
    }

    int FindDegree()
    {
        nodelist = GameObject.FindGameObjectsWithTag("Node");
        int max=0;
        foreach (GameObject node in nodelist)
        {
            if (node.GetComponent<EigenvectorCentrality>().degree > max)
            {
                max = node.GetComponent<EigenvectorCentrality>().degree;

            }
        }

        return max;
    }

    public void UpdateGraph()
    {
        ToggleNames Script = GameObject.FindGameObjectWithTag("GameController").GetComponent<ToggleNames>();
        Script.forceUpdate();

        linklist = GameObject.FindGameObjectsWithTag("link");
        foreach (GameObject link in linklist)
        {
            link.GetComponent<Link>().CheckHidden();
        }
    }

}
