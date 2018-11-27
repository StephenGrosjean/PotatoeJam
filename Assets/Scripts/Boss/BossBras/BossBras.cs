using System.Collections;
using UnityEngine;
/// <summary>
/// Boss Manager
/// </summary>
public class BossBras : MonoBehaviour {
    [SerializeField] private GameObject popup;
    [SerializeField] private GameObject brasSmash;
    [SerializeField] private GameObject brasClap;
    [SerializeField] private int waitTurnTime;
    [SerializeField] private string turn = "Smash";

    private BossBrasClap bossBrasClapScript;
    private BossBrasSmash bossBrasSmashScript;

    private const float timeWait = 1.5f;


    private void Start() {
        bossBrasClapScript = brasClap.GetComponent<BossBrasClap>();
        bossBrasSmashScript = brasSmash.GetComponent<BossBrasSmash>();
        
        popup.SetActive(true); //Enable the tip popup

        TurnSequence();

    }

    void TurnSequence() {
        StartCoroutine("UpdateTurn"); 
        StartCoroutine("TurnSelector");
    }

    //Switch the stage of the boss
    IEnumerator UpdateTurn () {
        switch (turn) {
            case "Smash":

                if (brasClap.activeSelf) {
                    bossBrasClapScript.InvokeState("Exit");
                }
                yield return new WaitForSeconds(timeWait);

                brasSmash.SetActive(true);
                break;

            case "Clap":
                if (brasSmash.activeSelf) {
                    bossBrasSmashScript.InvokeState("Exit");
                }

                yield return new WaitForSeconds(timeWait);
                brasClap.SetActive(true);

                break;
        }
	}

    //Change Boss stage
    IEnumerator TurnSelector() {
        yield return new WaitForSeconds(waitTurnTime);
        switch (turn) {
            case "Smash":
                turn = "Clap";
                TurnSequence();
                break;

            case "Clap":
                turn = "Smash";
                UpdateTurn();
                TurnSequence();
                break;
        }
        
    }

}
