using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Script that change the button stage if a save is found
/// </summary>
public class ButtonBehaviour : MonoBehaviour {

    [SerializeField] private Color disabledColor, enabledColor;
    [SerializeField] private TextMeshProUGUI currentLevelText;

    private int level;
    private Image imageComponent;
    private Button buttonComponent;
    private Selected selectedScript;
   

    private void Start() {
        level = PlayerPrefs.GetInt("CheckPoint");
        imageComponent = GetComponent<Image>();
        buttonComponent = GetComponent<Button>();
        selectedScript = GetComponent<Selected>();

        SetButtonState();
    }

    void SetButtonState() {
        if(level == 0) {
            imageComponent.color = disabledColor;
            buttonComponent.enabled = false;
            selectedScript.CanCallEvent = false;
            currentLevelText.text = "No save found";
        }
        else {
            imageComponent.color = enabledColor;
            buttonComponent.enabled = true;
            selectedScript.CanCallEvent = true;
            currentLevelText.text = "Current level : " + level;
        }
    }


}
