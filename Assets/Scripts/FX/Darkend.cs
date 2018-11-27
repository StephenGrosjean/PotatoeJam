using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Script to fade to black the screen
/// </summary>

public class Darkend : MonoBehaviour {
    [SerializeField] private bool callGameOver = true;
    private bool dark;
    private float alpha;
    private const float AlphaIncreaseValue = 0.01f;
    private const int deathWaitTime = 1;

    private SpriteRenderer spriteRendererComponent;

    private void Start() {
        spriteRendererComponent = GetComponent<SpriteRenderer>();
    }

    void Update () {
        if (dark) {
            alpha += AlphaIncreaseValue;
            if(alpha >= 1 && callGameOver) {
                SceneManager.LoadScene("GameOver");
            }
           spriteRendererComponent.color = new Color(0, 0, 0, alpha);
        }
	}

    public IEnumerator DeathProtocol() {
        yield return new WaitForSeconds(deathWaitTime);
        dark = true;
    }
}
