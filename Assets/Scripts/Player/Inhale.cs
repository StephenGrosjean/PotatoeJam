using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script that control the Inhale Power of the player
/// </summary>

public class Inhale : MonoBehaviour {

    [SerializeField] private string inhaleKey;
    public string InhaleKey
    {
        get { return inhaleKey; }
        set { inhaleKey = value; }
    }

    [SerializeField] private bool doInhale;
    public bool DoInhale
    {
        get { return doInhale; }
        set { doInhale = value; }
    }

    [SerializeField]  private Image inhaleSlider;
    public Image InhaleSlider
    {
        get { return inhaleSlider; }
        set { inhaleSlider = value; }

    }

    [SerializeField] private GameObject inhaleParticles;
    [SerializeField] private GameObject effector;
    [SerializeField] private float timerInhale;

    private float inhaleValue;
    private bool charge;

    private bool canInhale = true;
    private AnimatorNames animatorNames;
    private PlayerMovement playerMovementScript;

    void Start () {
        animatorNames = GetComponent<AnimatorNames>();
        playerMovementScript = GetComponent<PlayerMovement>();
    }

    void Update() {
        InhaleSlider.fillAmount = inhaleValue; //Update the value of the Inhale slider

        if (InhaleKey != "") {//Check if the Inhale key as been asigned
            if (Input.GetKeyDown(InhaleKey) && canInhale) {
                DoInhale = true;
                animatorNames.PlayAnimations("Inhale"); //Play the animation
                StartCoroutine("InhaleChargeTime");//Start the charge time (slider)
            }
            if (Input.GetKeyUp(InhaleKey)) {
                DoInhale = false;
            }
        }

        if (charge) {
            inhaleValue += Time.deltaTime/timerInhale; //Increase the slider value;
        }
        else {
            inhaleValue = 1;
        }
	}

    //Disable the player control while inhaling
    IEnumerator DisablePlayerControl() {
        playerMovementScript.enabled = false;
        yield return new WaitForSeconds(1f);

        playerMovementScript.enabled = true;
    }

    //Enable/Disable the aspiration effect
    IEnumerator ToggleEffects() {
        effector.SetActive(true);
        inhaleParticles.SetActive(true);
        yield return new WaitForSeconds(1f);
        effector.SetActive(false);
        inhaleParticles.SetActive(false);

    }

    //Start the charge procedure
    IEnumerator InhaleChargeTime() {
        inhaleValue = 0;
        canInhale = false;
        charge = true;

        StartCoroutine("ToggleEffects");
        StartCoroutine("DisablePlayerControl");

        yield return new WaitForSeconds(timerInhale);

        canInhale = true;
        charge = false;
    }
}
