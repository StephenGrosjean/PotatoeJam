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

    public string Turn = "Smash";
    private BossBrasClap bossBrasClapScript;
    private BossBrasSmash bossBrasSmashScript;

    private void Start() {
        bossBrasClapScript = brasClap.GetComponent<BossBrasClap>();
        bossBrasSmashScript = brasSmash.GetComponent<BossBrasSmash>();

        popup.SetActive(true);

        TurnSequence();

    }

    void TurnSequence() {
        StartCoroutine("UpdateTurn");
        StartCoroutine("TurnSelector");
    }

    IEnumerator UpdateTurn () {
        switch (Turn) {
            case "Smash":

                if (brasClap.activeSelf) {
                    bossBrasClapScript.InvokeState("Exit");
                }
                yield return new WaitForSeconds(1.5f);

                brasSmash.SetActive(true);
                break;

            case "Clap":
                if (brasSmash.activeSelf) {
                    bossBrasSmashScript.InvokeState("Exit");
                }

                yield return new WaitForSeconds(1.5f);
                brasClap.SetActive(true);

                break;
        }
	}

    //Change Boss stage
    IEnumerator TurnSelector() {
        yield return new WaitForSeconds(waitTurnTime);
        switch (Turn) {
            case "Smash":
                Turn = "Clap";
                TurnSequence();
                break;

            case "Clap":
                Turn = "Smash";
                UpdateTurn();
                TurnSequence();
                break;
        }
        
    }
}
