﻿using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Script with the Pause menu actions
/// </summary>

public class MenuPause : MonoBehaviour {
    [SerializeField] private GameObject mainButton;

    private const int normalTimeScale = 1;
    private InputManager inputManagerScript;
    private GameObject inputManager;

    private void Start() {
        inputManager = GameObject.Find("InputManager");
        inputManagerScript = inputManager.GetComponent<InputManager>();
    }

    public void Continue() {
        Time.timeScale = normalTimeScale; //Make the time flow normaly
        gameObject.SetActive(false);
    }

    public void BackToMenu() {
        Time.timeScale = normalTimeScale; //Make the time flow normaly
        SceneManager.LoadScene("MainMenu");
    }

    public void NewGame() {
        Time.timeScale = normalTimeScale; //Make the time flow normaly
        SceneManager.LoadScene("NewGame");

    }

}
