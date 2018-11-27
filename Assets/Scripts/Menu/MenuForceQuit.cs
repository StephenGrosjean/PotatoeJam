using UnityEngine;
/// <summary>
/// Menu force quit with escape, it exist because of Elias asked for it ;)
/// </summary>
public class MenuForceQuit : MonoBehaviour {
	void Update () {
        //Check if escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();//Quit the app
        }
	}
}
