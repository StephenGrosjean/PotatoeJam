using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
/// <summary>
/// Script to fade the audio and the screen
/// </summary>
public class FadeIn : MonoBehaviour {
    [SerializeField] private float fadeInSpeed;
    [SerializeField] private AudioMixer mixer;

    private const int volumeScale = 15;
    private Image whiteScreen;
    private float alpha;

	void Start () {
        mixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("Volume") / volumeScale);
        whiteScreen = GetComponent<Image>();
        alpha = 1.0f;

        StartCoroutine("Fade");
    }

    IEnumerator Fade() {
        while (alpha != 0.0f) {
            whiteScreen.color = new Color(1, 1, 1, alpha);
            yield return new WaitForSeconds(fadeInSpeed);
            alpha -= fadeInSpeed/2;
        }
    }
}
