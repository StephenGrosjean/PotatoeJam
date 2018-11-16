using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Smash : MonoBehaviour {
    [SerializeField] private string smashKey;
    public string SmashKey
    {
        get { return smashKey; }
        set { smashKey = value; }
    }

    [SerializeField] private GameObject smashBall;
    [SerializeField] private Image smashSlider;
    [SerializeField] private Transform smashSpawnZone;
    [SerializeField] private float waitTimeSmash;
    [SerializeField] private float waitTimeCharge;
    [SerializeField] private float waitTimeSpawnBall;
    [SerializeField] private float timerSmash;
    [SerializeField] private float smashReloadTime;


    private AnimatorNames animatorNames;
    private PlayerMovement playerMovementScript;
    private float smashValue;
    private bool charge;
    private bool canSmash = true;


    void Start () {
        animatorNames = GetComponent<AnimatorNames>();
        playerMovementScript = GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {
        smashSlider.fillAmount = smashValue;

        if (SmashKey != "") {
            if (Input.GetKeyDown(SmashKey) && canSmash) {
                StartCoroutine("ChargeSmash");
            }
        }

        if (charge) {
            smashValue += Time.deltaTime / smashReloadTime;
        }
        else {
            smashValue = 1;
        }
	}

    IEnumerator Smashing() {
        playerMovementScript.enabled = false;
        animatorNames.PlayAnimations("Smash");
        yield return new WaitForSeconds(waitTimeSpawnBall);
        GameObject ball = Instantiate(smashBall, smashSpawnZone.position, Quaternion.identity);
        ball.GetComponent<SmashBall>().SetDirection((int)Mathf.Sign(transform.localScale.x));
        yield return new WaitForSeconds(waitTimeSmash);
        playerMovementScript.enabled = true;

    }


    IEnumerator ChargeSmash() {
        smashValue = 0;
        canSmash = false;
        charge = true;
        StartCoroutine("Smashing");
        yield return new WaitForSeconds(waitTimeCharge);
        canSmash = true;
        charge = false;
    }

}
