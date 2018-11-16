using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

/// <summary>
/// Script that replace the Unity Input Manager for the game
/// </summary>

public class InputManager : MonoBehaviour {

    public Keys Inputs;

    private string json;

    [Serializable]
    public class Keys {
        public string Left;
        public string Right;
        public string Jump;
        public string Dash;
        public string Inhale;
        public string Smash;
    }


	void Start () {
        Invoke("ReadJson", 0.1f);
	}

    //Method to write the keys into a Json
    public void WriteJson(string[] key) {
        Inputs = new Keys(); //Create a new instance Keys()

        //Asign key values
        Inputs.Left = key[0];
        Inputs.Right = key[1];
        Inputs.Jump = key[2];
        Inputs.Dash = key[3];
        Inputs.Inhale = key[4];
        Inputs.Smash = key[5];

        //Write to Input.json
        json = JsonUtility.ToJson(Inputs);
        File.WriteAllText(Application.dataPath + "/Input.json", string.Empty);
        File.WriteAllText(Application.dataPath + "/Input.json", json);
    }

    //Method to red the Json
    public void ReadJson() {
        json = File.ReadAllText(Application.dataPath + "/Input.json");

        //Set the Inputs 
        Inputs = JsonUtility.FromJson<Keys>(json);

    }
}
