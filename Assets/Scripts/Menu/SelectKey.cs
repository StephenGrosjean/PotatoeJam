using System.Collections;
using UnityEngine;
using TMPro;
/// <summary>
/// Script to be able to select the key in the Main Menu
/// </summary>

public class SelectKey : MonoBehaviour {

    [SerializeField] private GameObject text;
    [SerializeField] private int id;

    private string key;
    private string processedKey;
    private bool wantToChange;
    private string[] alowedKeys;
    private bool validKey;
    private const int KeyPressTimeout = 10;
    private const float GetKeyTimer = 0.5f;

    private TextMeshProUGUI textMeshComponent;
    private KeyManager keyManagerScript;

    void Start () {
        validKey = false;
        alowedKeys = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", " " };
        keyManagerScript = GetComponentInParent<KeyManager>();
        textMeshComponent = text.GetComponent<TextMeshProUGUI>();

        StartCoroutine("GetKey");
    }
	
	void Update () {
        if (wantToChange) {

            key = Input.inputString; //Asign variable Key as the current Input

            if (Input.anyKey && wantToChange) {
                
                processedKey = key.ToLower(); //Lowercase the string

                foreach (string character in alowedKeys) {
                    if(character == processedKey) {
                        //If the Key is inside the AllowedKeys then it's valid
                        validKey = true;
                    }
                }

                //check if two keys have not been pressed or that it's invalid
                if(processedKey.Length > 1 || !validKey) {
                    processedKey = "#";
                }else if(processedKey == " ") { //Check if the key is a space
                    processedKey = "space";
                }

                textMeshComponent.SetText(processedKey); //Set the text to the name of the key

                wantToChange = false;

                if (validKey) {
                    keyManagerScript.AddKey(processedKey, id); //Add key to the Key Manager
                }
            }
            
        }
	}


   public void ChangeKey() {
        validKey = false;
        StartCoroutine("WantChange");
    }

    IEnumerator WantChange() {
        wantToChange = true;
        textMeshComponent.SetText("_");
        yield return new WaitForSeconds(KeyPressTimeout); //Set a timeout for pressing the key
        wantToChange = false;
    }

    //Get the key from the Json
    IEnumerator GetKey() {
        yield return new WaitForSeconds(GetKeyTimer);
        string keyFromJson = keyManagerScript.KeyMap[id];
        textMeshComponent.SetText(keyFromJson);
        keyManagerScript.AddKey(textMeshComponent.text, id);
    }
}
