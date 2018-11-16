using System.Collections;
using UnityEngine;
/// <summary>
/// Script to manage the level activation
/// </summary>

public class LevelManager : MonoBehaviour {
    [SerializeField] private GameObject levelContainer, levelTrigger, door, enemyContainer, popUp, arrowGo;
    [SerializeField] private Transform borderPos;

    private LevelTrigger levelTriggerScript;
    private const int PopWaitTime = 1;

	void Start () {
        levelTriggerScript = levelTrigger.GetComponent<LevelTrigger>();
	}


    void Update() {
        //Check if there is some enemies left
        if (enemyContainer.transform.childCount == 0) {
            //If no destroy the door and display arrows
            Destroy(door);
            arrowGo.SetActive(true);
        }

        //Asign the BorderWall to a new position at the start of the level
        if(levelTrigger != null) {
            if (levelTriggerScript.Triggered) {
                Destroy(levelTrigger);
                levelContainer.SetActive(true);
                StartCoroutine("Pop");
            }
        }
	}

    IEnumerator Pop() {
        yield return new WaitForSeconds(PopWaitTime);
        //Start the Tip PopUp
        if(popUp != null) {
            popUp.SetActive(true);
        }
        
    }
}
