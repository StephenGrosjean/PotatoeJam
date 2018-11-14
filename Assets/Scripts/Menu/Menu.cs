using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
/// <summary>
/// Script for all the Main Menu button actions
/// </summary>

public class Menu : MonoBehaviour {

    [SerializeField] private GameObject Panel;
    [SerializeField] private GameObject XboxPanel;
    [SerializeField] private GameObject KeyboardPanel;
    [SerializeField] private TextMeshProUGUI LayoutText;

    private string CurrentLayout = "Keyboard";
    private Animator PanelAnimator;

    void Start() {
        PanelAnimator = Panel.GetComponent<Animator>();
    }



    public void Play() {
        SceneManager.LoadScene("Game");
    }

    public void NewGame() {
        PlayerPrefs.SetInt("CheckPoint", 0); //Reset the player progression 
        SceneManager.LoadScene("Game");
    }

    public void PlayPanel() {
        PanelAnimator.Play("GotoPlay");
    }

    public void Back() {
        PanelAnimator.Play("GotoMain");
    }

    public void BackFromSettings() {
        PanelAnimator.Play("GotoMainFromSettings");
    }

    public void GotoCreditsFromMain() {
        PanelAnimator.Play("GotoCredits");
    }

    public void GotoMainFromCredits() {
        PanelAnimator.Play("GotoMainFromCredits");
    }

    public void Settings() {
        PanelAnimator.Play("GotoSettings");
    }

    public void LayoutButton() {
        if(CurrentLayout == "Keyboard") {
            KeyboardPanel.SetActive(false);
            XboxPanel.SetActive(true);
            CurrentLayout = "Xbox";
            LayoutText.text = CurrentLayout;
            PlayerPrefs.SetString("ControlLayout", CurrentLayout);
        }
        else {
            KeyboardPanel.SetActive(true);
            XboxPanel.SetActive(false);
            CurrentLayout = "Keyboard";
            LayoutText.text = CurrentLayout;
            PlayerPrefs.SetString("ControlLayout", CurrentLayout);
        }
        
    }

    public void Exit() {
        Application.Quit();
    }




}
