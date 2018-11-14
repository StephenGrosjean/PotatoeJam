using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Script with the Pause menu actions
/// </summary>

public class MenuPause : MonoBehaviour {

    private const int NormalTimeScale = 1;

    public void Continue() {
        Time.timeScale = NormalTimeScale;
        gameObject.SetActive(false);
    }

    public void BackToMenu() {
        Time.timeScale = NormalTimeScale;
        SceneManager.LoadScene("MainMenu");
    }

    public void NewGame() {
        Time.timeScale = NormalTimeScale;
        SceneManager.LoadScene("NewGame");

    }
}
