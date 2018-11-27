using UnityEngine;
/// <summary>
/// play the poof sound
/// </summary>
public class PoofCloud : MonoBehaviour {
    [SerializeField] private AudioClip poofSound;

	void Awake () {
        GetComponent<AudioSource>().clip = poofSound;
        GetComponent<AudioSource>().Play();
	}
}
