using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Script to restart a level from the begining
/// </summary>
public class NewGame : MonoBehaviour {

	void Start () {
        PlayerPrefs.SetInt("CheckPoint", 0); //Reset the checkpoint
        SceneManager.LoadScene("Game"); //Load Game
	}

}
