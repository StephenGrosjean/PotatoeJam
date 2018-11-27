using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

/// <summary>
/// Script to manage volume 
/// </summary>

public class Volume : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private string playerPrefName;
    [SerializeField] private string mixerPropertyName;
    [SerializeField] private AudioClip soundToPlay;
    [SerializeField] private AudioMixer mixer;

    private float vol;
    private const int volumeScale = 100;
    
    private Slider sliderVolume;

    private AudioSource audioSourceComponent;

    private bool canPlaySound;
    private const int minDb = 40;

	void Start () {
        canPlaySound = false;
        sliderVolume = GetComponent<Slider>();
        audioSourceComponent = GetComponent<AudioSource>();

        
        if(PlayerPrefs.HasKey(playerPrefName)) {
            vol = PlayerPrefs.GetFloat(playerPrefName); //Get the volume float from the playerprefs
        }
        else {
            PlayerPrefs.SetFloat(playerPrefName, 50); //Set the volume if the player pref not exist (first run)
        }

        sliderVolume.value = vol;
        canPlaySound = true;
    }
	
	void Update () {
        vol = sliderVolume.value;
        PlayerPrefs.SetFloat(playerPrefName, vol);
        text.SetText(Mathf.RoundToInt(vol * volumeScale) + " %");
        if (vol == 0) {
            mixer.SetFloat(mixerPropertyName, -80);
        }
        else {
            mixer.SetFloat(mixerPropertyName, (vol * minDb) - minDb);
        }
	}

    public void ValueChanged() {
        if (canPlaySound) {
            canPlaySound = false;
            audioSourceComponent.PlayOneShot(soundToPlay);
            Invoke("PlayTimer", soundToPlay.length);
        }

    }

    void PlayTimer() {
        canPlaySound = true;
    }
}
