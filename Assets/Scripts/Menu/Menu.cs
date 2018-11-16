using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
/// <summary>
/// Script for all the Main Menu button actions
/// </summary>

public class Menu : MonoBehaviour {

    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject xboxPanel;
    [SerializeField] private GameObject keyboardPanel;
    [SerializeField] private TextMeshProUGUI layoutText;

    private string currentLayout = "Keyboard";
    private Animator panelAnimator;

    void Start() {
        panelAnimator = panel.GetComponent<Animator>();
    }



    public void Play() {
        SceneManager.LoadScene("Game");
    }

    public void NewGame() {
        PlayerPrefs.SetInt("CheckPoint", 0); //Reset the player progression 
        SceneManager.LoadScene("Game");
    }

    public void PlayPanel() {
        panelAnimator.Play("GotoPlay");
    }

    public void Back() {
        panelAnimator.Play("GotoMain");
    }

    public void BackFromSettings() {
        panelAnimator.Play("GotoMainFromSettings");
    }

    public void GotoCreditsFromMain() {
        panelAnimator.Play("GotoCredits");
    }

    public void GotoMainFromCredits() {
        panelAnimator.Play("GotoMainFromCredits");
    }

    public void Settings() {
        panelAnimator.Play("GotoSettings");
    }

    public void LayoutButton() {
        if(currentLayout == "Keyboard") {
            keyboardPanel.SetActive(false);
            xboxPanel.SetActive(true);
            currentLayout = "Xbox";
            layoutText.text = currentLayout;
            PlayerPrefs.SetString("ControlLayout", currentLayout);
        }
        else {
            keyboardPanel.SetActive(true);
            xboxPanel.SetActive(false);
            currentLayout = "Keyboard";
            layoutText.text = currentLayout;
            PlayerPrefs.SetString("ControlLayout", currentLayout);
        }
        
    }

    public void Exit() {
        Application.Quit();
    }




}
