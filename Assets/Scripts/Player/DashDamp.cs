using System.Collections;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Script that control the value of the Dash slider and the boolean IsDashing
/// </summary>

public class DashDamp : MonoBehaviour {
    [SerializeField] private bool isDashing;
    public bool IsDashing
    {
        get { return isDashing; }
        set { isDashing = value; }
    }

    [SerializeField] private float waitTime = 1f;
    [SerializeField] private Image dashSlider;
    [SerializeField] private float dashValue;

    private Dash dashScript;

    void Start () {
        dashScript = GetComponent<Dash>();
	}

    void Update() {
        dashSlider.fillAmount = dashValue; //Set the slider value 

        //Check if the value is not at the maximum
        if (dashValue < 1){ 
            //Increase the value
            dashValue += Time.deltaTime;
        }
        else {
            //Limit the value to 1;
            dashValue = 1;
        }
       
    }

    public void StartDashing() {
        //Reset the value of the slider
        dashValue = 0;
        StartCoroutine("DashStart");
    }

    IEnumerator DashStart() {
        IsDashing = true;
        dashScript.enabled = false;

        yield return new WaitForSeconds(waitTime);
        dashValue = 1;

        IsDashing = false;
        dashScript.enabled = true;
        
    }
}
