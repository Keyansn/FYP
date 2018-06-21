using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ECSCRIPT : MonoBehaviour {
    public GameObject[] nodelist, linklist;
    public IDictionary<GameObject, List<GameObject>> NodeDict = new Dictionary<GameObject, List<GameObject>>();
    public IDictionary<GameObject, float> ecDict = new Dictionary<GameObject, float>();
    public IDictionary<GameObject, float> OldecDict = new Dictionary<GameObject, float>();
    public int count;
    // Use this for initialization
    void Start () {
        count = 0;
        nodelist = GameObject.FindGameObjectsWithTag("Node");
        linklist = GameObject.FindGameObjectsWithTag("link");

        foreach (GameObject node in nodelist)
        {
            OldecDict.Add(node, 1);
            ecDict.Add(node, 0);
        }

        foreach (GameObject node in nodelist)
        {
            NodeDict.Add(node, new List<GameObject>());
        }


        foreach (GameObject link in linklist)
        {
            List<GameObject> temptarget = new List<GameObject>();
            List<GameObject> tempsource = new List<GameObject>();

            temptarget = NodeDict[link.GetComponent<Link>().target];
            temptarget.Add(link.GetComponent<Link>().source);

            tempsource = NodeDict[link.GetComponent<Link>().source];
            tempsource.Add(link.GetComponent<Link>().target);

            NodeDict.Remove(link.GetComponent<Link>().target);
            NodeDict.Remove(link.GetComponent<Link>().source);


            NodeDict.Add(link.GetComponent<Link>().source, tempsource);
            NodeDict.Add(link.GetComponent<Link>().target, temptarget);

            //Don't clear as it will clear within the Dict stupid...
            //temptarget.Clear();
            //tempsource.Clear();

        }



    }
    float i;
    float sumaller=0;
    // Update is called once per frame
    void Update () {
        if (count < 20)
        {
            sumaller = 0;

            float Lval = 1;
            foreach (GameObject node in nodelist)
            {
                ecDict[node] = RecomputeScores(node);
            }


            foreach (GameObject node in nodelist)
            {
                OldecDict[node] = ecDict[node];
            }

            foreach (GameObject node in nodelist)
            {
                print("OldecDict[node]" + OldecDict[node]);
            }

            Lval = largestVal();
            print(Lval + "largest");
            i= 0;
            foreach (GameObject node in nodelist)
            {
                OldecDict[node] = OldecDict[node]/Lval;
                print(node.name + ":: " + OldecDict[node]);
                i = i + OldecDict[node];// sumall());
                sumaller = sumaller + OldecDict[node] / sumall();
            }

            foreach (GameObject node in nodelist)
            {
                print(node.name + ":;: " + OldecDict[node]/i);
               
            }
            count = count + 1;
            print(sumaller + "sumall");

        }
    }

    float RecomputeScores(GameObject _node)
    {
        float EC = 0;
        foreach (GameObject item in NodeDict[_node].ToList())
        {
            EC = EC + OldecDict[item];
        }

        return EC;
    }

    float largestVal()
    {
        float Val = 0;
        //print ("outer" + Val);

        foreach (GameObject node in nodelist)
        {
            if ((ecDict[node]) > Val)
            {
                Val = (ecDict[node]);
            }
            //Val = Val + (ecDict[node]);
            //print("val: " + Val);
            
        }

        return Val;
    }

    float sumall()
    {
        float Val = 0;
        //print("outer" + Val);

        foreach (GameObject node in nodelist)
        {
            
            Val = Val + (OldecDict[node]);
            //print("val: " + Val);

        }

        return Val;
    }
}


/*
1.	Start by  assigning centrality  score of  1	to all
nodes(v_i    = 1	for	all i	in	the network)

• 2.	Recompute scores  of each    node	as	weighted
sum of centraliHes of all nodes	in	a node's	
neighborhood:	v_i	=	sum_{j	\in	N}	a_{ij}* v_j

• 3.	Normalize v   by dividing    each value   by the
largest value

• 4.	Repeat steps   2	and	3	unHl values  of v   stop
changing.*/