using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Check if the mouse is moving to disable all button keyboard or controller control
/// </summary>
public class MouveMove : MonoBehaviour {

    [SerializeField] private GameObject[] Buttons;
    [SerializeField] private float timeBeforeDisable;
    [SerializeField] private float enableDampTimer;

    private Vector2 mousePreviousPos;
    private Vector2 mouseCurrentPos;
    private bool mouseMoving;
    private float time;
    private bool canEnable;

	// Use this for initialization
	void Start () {
        mouseCurrentPos = Input.mousePosition;
	}
	
	// Update is called once per frame
	void Update () {
        mousePreviousPos = mouseCurrentPos;
        mouseCurrentPos = Input.mousePosition;

        if (time >= timeBeforeDisable) {
            time = timeBeforeDisable;
        }

        if (!mouseMoving && mousePreviousPos == mouseCurrentPos) {
            time = 0;
        }

        if (mousePreviousPos != mouseCurrentPos && time < timeBeforeDisable) {
            time += Time.deltaTime;
        }
        else if (mousePreviousPos != mouseCurrentPos && time >= timeBeforeDisable) {
            mouseMoving = true;
            disableScripts();
        }
        else {
            mouseMoving = false;
            if (canEnable) {
                enableScripts();
            }
        }
	}

    void disableScripts() {
        foreach (GameObject obj in Buttons) {
            if(SceneManager.GetActiveScene().name == "MainMenu") {
                obj.GetComponent<Selected>().enabled = false;
            }
            else {
                obj.GetComponent<SelectedTutorial>().enabled = false;
            }
        }
        StartCoroutine("enableDamp");
    }

    void enableScripts() {
        foreach (GameObject obj in Buttons) {
            if (SceneManager.GetActiveScene().name == "MainMenu") {
                obj.GetComponent<Selected>().enabled = true;
            }
            else {
                obj.GetComponent<SelectedTutorial>().enabled = true;
            }
        }
    }

    IEnumerator enableDamp() {
        canEnable = false;
        yield return new WaitForSeconds(enableDampTimer);
        canEnable = true;
    }
}
