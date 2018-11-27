using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Script that display the end text boxes
/// </summary>
public class EndGameController : MonoBehaviour {
    [SerializeField] private GameObject firstPopup, secondPopup, thirdPopup;
    [SerializeField] private AnimationClip clip;

	void Start () {
        StartCoroutine("endSequence");
	}

    IEnumerator endSequence() {
        yield return new WaitForSeconds(clip.length/2);
        firstPopup.SetActive(true);
        yield return new WaitForSeconds(clip.length);
        firstPopup.SetActive(false);
        secondPopup.SetActive(true);
        yield return new WaitForSeconds(clip.length);
        secondPopup.SetActive(false);
        thirdPopup.SetActive(true);
        yield return new WaitForSeconds(clip.length);
        SceneManager.LoadScene("MainMenu");
    }
}
