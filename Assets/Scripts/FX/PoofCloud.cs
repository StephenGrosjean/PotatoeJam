using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoofCloud : MonoBehaviour {
    [SerializeField] private AudioClip poofSound;

	// Use this for initialization
	void Awake () {
        GetComponent<AudioSource>().clip = poofSound;
        GetComponent<AudioSource>().Play();
	}
}
