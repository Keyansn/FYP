using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Deactivate();
    }

	
	// Update is called once per frame
	void Update () {
        Check();
    }

    void Activate()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(true);
        SetPanelText Script = transform.GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<SetPanelText>();
        Script.GetInfo();
    }

    void Deactivate()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    void Check()
    {
        if (gameObject.transform.parent.GetChild(0).tag == "Highlighted")
        {
            Activate();
        }
        else
        {
            Deactivate();
        }
    }

}
