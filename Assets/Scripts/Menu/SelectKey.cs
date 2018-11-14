using System.Collections;
using UnityEngine;
using TMPro;
/// <summary>
/// Script to be able to select the key in the Main Menu
/// </summary>

public class SelectKey : MonoBehaviour {

    [SerializeField] private GameObject Text;
    [SerializeField] private int ID;

    private string Key;
    private string ProcessedKey;
    private bool WantToChange;
    private string[] AlowedKeys;
    private bool ValidKey;
    private const int KeyPressTimeout = 10;
    private const float GetKeyTimer = 0.5f;

    private TextMeshProUGUI TextMeshComponent;
    private KeyManager KeyManagerScript;

    void Start () {
        ValidKey = false;
        AlowedKeys = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", " " };
        KeyManagerScript = GetComponentInParent<KeyManager>();
        TextMeshComponent = Text.GetComponent<TextMeshProUGUI>();

        StartCoroutine("GetKey");
    }
	
	void Update () {
        if (WantToChange) {

            Key = Input.inputString; //Asign variable Key as the current Input

            if (Input.anyKey && WantToChange) {
                
                ProcessedKey = Key.ToLower(); //Lowercase the string

                foreach (string character in AlowedKeys) {
                    if(character == ProcessedKey) {
                        //If the Key is inside the AllowedKeys then it's valid
                        ValidKey = true;
                    }
                }

                //check if two keys have not been pressed or that it's invalid
                if(ProcessedKey.Length > 1 || !ValidKey) {
                    ProcessedKey = "#";
                }else if(ProcessedKey == " ") { //Check if the key is a space
                    ProcessedKey = "space";
                }

                TextMeshComponent.SetText(ProcessedKey); //Set the text to the name of the key

                WantToChange = false;

                if (ValidKey) {
                    KeyManagerScript.AddKey(ProcessedKey, ID); //Add key to the Key Manager
                }
            }
            
        }
	}


   public void ChangeKey() {
        ValidKey = false;
        StartCoroutine("WantChange");
    }

    IEnumerator WantChange() {
        WantToChange = true;
        TextMeshComponent.SetText("_");
        yield return new WaitForSeconds(KeyPressTimeout); //Set a timeout for pressing the key
        WantToChange = false;
    }

    //Get the key from the Json
    IEnumerator GetKey() {
        yield return new WaitForSeconds(GetKeyTimer);
        string KeyFromJson = KeyManagerScript.KeyMap[ID];
        TextMeshComponent.SetText(KeyFromJson);
        KeyManagerScript.AddKey(TextMeshComponent.text, ID);
    }
}
