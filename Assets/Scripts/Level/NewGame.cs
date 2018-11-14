using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlayerPrefs.SetInt("CheckPoint", 0);
        SceneManager.LoadScene("Game");
	}

}
