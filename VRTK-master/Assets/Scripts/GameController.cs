using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject[] nodelist, linklist;
    public IDictionary<GameObject, List<GameObject>> NodeDict = new Dictionary<GameObject, List<GameObject>>();
    public List<GameObject> Vaccinated = new List<GameObject>();
    public List<GameObject> AntiVaxxers = new List<GameObject>();
    public List<GameObject> Sick = new List<GameObject>();
    public List<GameObject> Normal = new List<GameObject>();

    public Material VaccinatedMaterial;
    public Material AntiVaxxersMaterial;
    public Material SickMaterial;
    public Material NormalMaterial;

    public int StartingInfected=1;
    public int StartingAntiVaxxer = 2;

    public float infectious = 0.3f;

    // Use this for initialization
    void Start () {
        nodelist = GameObject.FindGameObjectsWithTag("Node");
        linklist = GameObject.FindGameObjectsWithTag("link");

        List<GameObject> AvailableLinks = new List<GameObject>();
        AvailableLinks.AddRange(linklist);

        List<GameObject> Nodes = new List<GameObject>();
        Nodes.AddRange(nodelist);

        //SOMEHOW DECLARING IT HERE MAKES IT VANISH AFTER VOID START??
        //IDictionary<GameObject, List<GameObject>> NodeDict = new Dictionary<GameObject, List<GameObject>>();

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


          //Below is just to test if working
           
        foreach (GameObject element in nodelist)
        {
            print("Node: " + element.name);


            foreach(GameObject available in NodeDict[element])
            {
                print("Available Node: " + available.name);
            }
        }
        


        Normal.AddRange(nodelist);


        AntiVaxxer();
        AutoVaccinate();
        InitialInfect();

        UpdateMaterials();
    }

    // Update is called once per frame
    void Update () {
        //print("testing GameController script");
        if (Input.GetKeyDown(KeyCode.Z))
        {
            DiseaseSpread();

            //print("return");

            foreach (GameObject element in nodelist.ToList())
            {
                //print("Node: " + element.name);

                
                foreach (GameObject available in NodeDict[element].ToList())
                {
                    if (NodeDict.ContainsKey(available))
                    {
                        //print("Available Node: " + available.name);
                    }

                }
            }
            UpdateMaterials();
        }

    }

    void NewTurn()
    {
        UpdateMaterials();

        //Check if game over
        if (CheckIfComplete())
        {
            //End game
        }
        else
        {
            //Start new turn

            //Fix all materials
           

        }

    }

    void UpdateMaterials()
    {
        //Fix all materials
        foreach (GameObject node in Vaccinated.ToList())
        {
            node.GetComponent<Renderer>().material = VaccinatedMaterial;
        }

        foreach (GameObject node in AntiVaxxers.ToList())
        {
            node.GetComponent<Renderer>().material = AntiVaxxersMaterial;
        }

        foreach (GameObject node in Sick.ToList())
        {
            node.GetComponent<Renderer>().material = SickMaterial;
        }

        foreach (GameObject node in Normal.ToList())
        {
            node.GetComponent<Renderer>().material = NormalMaterial;
        }
    }

    bool CheckIfComplete()
    {
        return true;
    }

    void AvailableLinks()
    {

    }

    public int count;
    void DiseaseSpread()
    {
         count = 0;
        List<GameObject> validnodes = new List<GameObject>();
        foreach (GameObject node in Sick.ToList())
        {
            foreach (GameObject item in NodeDict[node].ToList())
            {
                
                if (Normal.Contains(item) || AntiVaxxers.Contains(item))
                {
                    //print("Item: " + item);
                    ChanceSickness(item);
                    count = count + 1;
                }
            }
            //print("Disease Spread");

            //NodeDict.Remove(node);
            //NodeDict[node].Add(null);
        }


        if (count < 10)
        {
            count = 0;
            foreach (GameObject node in Sick.ToList())
            {


                foreach (GameObject item in NodeDict[node].ToList())
                {

                    if (Normal.Contains(item) || AntiVaxxers.Contains(item))
                    {
                        
                        
                        count = count + 1;
                    }
                }
            }
        }
        if (count == 0)
        {
            print("END GAME!!!");
        }

    }

    void InitialInfect()
    {
        for (int i = 0; i < StartingInfected ; i++)
        {
            int rng = Convert.ToInt32(UnityEngine.Random.Range(0, Normal.Count-1));
            Sick.Add(Normal[rng]);
            Normal.Remove(Normal[rng]);
            //print(Normal[rng].name);
        }
    }

    void ChanceSickness(GameObject poornode)
    {
        if(UnityEngine.Random.Range(0, 100)< infectious)
        {
            Sick.Add(poornode);
            Normal.Remove(poornode);
            AntiVaxxers.Remove(poornode);
        }
    }

    void Vaccinate(GameObject VaxNode)
    {

            Vaccinated.Add(VaxNode);
            Normal.Remove(VaxNode);
            //print(Normal[rng].name);

            //RemoveFromDict(VaxNode);

        
    }

    void AutoVaccinate()
    {
        for (int i = 0; i < 6; i++)
        {
            int rng = Convert.ToInt32(UnityEngine.Random.Range(0, Normal.Count - 1));
            Vaccinated.Add(Normal[rng]);
            Normal.Remove(Normal[rng]);
            //print(Normal[rng].name);

            RemoveFromDict(Normal[rng]);

        }
    }

    void RemoveFromDict(GameObject NodeToRemove)
    {
        foreach (GameObject node in nodelist.ToList())
        {
            /*

            if (NodeDict[node].Contains(NodeToRemove))
            {
                NodeDict[node].Remove(NodeToRemove);
            }
            */
        }
    }

    void AntiVaxxer()
    {
        

        for (int i = 0; i < StartingAntiVaxxer; i++)
        {
            int rng = Convert.ToInt32(UnityEngine.Random.Range(0, Normal.Count-1));
            AntiVaxxers.Add(Normal[rng]);
            Normal.Remove(Normal[rng]);
            //print(Normal[rng].name);
        }
    }
}
