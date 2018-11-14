using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossJambes : MonoBehaviour {

    [SerializeField] private Transform LeftPos;
    [SerializeField] private Transform RightPos;
    [SerializeField] private GameObject RightDamageZone, LeftDamageZone;
    [SerializeField] private GameObject HitCircle;
    [SerializeField] private GameObject DashZone;
    [SerializeField] private float Speed;
    [SerializeField] private float Wait_Idle, Wait_Walk, Wait_BeforeHit, Wait_Hit, Wait_AfterHit;
    [SerializeField] private float KickForce;

    private bool SideLeft;
    private Transform Target;
    private GameObject Player;

    private Animator AnimatorComponent;
    private DamageZone RightDamageZoneScript, LeftDamageZoneScript;
    private LifeSystem LifeSystemScript;
    private Rigidbody2D PlayerRigidbody;
    private PlayerMovement PlayerMovementScript;
    private DashZone DashZoneScript;

	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");

        AnimatorComponent = GetComponent<Animator>();
        RightDamageZoneScript = RightDamageZone.GetComponent<DamageZone>();
        LeftDamageZoneScript = LeftDamageZone.GetComponent<DamageZone>();
        LifeSystemScript = Player.GetComponent<LifeSystem>();
        PlayerRigidbody = Player.GetComponent<Rigidbody2D>();
        PlayerMovementScript = Player.GetComponent<PlayerMovement>();
        DashZoneScript = DashZone.GetComponent<DashZone>();

        StartCoroutine("States", "Start");
	}

	void Update () {
        if(Target != null) {
            transform.position = Vector2.MoveTowards(transform.position, new Vector3(Target.position.x, transform.position.y, Target.position.z), Speed);
        }
       
        if (SideLeft) {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if(!SideLeft) {
            transform.localScale = new Vector3(1, 1, 1);
        }

        
	}

    private void OnDisable() {
        HitCircle.SetActive(false);
    }

    void ApplyKickDamages() {
        if (SideLeft) {
            if (LeftDamageZoneScript.IsInZone) {
                LifeSystemScript.LowerLife();
                PlayerRigidbody.AddForce(Vector2.up * KickForce, ForceMode2D.Impulse);
            }
        }
        else {
            if (RightDamageZoneScript.IsInZone) {
                LifeSystemScript.LowerLife();
                PlayerRigidbody.AddForce(Vector2.up * KickForce, ForceMode2D.Impulse);
            }
        }
    }


    IEnumerator States(string state) {
        switch (state) {
            case "Start":
                Debug.Log("Start");
                yield return new WaitForSeconds(1);
                StartCoroutine("States", "Idle");
                break;

            case "Idle":
                Debug.Log("Idle");
                AnimatorComponent.Play("Idle");
                yield return new WaitForSeconds(Wait_Idle);
                HitCircle.SetActive(false);
                DashZone.SetActive(false);

                if (SideLeft) {
                    StartCoroutine("States", "WalkRight");

                }
                else {
                    StartCoroutine("States", "WalkLeft");
                }
                break;

            case "WalkLeft":
                Debug.Log("WalkLeft");
                Target = LeftPos;
                AnimatorComponent.Play("Walk");
                yield return new WaitForSeconds(Wait_Walk);
                SideLeft = true;
                StartCoroutine("States", "Wait");
                break;

            case "WalkRight":
                Debug.Log("WalkRight");
                Target = RightPos;
                AnimatorComponent.Play("Walk");
                yield return new WaitForSeconds(Wait_Walk);
                SideLeft = false;
                StartCoroutine("States", "Wait");
                break;

            case "Wait":
                AnimatorComponent.Play("Idle");
                yield return new WaitForSeconds(1);
                StartCoroutine("States", "Hit");
                break;

            case "Hit":
                Debug.Log("Hit");
                yield return new WaitForSeconds(Wait_BeforeHit);
                AnimatorComponent.Play("Kick");

                yield return new WaitForSeconds(Wait_Hit);
                ApplyKickDamages();
                DashZone.SetActive(true);
                HitCircle.SetActive(true);

                yield return new WaitForSeconds(Wait_AfterHit);
                StartCoroutine("States", "Idle");


                break;
        }
    }
}
