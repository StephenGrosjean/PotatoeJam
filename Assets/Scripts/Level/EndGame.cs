using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
/// <summary>
/// Script that control the final part of the game
/// </summary>
public class EndGame : MonoBehaviour {
    [SerializeField] private float maxHeight;
    [SerializeField] private GameObject whiteImage;
    [SerializeField] private GameObject player;
    [SerializeField] private AudioMixer mixer;

    private bool isOnTop;

    private SpriteRenderer whiteImageSpriteRenderer;
    private float height;

    private const int minVolume = -80;
    private float maxVolume;
    private float currentVolume;

    //Volume
    //Equation -> y = A+Bx+Cx^2
    private const float equationVolumeA = -0.126f; 
    private const float equationVolumeB = -0.009f;
    private const float equationVolumeC = -0.0002f;

    //Alpha
    //Equation -> y = A+Bx+Cx^2
    private const float equationAlphaA = -0.0074f;
    private const float equationAlphaB = -0.00013f;
    private const float equationAlphaC = 0.000005f;

    void Start () {
        mixer.GetFloat("MusicVolume", out maxVolume);
        whiteImageSpriteRenderer = whiteImage.GetComponent<SpriteRenderer>();

    }
	
	void Update () {
        height = player.transform.position.y;
        mixer.GetFloat("MusicVolume", out currentVolume);

        float alpha = equationAlphaA + (equationAlphaB * height) + (equationAlphaC * Mathf.Pow(height, 2));
        float equationVolume = equationVolumeA + (equationVolumeB * height) + (equationVolumeC * Mathf.Pow(height, 2));
        float finalVolume = Mathf.Clamp(equationVolume, minVolume, maxVolume); // Clamp the volume to it doesn't get past the user defined volume
 
        mixer.SetFloat("MusicVolume", finalVolume);


        whiteImageSpriteRenderer.color = new Color(1, 1, 1, alpha);
        

        if(!isOnTop && height >= maxHeight) {
            isOnTop = true;
            SceneManager.LoadScene("EndGame");
        }
	}
}
