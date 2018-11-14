using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoofCloud : MonoBehaviour {
    [SerializeField] private AudioClip PoofSound;

	// Use this for initialization
	void Awake () {
        GetComponent<AudioSource>().clip = PoofSound;
        GetComponent<AudioSource>().Play();
	}
}
