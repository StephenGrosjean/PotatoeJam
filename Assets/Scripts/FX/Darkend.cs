using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Script to fade to black the screen
/// </summary>

public class Darkend : MonoBehaviour {
   
    private bool Dark;
    private float Alpha;
    private const float AlphaIncreaseValue = 0.01f;
    private const int DeathWaitTime = 1;

    private SpriteRenderer SpriteRendererComponent;

    private void Start() {
        SpriteRendererComponent = GetComponent<SpriteRenderer>();
    }

    void Update () {
        if (Dark) {
            Alpha += AlphaIncreaseValue;
            if(Alpha >= 1) {
                SceneManager.LoadScene("GameOver");
            }
           SpriteRendererComponent.color = new Color(0, 0, 0, Alpha);
        }
        
	}

    IEnumerator DeathProtocol() {
        yield return new WaitForSeconds(DeathWaitTime);
        Dark = true;
    }
}
