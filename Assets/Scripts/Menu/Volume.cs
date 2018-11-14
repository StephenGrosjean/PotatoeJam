using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Script to manage volume 
/// </summary>

public class Volume : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI Text;

    private float Vol;
    private const int VolumeScale = 100;

    private Slider SliderVolume;


	void Start () {
        SliderVolume = GetComponent<Slider>();

        Vol = PlayerPrefs.GetFloat("Volume"); //Get the volume float from the playerprefs
        SliderVolume.value = Vol;
	}
	
	void Update () {
        Vol = SliderVolume.value;
        PlayerPrefs.SetFloat("Volume", Vol);
        Text.SetText(Mathf.RoundToInt(Vol * VolumeScale) + " %");

	}
}
