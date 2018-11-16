using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Script to fade to black the screen
/// </summary>

public class Darkend : MonoBehaviour {
   
    private bool dark;
    private float alpha;
    private const float AlphaIncreaseValue = 0.01f;
    private const int DeathWaitTime = 1;

    private SpriteRenderer spriteRendererComponent;

    private void Start() {
        spriteRendererComponent = GetComponent<SpriteRenderer>();
    }

    void Update () {
        if (dark) {
            alpha += AlphaIncreaseValue;
            if(alpha >= 1) {
                SceneManager.LoadScene("GameOver");
            }
           spriteRendererComponent.color = new Color(0, 0, 0, alpha);
        }
        
	}

    IEnumerator DeathProtocol() {
        yield return new WaitForSeconds(DeathWaitTime);
        dark = true;
    }
}
