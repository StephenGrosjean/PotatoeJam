using System.Collections;
using UnityEngine;
/// <summary>
/// Script that manage the Dash Physics
/// </summary>
public class Dash : MonoBehaviour {
   [SerializeField] private string dashKey;

    public string DashKey
    {
        get { return dashKey; }
        set { dashKey = value; }
    }

    [SerializeField] private GameObject dashTrail;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;
    [SerializeField] private float startDashTime;
    [SerializeField] private int direction;
    [SerializeField] private float dashDamp;
    [SerializeField] private bool dashing;
    [SerializeField] private bool charging;


    private PlayerMovement playerMovementScript;
    private AnimatorNames animatorNames;
    private Rigidbody2D rb;


    void Start () {
        rb = GetComponent<Rigidbody2D>();
        animatorNames = GetComponent<AnimatorNames>();
        playerMovementScript = GetComponent<PlayerMovement>();
        dashTime = startDashTime;
	}
	
	void Update () {
            if (direction == 0) {
                if (DashKey != "") {
                    if (Input.GetKeyDown(DashKey)) {
                        StartCoroutine("DashChargeTime");
                        if (transform.localScale.x < 0) {
                            direction = 1;
                        }
                        if (transform.localScale.x > 0) {
                            direction = 2;
                        }
                    }
                }
            }
            else {
                if (dashTime <= 0) {
                    direction = 0;
                    dashTime = startDashTime;
                    if (!dashing) {
                        animatorNames.PlayAnimations("Idle");
                    }


                    rb.velocity = Vector2.zero;
                }
                else if (!charging && !dashing) {

                    dashTime -= Time.deltaTime;

                    if (DashKey != "") {
                       if (Input.GetKey(DashKey)) {
                            animatorNames.PlayAnimations("Dash");

                            if (!dashing) {
                                if (direction == 1) {
                                    rb.AddForce(Vector2.left * dashSpeed, ForceMode2D.Impulse);
                               
                                }
                                else if (direction == 2) {
                                    rb.AddForce(Vector2.right * dashSpeed, ForceMode2D.Impulse);
                                }

                                GetComponent<DashDamp>().StartDashing();
                                direction = 0;
                                dashTime = startDashTime;
                            }
                       }
                    }
                }
        }
	}

    IEnumerator DisablePlayerControl() {
        dashTrail.SetActive(true);
        playerMovementScript.enabled = false;
        yield return new WaitForSeconds(dashTime + 0.4f);
        dashTrail.SetActive(false);
        playerMovementScript.enabled = true;
    }

    IEnumerator DashChargeTime() {
        StartCoroutine("DisablePlayerControl");
        animatorNames.PlayAnimations("Charge");

        charging = true;
        yield return new WaitForSeconds(0.35f);
        charging = false;
        
    }
}
