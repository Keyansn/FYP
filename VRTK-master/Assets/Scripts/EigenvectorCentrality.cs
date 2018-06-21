
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEditor;
    using System.Collections;

    public class EigenvectorCentrality : MonoBehaviour
    {

        public int degree;
        public int ec;
        public string data;
        //GameObject[] connectednodelist;
        /*
        public int ec
        {
            get { return EC; }
            set { EC = value; }
        }


            THIS WILL NEED TO BE CALLED BY THE BUTTON PRESSES RATHER THAN BEING ATTACHED TO THE PREFAB AND RUNNING ON UPDATE

            Also needs to be normalised.
    */



        // Use this for initialization
        void Start()
        {
            degree = 0;
            ec = 0;
            CountCentrality();
            //StartCoroutine(Begin());
        }

        // Update is called once per frame
        void Update()
        {
        //CountCentrality();
        int scaler = int.Parse(GameObject.FindGameObjectWithTag("Slider").GetComponent<TextMesh>().text);
        scaler = scaler / 100;
        //gameObject.transform.localScale = new Vector3(scaler + 0.04f * degree, scaler + 0.04f * degree, scaler + 0.04f * degree);



    }

    void CountCentrality()
        {
            GameObject[] linklist;
        GameObject[] nodelist;
        int link_length;
        int node_length;

            linklist = GameObject.FindGameObjectsWithTag("link");
        nodelist = GameObject.FindGameObjectsWithTag("Node");

        //Find how many nodes & links there are
        link_length = linklist.Length;
        node_length = nodelist.Length;

            ec = 0;
            degree = 0;
            for (int i = 0; i < link_length; i++)
            {
                if ((linklist[i].GetComponent<Link>().source == this.gameObject) || linklist[i].GetComponent<Link>().target == this.gameObject)
                {
                    degree = degree + 1;

                }

                if ((linklist[i].GetComponent<Link>().source == this.gameObject) || linklist[i].GetComponent<Link>().target == this.gameObject)
                {
                    if ((linklist[i].GetComponent<Link>().source != this.gameObject))
                    {

                        EigenvectorCentrality eScript = linklist[i].GetComponent<Link>().source.GetComponent<EigenvectorCentrality>();
                        //print(eScript.degree);
                        ec = ec + eScript.degree;


                    }

                    if ((linklist[i].GetComponent<Link>().target != this.gameObject))
                    {

                        EigenvectorCentrality eScript = linklist[i].GetComponent<Link>().target.GetComponent<EigenvectorCentrality>();
                        //print(eScript.degree);
                        ec = ec + eScript.degree;



                    }
                }


            }

        degree = degree / (node_length-1);

            /*
            int scaler = int.Parse(GameObject.FindGameObjectWithTag("Slider").GetComponent<TextMesh>().text);
            scaler = scaler / 100;
            gameObject.transform.localScale = 
            new Vector3(scaler + 0.04f * degree, scaler + 0.04f * degree, scaler + 0.04f * degree);
            */
        }


        IEnumerator Begin()
        {
            yield return new WaitForSeconds(1);
            CountCentrality();
        }
    }


