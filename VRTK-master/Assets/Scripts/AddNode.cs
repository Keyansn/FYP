using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AddNode : MonoBehaviour {


    GameObject[] nodelist;
    GameObject[] HighlightedNodes;
    int node_length;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpawnNode()
    {

        Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Node1.prefab", typeof(GameObject));
        GameObject clone = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;

        HighlightedNodes = GameObject.FindGameObjectsWithTag("Selected");

        float xcord = 0, ycord = 0, zcord =0;

        foreach (GameObject Node in HighlightedNodes)
        {
            xcord += Node.transform.parent.position.x;
            ycord += Node.transform.parent.position.y;
            zcord += Node.transform.parent.position.z;
        }



        //Move the new cloned prefab to random location 
        clone.transform.position = new Vector3(xcord/HighlightedNodes.Length, ycord / HighlightedNodes.Length, zcord / HighlightedNodes.Length);

        clone.name = NameGen();


        SpawnLinks(clone, HighlightedNodes);

        GraphController Script = GameObject.FindGameObjectWithTag("GameController").GetComponent<GraphController>();
        Script.UpdateGraph();

    }

    void SpawnLinks(GameObject SpawnedNode, GameObject[] Highlighted)
    {
        foreach (GameObject Node in Highlighted)
        {
            

            //Create link from the prefab
            Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Link.prefab", typeof(GameObject));
            GameObject clone = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;

            //Sets the source and target nodes for the connection
            clone.GetComponent<Link>().source = SpawnedNode;
            clone.GetComponent<Link>().target = Node.transform.parent.gameObject;
            clone.GetComponent<Link>().weight = 1;


            clone.GetComponent<Link>().direction = false;

        }
    }

    public void connect() {
        HighlightedNodes = GameObject.FindGameObjectsWithTag("Selected");
        List<GameObject> iList = new List<GameObject>();

        foreach (GameObject item in HighlightedNodes)
        {
            iList.Add(item);
        }


        justLinks(iList);

        GraphController Script = GameObject.FindGameObjectWithTag("GameController").GetComponent<GraphController>();
        Script.UpdateGraph();
    }

    void justLinks(List<GameObject> Highlighted)
    {
        for (int i = 1; i < Highlighted.Count; i++)
        {
            for (int j = 0; i > j; j++)
            {
                Join(Highlighted[i], Highlighted[j]);
            }
        }
    }

    void Join(GameObject a, GameObject b)
    {
        //Create link from the prefab
        Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Link.prefab", typeof(GameObject));
        GameObject clone = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;

        //Sets the source and target nodes for the connection
        clone.GetComponent<Link>().source = a.transform.parent.gameObject;
        clone.GetComponent<Link>().target = b.transform.parent.gameObject;
        clone.GetComponent<Link>().weight = 1;


        clone.GetComponent<Link>().direction = false;
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
