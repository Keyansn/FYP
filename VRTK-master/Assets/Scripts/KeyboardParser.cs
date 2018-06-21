using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardParser : MonoBehaviour {

    bool debug = true;
    public TextAsset xmlRawFile;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.KeypadPlus)){
            if (debug)
            {
                InputToAction Script = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputToAction>();
                Script.TestPrint();
            }
        }

        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            if (debug)
            {
                InputToAction Script = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputToAction>();

                for (int i = 0; i < 100; i++)
                {
                    Script.NewRandomNode();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (debug)
            {
                InputToAction Script = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputToAction>();
                Script.FreezeAll();
            }
        }

        if (Input.GetKeyDown(KeyCode.KeypadPeriod))
        {
            if (debug)
            {
                InputToAction Script = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputToAction>();
                Script.UnFreezeAll();
            }
        }

        if (Input.GetKeyDown(KeyCode.Delete))
        {
            InputToAction Script = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputToAction>();
            Script.DeleteGraph();
            print("deleted");
        }

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            InputToAction Script = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputToAction>();
            Script.DeleteGraph();
        }

        if (Input.GetKeyUp(KeyCode.Keypad1))
        {
            InputToAction Script = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputToAction>();

            Script.LoadGraph(xmlRawFile);
        }

        if (Input.GetKeyUp(KeyCode.Keypad7))
        {
            InputToAction Script = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputToAction>();
            Script.ToggleNames();
        }

        if (Input.GetKeyUp(KeyCode.Keypad4))
        {
            InputToAction Script = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputToAction>();
            Script.SpawnNode();
        }

        if (Input.GetKeyUp(KeyCode.Keypad5))
        {
            InputToAction Script = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputToAction>();
            Script.SpawnLink();
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            InputToAction Script = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputToAction>();
            Script.UpdateGraph();
        }

        if (Input.GetKeyUp(KeyCode.T))
        {
            InputToAction Script = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputToAction>();
            Script.Save();
        }

        if (Input.GetKeyUp(KeyCode.V))
        {
            InputToAction Script = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputToAction>();
            Script.BlurScreen();
        }

    }

}
