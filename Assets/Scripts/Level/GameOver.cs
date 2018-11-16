﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour {

	void Start () {
        StartCoroutine("BackToMenu");
	}


    IEnumerator BackToMenu() {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("MainMenu");
    }
}
