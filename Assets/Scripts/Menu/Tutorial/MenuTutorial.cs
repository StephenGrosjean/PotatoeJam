using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
/// <summary>
/// Script for all the Main Menu button actions
/// </summary>

public class MenuTutorial : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI slideCounter;
    [SerializeField] private GameObject slide1, slide2, slide3, slide4;
    [SerializeField] private string currentPanel;
    public string CurrentPanel {
        get { return currentPanel; }
    }

    private int slideID = 1;
    private string currentLayout = "Keyboard";
    
    void Start() {
        Cursor.visible = true;
        currentLayout = GetLayout();
    }

    public void MainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void Play() {
        SceneManager.LoadScene("Intro");
    }

    public void Next() {
        if (slideID < 4) {
            slideID++;
            UpdateSlide();
        }
    }

    public void Previous() {
        if(slideID > 1) {
            slideID--;
            UpdateSlide();
        }
    }

    string GetLayout() {
       string layout = PlayerPrefs.GetString("ControlLayout");
        return layout;
    }

    void UpdateSlide() {
        
        slideCounter.text = slideID.ToString()+ " / 4";

        switch (slideID) {
            case 1:
                slide1.SetActive(true);
                slide2.SetActive(false);
                break;

            case 2:
                slide1.SetActive(false);
                slide2.SetActive(true);
                slide3.SetActive(false);
                break;

            case 3:
                slide2.SetActive(false);
                slide3.SetActive(true);
                slide4.SetActive(false);
                break;

            case 4:
                slide3.SetActive(false);
                slide4.SetActive(true);
                break;
            
        }
    }
}
