using UnityEngine;
using UnityEngine.UI;

public class PauseControls : MonoBehaviour {
    [Header ("Button")]
    [SerializeField] private Button continueButton;
    [SerializeField] private Button menuButton;
    [SerializeField] private Button restartButton;

    [Space(15)]
    [Header("Button Images Object")]
    [SerializeField] private Image continueImage;
    [SerializeField] private Image menuImage;
    [SerializeField] private Image restartImage;

    [Space(15)]
    [Header("Button Sprites Xbox")]
    [SerializeField] private Sprite continueXboxSprite;
    [SerializeField] private Sprite menuXboxSprite;
    [SerializeField] private Sprite restartXboxSprite;

    [Space(15)]
    [Header("Button Sprites Keyboard")]
    [SerializeField] private Sprite continueSprite;
    [SerializeField] private Sprite menuSprite;
    [SerializeField] private Sprite restartSprite;

    [Space(15)]
    [Header("KeyToPress Keyboard")]
    [SerializeField] private string continueKey;
    [SerializeField] private string menuKey;
    [SerializeField] private string restartKey;

    [Space(15)]
    [Header("KeyToPress Xbox")]
    [SerializeField] private string continueXboxKey;
    [SerializeField] private string menuXboxKey;
    [SerializeField] private string restartXboxKey;

    private bool isXboxControl;

	// Use this for initialization
	void Start () {
        isXboxControl = (PlayerPrefs.GetString("ControlLayout") == "Xbox");

        if (isXboxControl) {
            continueImage.sprite = continueXboxSprite;
            menuImage.sprite = menuXboxSprite;
            restartImage.sprite = restartXboxSprite;
        }
        else {
            continueImage.sprite = continueSprite;
            menuImage.sprite = menuSprite;
            restartImage.sprite = restartSprite;
        }

    }
	
	// Update is called once per frame
	void Update () {
       

        if (isXboxControl) {
            if (Input.GetButtonDown(continueXboxKey)) {
                continueButton.onClick.Invoke();
            }
            if (Input.GetButtonDown(menuXboxKey)) {
                menuButton.onClick.Invoke();
            }
            if (Input.GetButtonDown(restartXboxKey)) {
                restartButton.onClick.Invoke();
            }
        }
        else {
            if (Input.GetKeyDown(continueKey)) {
                continueButton.onClick.Invoke();
            }
            if (Input.GetKeyDown(menuKey)) {
                menuButton.onClick.Invoke();
            }
            if (Input.GetKeyDown(restartKey)) {
                restartButton.onClick.Invoke();
            }
        }
        

    }
}
