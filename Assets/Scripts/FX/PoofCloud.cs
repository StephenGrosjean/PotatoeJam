using UnityEngine;

public class PoofCloud : MonoBehaviour {
    [SerializeField] private AudioClip poofSound;

	// Use this for initialization
	void Awake () {
        GetComponent<AudioSource>().clip = poofSound;
        GetComponent<AudioSource>().Play();
	}
}
