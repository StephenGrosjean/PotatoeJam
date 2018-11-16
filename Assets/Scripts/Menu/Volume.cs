using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Script to manage volume 
/// </summary>

public class Volume : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI text;

    private float vol;
    private const int VolumeScale = 100;

    private Slider sliderVolume;


	void Start () {
        sliderVolume = GetComponent<Slider>();

        vol = PlayerPrefs.GetFloat("Volume"); //Get the volume float from the playerprefs
        sliderVolume.value = vol;
	}
	
	void Update () {
        vol = sliderVolume.value;
        PlayerPrefs.SetFloat("Volume", vol);
        text.SetText(Mathf.RoundToInt(vol * VolumeScale) + " %");

	}
}
