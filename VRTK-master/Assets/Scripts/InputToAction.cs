using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Xml;
using UnityEngine.PostProcessing;

public class InputToAction : MonoBehaviour {
    bool debug = true;
    public bool frozen = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TestPrint()
    {
        print(NameGen());                   
    }

    public void NewRandomNode()
    {

        Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Node1.prefab", typeof(GameObject));
        GameObject clone = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;

        clone.transform.position = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f));
        clone.name = NameGen();

        if (debug){ print("New Node"); }
    }

    public void FreezeAll()
    {
        GameObject[] nodelist;
        nodelist = GameObject.FindGameObjectsWithTag("Node");

        foreach (GameObject node in nodelist)
        {
            Rigidbody rb = node.GetComponent<Rigidbody>();
            rb.isKinematic = true;
        }
        frozen = true;
    }

    public void UnFreezeAll()
    {
        GameObject[] nodelist;
        nodelist = GameObject.FindGameObjectsWithTag("Node");

        foreach (GameObject node in nodelist)
        {
            Rigidbody rb = node.GetComponent<Rigidbody>();
            rb.isKinematic = false;
        }
        frozen = false;
    }

    public void FreezeRotationAll()
    {
        GameObject[] nodelist;
        nodelist = GameObject.FindGameObjectsWithTag("Node");

        foreach (GameObject node in nodelist)
        {
            Rigidbody rb = node.GetComponent<Rigidbody>();
            rb.freezeRotation = true;
        }
    }

    public void DeleteGraph()
    {
        FDG Script = GameObject.FindGameObjectWithTag("GameController").GetComponent<FDG>();
        Script.Disable();
        GameObject[] nodelist;
        nodelist = GameObject.FindGameObjectsWithTag("Node");
        for (int i =0; i<nodelist.Length; i++)
        {
            Destroy(nodelist[i]);
            nodelist[i] = null;
        }

        GameObject[] linklist;
        linklist = GameObject.FindGameObjectsWithTag("link");
        foreach (GameObject link in linklist)
        {
            Destroy(link);  
        }

        Script.UpdateLists();

    }

    public void LoadGraph(TextAsset File)
    {
        InputFile Script = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputFile>();
        string data = File.text;
        Script.LoadInputFile(data);
    }

    public void ToggleNames()
    {
        ToggleNames Script = GameObject.FindGameObjectWithTag("GameController").GetComponent<ToggleNames>();
        Script.toggle();
    }

    public void SpawnNode()
    {
        AddNode Script = GameObject.FindGameObjectWithTag("GameController").GetComponent<AddNode>();
        Script.SpawnNode();
    }

    public void SpawnLink()
    {
        AddNode Script = GameObject.FindGameObjectWithTag("GameController").GetComponent<AddNode>();
        Script.connect();
    }

    public void UpdateGraph()
    {
        GraphController Script = GameObject.FindGameObjectWithTag("GameController").GetComponent<GraphController>();
        Script.UpdateGraph();
    }

    public PostProcessingProfile vig;
    public void BlurScreen()
    {

        //GameObject.FindWithTag("MainCamera").GetComponent<PostProcessingBehaviour>().profile = vig;
        if (GameObject.Find("VRSimulatorCameraRig"))
        {
            GameObject.Find("VRSimulatorCameraRig").GetComponent<PostProcessingBehaviour>().profile=vig;
        }

        if (GameObject.Find("OVRCameraRig"))
        {
            GameObject.Find("OVRCameraRig").GetComponent<PostProcessingBehaviour>().profile = vig;
        }


    }

    public void Save()
    {
        XmlSave Script = GameObject.FindGameObjectWithTag("GameController").GetComponent<XmlSave>();
        Script.SaveItems();
    }
    //Returns random name from long array of names 
    private string NameGen()
    {
        string[] names = new string[]
        {
            "Keyan",
            "Allison",
            "Arthur",
            "Ana",
            "Alex",
            "Arlene",
            "Alberto",
            "Barry",
            "Bertha",
            "Bill",
            "Bonnie",
            "Bret",
            "Beryl",
            "Chantal",
            "Cristobal",
            "Claudette",
            "Charley",
            "Cindy",
            "Chris",
            "Dean",
            "Dolly",
            "Danny",
            "Danielle",
            "Dennis",
            "Debby",
            "Erin",
            "Edouard",
            "Erika",
            "Earl",
            "Emily",
            "Ernesto",
            "Felix",
            "Fay",
            "Fabian",
            "Frances",
            "Franklin",
            "Florence",
            "Gabielle",
            "Gustav",
            "Grace",
            "Gaston",
            "Gert",
            "Gordon",
            "Humberto",
            "Hanna",
            "Henri",
            "Hermine",
            "Harvey",
            "Helene",
            "Iris",
            "Isidore",
            "Isabel",
            "Ivan",
            "Irene",
            "Isaac",
            "Jerry",
            "Josephine",
            "Juan",
            "Jeanne",
            "Jose",
            "Joyce",
            "Karen",
            "Kyle",
            "Kate",
            "Karl",
            "Katrina",
            "Kirk",
            "Lorenzo",
            "Lili",
            "Larry",
            "Lisa",
            "Lee",
            "Leslie",
            "Michelle",
            "Marco",
            "Mindy",
            "Maria",
            "Michael",
            "Noel",
            "Nana",
            "Nicholas",
            "Nicole",
            "Nate",
            "Nadine",
            "Olga",
            "Omar",
            "Odette",
            "Otto",
            "Ophelia",
            "Oscar",
            "Pablo",
            "Paloma",
            "Peter",
            "Paula",
            "Philippe",
            "Patty",
            "Rebekah",
            "Rene",
            "Rose",
            "Richard",
            "Rita",
            "Rafael",
            "Sebastien",
            "Sally",
            "Sam",
            "Shary",
            "Stan",
            "Sandy",
            "Tanya",
            "Teddy",
            "Teresa",
            "Tomas",
            "Tammy",
            "Tony",
            "Van",
            "Vicky",
            "Victor",
            "Virginie",
            "Vince",
            "Valerie",
            "Wendy",
            "Wilfred",
            "Wanda",
            "Walter",
            "Wilma",
            "William",
            "Kumiko",
            "Aki",
            "Miharu",
            "Chiaki",
            "Michiyo",
            "Itoe",
            "Nanaho",
            "Reina",
            "Emi",
            "Yumi",
            "Ayumi",
            "Kaori",
            "Sayuri",
            "Rie",
            "Miyuki",
            "Hitomi",
            "Naoko",
            "Miwa",
            "Etsuko",
            "Akane",
            "Kazuko",
            "Miyako",
            "Youko",
            "Sachiko",
            "Mieko",
            "Toshie",
            "Junko"
        };

        return names[Random.Range(0, 152)];
    }
}
