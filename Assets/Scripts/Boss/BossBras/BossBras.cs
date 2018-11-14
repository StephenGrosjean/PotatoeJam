using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Boss Manager
/// </summary>
public class BossBras : MonoBehaviour {
    [SerializeField] private GameObject Popup;
    [SerializeField] private GameObject BrasSmash;
    [SerializeField] private GameObject BrasClap;
    [SerializeField] private int WaitTurnTime;

    public string Turn = "Smash";
    private BossBrasClap BossBrasClapScript;
    private BossBrasSmash BossBrasSmashScript;

    private void Start() {
        BossBrasClapScript = BrasClap.GetComponent<BossBrasClap>();
        BossBrasSmashScript = BrasSmash.GetComponent<BossBrasSmash>();

        Popup.SetActive(true);

        StartCoroutine("UpdateTurn");
        StartCoroutine("TurnSelector");

    }

    IEnumerator UpdateTurn () {
        switch (Turn) {
            case "Smash":

                if (BrasClap.activeSelf) {
                    BossBrasClapScript.StartCoroutine("States", "Exit");
                }
                yield return new WaitForSeconds(1.5f);

                BrasSmash.SetActive(true);
                break;

            case "Clap":
                if (BrasSmash.activeSelf) {
                    BossBrasSmashScript.StartCoroutine("States", "Exit");
                }

                yield return new WaitForSeconds(1.5f);
                BrasClap.SetActive(true);

                break;
        }
	}

    //Change Boss stage
    IEnumerator TurnSelector() {
        yield return new WaitForSeconds(WaitTurnTime);
        switch (Turn) {
            case "Smash":
                Turn = "Clap";
                StartCoroutine("UpdateTurn");
                StartCoroutine("TurnSelector");
                break;

            case "Clap":
                Turn = "Smash";
                UpdateTurn();
                StartCoroutine("UpdateTurn");
                StartCoroutine("TurnSelector");
                break;
        }
        
    }
}
