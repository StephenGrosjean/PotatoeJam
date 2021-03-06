﻿using System.Collections;
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
    [SerializeField] private AudioClip dashSound;

    private bool isXboxControls;
    private PlayerMovement playerMovementScript;
    private AnimatorNames animatorNames;
    private Rigidbody2D rb;
    private GameObject inputManager;
    private InputManager inputManagerScript;
    private DashDamp dashDampScript;
    private AudioSource audioSourceComponent;


    void Start () {
        inputManager = GameObject.Find("InputManager");
        inputManagerScript = inputManager.GetComponent<InputManager>();
        rb = GetComponent<Rigidbody2D>();
        animatorNames = GetComponent<AnimatorNames>();
        playerMovementScript = GetComponent<PlayerMovement>();
        dashDampScript = GetComponent<DashDamp>();
        audioSourceComponent = GetComponent<AudioSource>();
        dashTime = startDashTime;
	}
	
	void Update () {
        isXboxControls = inputManagerScript.IsXboxControls;

        if (direction == 0) {
                if (DashKey != "") {
                    if (isXboxControls) {
                        if (Input.GetButtonDown("X360_Dash")) {
                            StartCoroutine("DashChargeTime");
                            if (transform.localScale.x < 0) {
                                direction = 1;
                            }
                            if (transform.localScale.x > 0) {
                                direction = 2;
                            }
                        }
                    }
                    else {
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
                    if (isXboxControls) {
                        if (Input.GetButton("X360_Dash")) {
                            animatorNames.PlayAnimations("Dash");
                            audioSourceComponent.PlayOneShot(dashSound);

                            if (!dashing) {
                                if (direction == 1) {
                                    rb.AddForce(Vector2.left * dashSpeed, ForceMode2D.Impulse);

                                }
                                else if (direction == 2) {
                                    rb.AddForce(Vector2.right * dashSpeed, ForceMode2D.Impulse);
                                }

                                dashDampScript.StartDashing();
                                direction = 0;
                                dashTime = startDashTime;
                            }
                        }
                    }
                    else {
                        if (Input.GetKey(DashKey)) {
                            animatorNames.PlayAnimations("Dash");
                            audioSourceComponent.PlayOneShot(dashSound);

                            if (!dashing) {
                                if (direction == 1) {
                                    rb.AddForce(Vector2.left * dashSpeed, ForceMode2D.Impulse);
                                }
                                else if (direction == 2) {
                                    rb.AddForce(Vector2.right * dashSpeed, ForceMode2D.Impulse);
                                }

                                dashDampScript.StartDashing();
                                direction = 0;
                                dashTime = startDashTime;
                            }
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
