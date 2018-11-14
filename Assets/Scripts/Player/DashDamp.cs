using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Script that controle the value of the Dash slider and the boolean IsDashing
/// </summary>

public class DashDamp : MonoBehaviour {
    public bool IsDashing;

    [SerializeField] private float WaitTime = 1f;
    [SerializeField] private Image DashSlider;
    [SerializeField] private float DashValue;

    private Dash DashScript;

    void Start () {
        DashScript = GetComponent<Dash>();
	}

    void Update() {
        DashSlider.fillAmount = DashValue; //Set the slider value 

        //Check if the value is not at the maximum
        if (DashValue < 1){ 
            //Increase the value
            DashValue += Time.deltaTime;
        }
        else {
            //Limit the value to 1;
            DashValue = 1;
        }
       
    }

    public void StartDashing() {
        //Reset the value of the slider
        DashValue = 0;
        StartCoroutine("DashStart");
    }

    IEnumerator DashStart() {
        IsDashing = true;
        DashScript.enabled = false;

        yield return new WaitForSeconds(WaitTime);
        DashValue = 1;

        IsDashing = false;
        DashScript.enabled = true;
        
    }
}
