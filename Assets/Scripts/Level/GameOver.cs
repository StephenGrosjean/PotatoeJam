using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Game over script
/// </summary>

public class GameOver : MonoBehaviour {
    private const int secondsToWait = 2;
	void Start () {
        StartCoroutine("BackToMenu");
	}


    IEnumerator BackToMenu() {
        yield return new WaitForSeconds(secondsToWait);
        SceneManager.LoadScene("MainMenu");
    }
}
