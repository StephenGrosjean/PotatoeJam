using UnityEngine;
/// <summary>
/// Force the screen resolution to 1920 x 1080
/// </summary>
public class ForceResolution : MonoBehaviour {
    [SerializeField] private int height = 1920;
    [SerializeField] private int width = 1080;
	// Use this for initialization
	void Start () {
        Screen.SetResolution(height, width, true);
	}
	
}
