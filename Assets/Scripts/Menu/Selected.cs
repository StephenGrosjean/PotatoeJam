using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selected : MonoBehaviour {
    [SerializeField] private GameObject up, down, left, right;
    [SerializeField] private string activeZone;
    [SerializeField] private GameObject menu;
    [SerializeField] private Image selectedImage;
    [SerializeField] private bool isXboxControl;
    [SerializeField] private bool isSelected;
    public bool IsSelected {
        get { return isSelected; }
        set { isSelected = value; }
    }

    private Selected upSelectedScript, downSelectedScript, leftSelectedScript, rightSelectedScript;
    private bool canSelect = true;
    private bool previousState, currentState;
    private Button ButtonScript;
    private GameObject InputManager;
    private InputManager inputManagerScript;
    private Menu menuScript;
    public bool wasEnabled;

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
        ButtonScript = GetComponent<Button>();
        InputManager = GameObject.Find("InputManager");
        inputManagerScript = InputManager.GetComponent<InputManager>();
        currentState = IsSelected;

        menuScript = menu.GetComponent<Menu>();

        if(up != null) {
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
                        ButtonScript.onClick.Invoke();
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
                        ButtonScript.onClick.Invoke();
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
