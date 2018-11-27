using System.Collections;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Make a button selectable with keyboard and controller input
/// </summary>
public class Selected : MonoBehaviour {
    [SerializeField] private AudioClip clickOkSound, clickNoSound;
    [SerializeField] private GameObject up, down, left, right; //what is the next button to activate?
    [SerializeField] private string activeZone;
    [SerializeField] private GameObject menu;
    [SerializeField] private Image selectedImage;
    [SerializeField] private bool isXboxControl;
    [SerializeField] private bool isSelected;
    public bool IsSelected {
        get { return isSelected; }
        set { isSelected = value; }
    }

    [SerializeField] private bool canCallEvent = true;
    public bool CanCallEvent {
        get { return canCallEvent; }
        set { canCallEvent = value; }

    }

    private Selected upSelectedScript, downSelectedScript, leftSelectedScript, rightSelectedScript;
    private bool canSelect = true;
    private bool previousState, currentState;
    private Button ButtonScript;
    private GameObject InputManager;
    private InputManager inputManagerScript;
    private Menu menuScript;
    public bool wasEnabled;
    private AudioSource audioSourceComponent;

    private void OnDisable() {
        wasEnabled = currentState;
        IsSelected = false;
        selectedImage.gameObject.SetActive(IsSelected);

    }

    private void OnEnable() {
        IsSelected = wasEnabled;
        selectedImage.gameObject.SetActive(IsSelected);
    }

    void Start () {
        audioSourceComponent = GetComponent<AudioSource>();
        ButtonScript = GetComponent<Button>();
        InputManager = GameObject.Find("InputManager");
        inputManagerScript = InputManager.GetComponent<InputManager>();
        currentState = IsSelected;

        menuScript = menu.GetComponent<Menu>();

        if (up != null) {
            upSelectedScript = up.GetComponent<Selected>();
        }
        if(down != null) {
            downSelectedScript = down.GetComponent<Selected>();
        }
        if(left != null) {
            leftSelectedScript = left.GetComponent<Selected>();
        }
        if (right != null) {
            rightSelectedScript = right.GetComponent<Selected>();
        }
    }

    void Update() {
        isXboxControl = inputManagerScript.IsXboxControls;

        previousState = currentState;
        currentState = IsSelected;

        if (currentState != previousState) {
            StartCoroutine("SelectCooldown");
        }

        if (activeZone == menuScript.CurrentPanel) {
            selectedImage.gameObject.SetActive(IsSelected);
        }
        else {
            selectedImage.gameObject.SetActive(false);
            IsSelected = false;
        }
        

        if (canSelect) {
            if (isXboxControl) {
                if (IsSelected && activeZone == menuScript.CurrentPanel) {
                    if (Input.GetAxis("X360_DPad_Vertical") < 0) {
                        if (down != null) {
                            IsSelected = false;
                            downSelectedScript.IsSelected = true;
                        }
                    }
                    if (Input.GetAxis("X360_DPad_Vertical") > 0) {
                        if (up != null) {
                            IsSelected = false;
                            upSelectedScript.IsSelected = true;
                        }
                    }
                    if (Input.GetAxis("X360_DPad_Horizontal") < 0) {
                        if (left != null) {
                            IsSelected = false;
                            leftSelectedScript.IsSelected = true;
                        }
                    }
                    if (Input.GetAxis("X360_DPad_Horizontal") > 0) {
                        if (right != null) {
                            IsSelected = false;
                            rightSelectedScript.IsSelected = true;
                        }
                    }
                    if (Input.GetButtonDown("X360_Jump")) {
                        if (CanCallEvent) {
                            ButtonScript.onClick.Invoke();
                            audioSourceComponent.PlayOneShot(clickOkSound);
                        }
                        else {
                            audioSourceComponent.PlayOneShot(clickNoSound);
                        }
                    }
                }
            }
            else {
                if (IsSelected && activeZone == menuScript.CurrentPanel) {
                    if (Input.GetKeyDown(KeyCode.DownArrow)) {
                        if (down != null) {
                            IsSelected = false;
                            downSelectedScript.IsSelected = true;
                        }
                    }
                    if (Input.GetKeyDown(KeyCode.UpArrow)) {
                        if (up != null) {
                            IsSelected = false;
                            upSelectedScript.IsSelected = true;
                        }
                    }
                    if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                        if (left != null) {
                            IsSelected = false;
                            leftSelectedScript.IsSelected = true;
                        }
                    }
                    if (Input.GetKeyDown(KeyCode.RightArrow)) {
                        if (right != null) {
                            IsSelected = false;
                            rightSelectedScript.IsSelected = true;
                        }
                    }
                    if (Input.GetKeyDown(KeyCode.Return)) {
                        if (CanCallEvent) {
                            ButtonScript.onClick.Invoke();
                            audioSourceComponent.PlayOneShot(clickOkSound);
                        }
                        else {
                            audioSourceComponent.PlayOneShot(clickNoSound);
                        }
                    }
                }
            }
        }
    }

    IEnumerator SelectCooldown() {
        canSelect = false;
        yield return new WaitForSeconds(0.1f);
        canSelect = true;
    }
}
