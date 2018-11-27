using System.Collections;
using UnityEngine;
/// <summary>
/// Script to flicker the light
/// </summary>
public class Lampadaire : MonoBehaviour {
    [SerializeField] private GameObject lightLampadaire;
    private const float flickerSpeed = 0.05f;
    private const int chanceToFlicker = 60;

	void Start () {
        InvokeRepeating("flicker", 0, 3);
	}

    void flicker() {
        StartCoroutine("FlickerLight");
    }
	
	IEnumerator FlickerLight() {
        float RDMN = Random.Range(0.0f, 100.0f);
        if(RDMN > chanceToFlicker) { 
            lightLampadaire.SetActive(false);
            yield return new WaitForSeconds(flickerSpeed);
            lightLampadaire.SetActive(true);
        }
    }
}
