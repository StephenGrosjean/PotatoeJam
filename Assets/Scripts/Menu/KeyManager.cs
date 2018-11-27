using System.Collections;
using UnityEngine;
/// <summary>
/// Script to add the keys to the InputManager 
/// </summary>
public class KeyManager : MonoBehaviour {
    [SerializeField] private GameObject inputs;

    [SerializeField] private string[] keys;
    public string[] Keys
    {
        get { return keys; }
        set { keys = value; }
    }

    [SerializeField] private string[] keyMap;
    public string[] KeyMap
    {
        get { return keyMap; }
        set { keyMap = value; }
    }

    private InputManager inputManagerScript;
    private const float GetKeyWaitTime = 0.2f;

    private void Start() {
        inputManagerScript = GameObject.FindGameObjectWithTag("InputManager").GetComponent<InputManager>();
        StartCoroutine("GetKey");
    }

    //Add the keys to the Keys array
    public void AddKey(string key, int id) {
        Keys[id] = key;
    }

    //Save into the Json by passing the keys array to the Input Manager 
    public void Save() {
        inputManagerScript.WriteJson(Keys);
        Read();
    }


    //Read the keys from the InputManager
    void Read() {
        KeyMap[0] = inputManagerScript.Inputs.Left;
        KeyMap[1] = inputManagerScript.Inputs.Right;
        KeyMap[2] = inputManagerScript.Inputs.Jump;
        KeyMap[3] = inputManagerScript.Inputs.Dash;
        KeyMap[4] = inputManagerScript.Inputs.Inhale;
        KeyMap[5] = inputManagerScript.Inputs.Smash;
    }
    
    IEnumerator GetKey() {
        yield return new WaitForSeconds(GetKeyWaitTime);
        Read();
    }

}
