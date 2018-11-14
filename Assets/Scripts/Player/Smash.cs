using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Smash : MonoBehaviour {
    public string SmashKey; //SET (yes)

    [SerializeField] private GameObject SmashBall;
    [SerializeField] private Image SmashSlider;
    [SerializeField] private Transform SmashSpawnZone;
    [SerializeField] private float WaitTimeSmash;
    [SerializeField] private float WaitTimeCharge;
    [SerializeField] private float WaitTimeSpawnBall;
    [SerializeField] private float TimerSmash;
    [SerializeField] private float SmashReloadTime;


    private AnimatorNames AnimatorNames;
    private PlayerMovement PlayerMovementScript;
    private float SmashValue;
    private bool Charge;
    private bool CanSmash = true;


    void Start () {
        AnimatorNames = GetComponent<AnimatorNames>();
        PlayerMovementScript = GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {
        SmashSlider.fillAmount = SmashValue;

        if (SmashKey != "") {
            if (Input.GetKeyDown(SmashKey) && CanSmash) {
                StartCoroutine("ChargeSmash");
            }
        }

        if (Charge) {
            SmashValue += Time.deltaTime / SmashReloadTime;
        }
        else {
            SmashValue = 1;
        }
	}

    IEnumerator Smashing() {
        PlayerMovementScript.enabled = false;
        AnimatorNames.PlayAnimations("Smash");
        yield return new WaitForSeconds(WaitTimeSpawnBall);
        GameObject Ball = Instantiate(SmashBall, SmashSpawnZone.position, Quaternion.identity);
        Ball.GetComponent<SmashBall>().SetDirection((int)Mathf.Sign(transform.localScale.x));
        yield return new WaitForSeconds(WaitTimeSmash);
        PlayerMovementScript.enabled = true;

    }


    IEnumerator ChargeSmash() {
        SmashValue = 0;
        CanSmash = false;
        Charge = true;
        StartCoroutine("Smashing");
        yield return new WaitForSeconds(WaitTimeCharge);
        CanSmash = true;
        Charge = false;
    }

}
