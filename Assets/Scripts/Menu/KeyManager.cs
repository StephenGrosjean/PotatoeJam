using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script to add the keys to the InputManager 
/// </summary>
public class KeyManager : MonoBehaviour {
    [SerializeField] private GameObject Inputs;

    public string[] Keys;
    public string[] KeyMap;

    private InputManager InputManagerScript;
    private const float GetKey_WaitTime = 0.2f;

    private void Start() {
        InputManagerScript = GameObject.FindGameObjectWithTag("InputManager").GetComponent<InputManager>();
        StartCoroutine("GetKey");
    }

    //Add the keys to the Keys array
    public void AddKey(string Key, int ID) {
        Keys[ID] = Key;
    }

    //Save into the Json by passing the keys array to the Input Manager 
    public void Save() {
        InputManagerScript.WriteJson(Keys);
        Read();
    }

    //Read the keys from the InputManager
    void Read() {
        KeyMap[0] = InputManagerScript.Inputs.Left;
        KeyMap[1] = InputManagerScript.Inputs.Right;
        KeyMap[2] = InputManagerScript.Inputs.Jump;
        KeyMap[3] = InputManagerScript.Inputs.Dash;
        KeyMap[4] = InputManagerScript.Inputs.Inhale;
        KeyMap[5] = InputManagerScript.Inputs.Smash;
    }
    
    IEnumerator GetKey() {
        yield return new WaitForSeconds(GetKey_WaitTime);
        Read();
    }

}
