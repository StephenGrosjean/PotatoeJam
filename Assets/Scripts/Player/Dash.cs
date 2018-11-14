using System.Collections;
using UnityEngine;
/// <summary>
/// Script that manage the Dash Physics
/// </summary>
public class Dash : MonoBehaviour {
    public string DashKey; //SET (yes)

    [SerializeField] private GameObject DashTrail;
    [SerializeField] private float DashSpeed;
    [SerializeField] private float DashTime;
    [SerializeField] private float StartDashTime;
    [SerializeField] private int Direction;
    [SerializeField] private float DashDamp;
    [SerializeField] private bool Dashing;
    [SerializeField] private bool Charging;


    private PlayerMovement PlayerMovementScript;
    private AnimatorNames AnimatorNames;
    private Rigidbody2D rb;


    void Start () {
        rb = GetComponent<Rigidbody2D>();
        AnimatorNames = GetComponent<AnimatorNames>();
        PlayerMovementScript = GetComponent<PlayerMovement>();
        DashTime = StartDashTime;
	}
	
	void Update () {
            if (Direction == 0) {
                if (DashKey != "") {
                    if (Input.GetKeyDown(DashKey)) {
                        StartCoroutine("DashChargeTime");
                        if (transform.localScale.x < 0) {
                            Direction = 1;
                        }
                        if (transform.localScale.x > 0) {
                            Direction = 2;
                        }
                    }
                }
            }
            else {
                if (DashTime <= 0) {
                    Direction = 0;
                    DashTime = StartDashTime;
                    if (!Dashing) {
                        AnimatorNames.PlayAnimations("Idle");
                    }


                    rb.velocity = Vector2.zero;
                }
                else if (!Charging && !Dashing) {

                    DashTime -= Time.deltaTime;

                    if (DashKey != "") {
                       if (Input.GetKey(DashKey)) {
                            AnimatorNames.PlayAnimations("Dash");

                            if (!Dashing) {
                                if (Direction == 1) {
                                    rb.AddForce(Vector2.left * DashSpeed, ForceMode2D.Impulse);
                               
                                }
                                else if (Direction == 2) {
                                    rb.AddForce(Vector2.right * DashSpeed, ForceMode2D.Impulse);
                                }

                                GetComponent<DashDamp>().StartDashing();
                                Direction = 0;
                                DashTime = StartDashTime;
                            }
                       }
                    }
                }
        }
	}

    IEnumerator DisablePlayerControl() {
        DashTrail.SetActive(true);
        PlayerMovementScript.enabled = false;
        yield return new WaitForSeconds(DashTime + 0.4f);
        DashTrail.SetActive(false);
        PlayerMovementScript.enabled = true;
    }

    IEnumerator DashChargeTime() {
        StartCoroutine("DisablePlayerControl");
        AnimatorNames.PlayAnimations("Charge");

        Charging = true;
        yield return new WaitForSeconds(0.35f);
        Charging = false;
        
    }
}
