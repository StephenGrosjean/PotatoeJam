using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
/// <summary>
/// Script for all the Main Menu button actions
/// </summary>

public class Menu : MonoBehaviour {

    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject panelDetector;
    [SerializeField] private GameObject xboxPanel;
    [SerializeField] private GameObject keyboardPanel;
    [SerializeField] private TextMeshProUGUI layoutText;
    [SerializeField] private GameObject buttonMain, buttonPlay, buttonSettings, buttonCredits;
    [SerializeField] private string currentPanel;
    public string CurrentPanel {
        get { return currentPanel; }
        set { currentPanel = value; }
    }

    private string currentLayout = "Keyboard";
    private Animator panelAnimator;
    private Selected buttonMainScript, buttonPlayScript, buttonSettingsScript, buttonCreditsScript;
    private PanelDetector panelDetectorScript;
    private string previousPanel;

    void Start() {
        Cursor.visible = true;
        currentLayout = GetLayout();
        SetLayoutUI();

        buttonMainScript = buttonMain.GetComponent<Selected>();
        buttonPlayScript = buttonPlay.GetComponent<Selected>();
        buttonSettingsScript = buttonSettings.GetComponent<Selected>();
        buttonCreditsScript = buttonCredits.GetComponent<Selected>();
        panelAnimator = panel.GetComponent<Animator>();
        panelDetectorScript = panelDetector.GetComponent<PanelDetector>();
    }

    private void Update() {
        previousPanel = currentPanel;
        CurrentPanel = panelDetectorScript.CurrentPanel;

        if(previousPanel != currentPanel) {
            Debug.Log("Enable");
            EnableMainButton();
        }
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
        buttonPlayScript.IsSelected = true;
    }

    public void Back() {
        panelAnimator.Play("GotoMain");
        buttonMainScript.IsSelected = true;
    }

    public void BackFromSettings() {
        panelAnimator.Play("GotoMainFromSettings");
        buttonMainScript.IsSelected = true;
    }

    public void GotoCreditsFromMain() {
        panelAnimator.Play("GotoCredits");
        buttonCreditsScript.IsSelected = true;
    }

    public void GotoMainFromCredits() {
        panelAnimator.Play("GotoMainFromCredits");
        buttonMainScript.IsSelected = true;
    }

    public void Settings() {
        panelAnimator.Play("GotoSettings");
        buttonSettingsScript.IsSelected = true;
    }

    public void LayoutButton() {
        if(currentLayout == "Keyboard") {
            keyboardPanel.SetActive(false);
            xboxPanel.SetActive(true);
            currentLayout = "Xbox";
            layoutText.text = currentLayout;
            SaveLayout(currentLayout);
        }
        else {
            keyboardPanel.SetActive(true);
            xboxPanel.SetActive(false);
            currentLayout = "Keyboard";
            layoutText.text = currentLayout;
            SaveLayout(currentLayout);
        }
        
    }

    void SetLayoutUI() {
        if(currentLayout == "Xbox") {
            keyboardPanel.SetActive(false);
            xboxPanel.SetActive(true);
            layoutText.text = currentLayout;
        }
        else {
            keyboardPanel.SetActive(true);
            xboxPanel.SetActive(false);
            layoutText.text = currentLayout;
        }
    }

    void SaveLayout(string layout) {
        PlayerPrefs.SetString("ControlLayout", layout);
        Debug.Log("Layout set to : " + layout);
    }
    string GetLayout() {
       string layout = PlayerPrefs.GetString("ControlLayout");
        return layout;
    }

    public void Exit() {
        Application.Quit();
    }


    void EnableMainButton() {
        switch (currentPanel) {
            case "Main":
                buttonMainScript.IsSelected = true;
                buttonSettingsScript.IsSelected = false;
                buttonPlayScript.IsSelected = false;
                buttonCreditsScript.IsSelected = false;
                break;
            case "Settings":
                buttonMainScript.IsSelected = false;
                buttonSettingsScript.IsSelected = true;
                buttonPlayScript.IsSelected = false;
                buttonCreditsScript.IsSelected = false;
                break;
            case "Credits":
                buttonMainScript.IsSelected = false;
                buttonSettingsScript.IsSelected = false;
                buttonPlayScript.IsSelected = false;
                buttonCreditsScript.IsSelected = true;
                break;
            case "Play":
                buttonMainScript.IsSelected = false;
                buttonSettingsScript.IsSelected = false;
                buttonPlayScript.IsSelected = true;
                buttonCreditsScript.IsSelected = false;
                break;
        }
    }

}
