using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Script to play the text box at the same time as the voice
/// </summary>
public class VoiceOverController : MonoBehaviour {
    [SerializeField] private GameObject cam;
    [SerializeField] private GameObject firstPopup, secondPopup, thirdPopup;
    [SerializeField] private float timer1, timer2, timer3;

    private Darkend fadeoutScript;

	void Start () {
        fadeoutScript = cam.GetComponent<Darkend>();
        StartCoroutine("VoiceSequence");
	}

    IEnumerator VoiceSequence() {

        firstPopup.SetActive(true);
        yield return new WaitForSeconds(timer1);

        firstPopup.SetActive(false);
        secondPopup.SetActive(true);

        yield return new WaitForSeconds(timer2);

        secondPopup.SetActive(false);
        thirdPopup.SetActive(true);
        fadeoutScript.StartCoroutine(fadeoutScript.DeathProtocol());

        yield return new WaitForSeconds(timer3);
        
        SceneManager.LoadScene("Game");
    }
}
