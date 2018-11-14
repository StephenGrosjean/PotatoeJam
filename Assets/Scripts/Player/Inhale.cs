using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script that control the Inhale Power of the player
/// </summary>

public class Inhale : MonoBehaviour {

    public string InhaleKey; //GET;SET; (Maybe?)
    public bool DoInhale; //GET (yes)
    public Image InhaleSlider; //GET (yes)

    [SerializeField] private GameObject InhaleParticles;
    [SerializeField] private GameObject Effector;
    [SerializeField] private float TimerInhale;

    private float InhaleValue;
    private bool Charge;

    private bool CanInhale = true;
    private AnimatorNames AnimatorNames;
    private PlayerMovement PlayerMovementScript;

    void Start () {
        AnimatorNames = GetComponent<AnimatorNames>();
        PlayerMovementScript = GetComponent<PlayerMovement>();
    }

    void Update() {
        InhaleSlider.fillAmount = InhaleValue; //Update the value of the Inhale slider

        if (InhaleKey != "") {//Check if the Inhale key as been asigned
            if (Input.GetKeyDown(InhaleKey) && CanInhale) {
                DoInhale = true;
                AnimatorNames.PlayAnimations("Inhale"); //Play the animation
                StartCoroutine("InhaleChargeTime");//Start the charge time (slider)
            }
            if (Input.GetKeyUp(InhaleKey)) {
                DoInhale = false;
            }
        }

        if (Charge) {
            InhaleValue += Time.deltaTime/TimerInhale; //Increase the slider value;
        }
        else {
            InhaleValue = 1;
        }
	}

    //Disable the player control while inhaling
    IEnumerator DisablePlayerControl() {
        PlayerMovementScript.enabled = false;
        yield return new WaitForSeconds(1f);

        PlayerMovementScript.enabled = true;
    }

    //Enable/Disable the aspiration effect
    IEnumerator ToggleEffects() {
        Effector.SetActive(true);
        InhaleParticles.SetActive(true);
        yield return new WaitForSeconds(1f);
        Effector.SetActive(false);
        InhaleParticles.SetActive(false);

    }

    //Start the charge procedure
    IEnumerator InhaleChargeTime() {
        InhaleValue = 0;
        CanInhale = false;
        Charge = true;

        StartCoroutine("ToggleEffects");
        StartCoroutine("DisablePlayerControl");

        yield return new WaitForSeconds(TimerInhale);

        CanInhale = true;
        Charge = false;
    }
}
