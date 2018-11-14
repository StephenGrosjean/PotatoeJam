using System.Collections;
using UnityEngine;
/// <summary>
/// Script to manage the level activation
/// </summary>

public class LevelManager : MonoBehaviour {
    [SerializeField] private GameObject LevelContainer, LevelTrigger, Door, EnemyContainer, PopUp, ArrowGo;
    [SerializeField] private Transform BorderPos;

    private LevelTrigger LevelTriggerScript;
    private const int PopWaitTime = 1;

	void Start () {
        LevelTriggerScript = LevelTrigger.GetComponent<LevelTrigger>();
	}


    void Update() {
        //Check if there is some enemies left
        if (EnemyContainer.transform.childCount == 0) {
            //If no destroy the door and display arrows
            Destroy(Door);
            ArrowGo.SetActive(true);
        }

        //Asign the BorderWall to a new position at the start of the level
        if(LevelTrigger != null) {
            if (LevelTriggerScript.Triggered) {
                Destroy(LevelTrigger);
                LevelContainer.SetActive(true);
                StartCoroutine("Pop");
            }
        }
	}

    IEnumerator Pop() {
        yield return new WaitForSeconds(PopWaitTime);
        //Start the Tip PopUp
        if(PopUp != null) {
            PopUp.SetActive(true);
            PopUp.GetComponent<Popup>().Pop();
        }
        
    }
}
