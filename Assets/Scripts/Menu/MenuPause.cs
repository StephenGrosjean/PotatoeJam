using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Script with the Pause menu actions
/// </summary>

public class MenuPause : MonoBehaviour {

    private const int normalTimeScale = 1;

    public void Continue() {
        Time.timeScale = normalTimeScale;
        gameObject.SetActive(false);
    }

    public void BackToMenu() {
        Time.timeScale = normalTimeScale;
        SceneManager.LoadScene("MainMenu");
    }

    public void NewGame() {
        Time.timeScale = normalTimeScale;
        SceneManager.LoadScene("NewGame");

    }
}
