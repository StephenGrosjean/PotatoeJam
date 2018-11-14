using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script that control the first boss at his first stage. 
/// </summary>

public class BossBrasSmash : MonoBehaviour {
    [SerializeField] private GameObject DashZone, DamageZone;
    [SerializeField] private GameObject LockVirtualCamera;
    [SerializeField] private GameObject FXPlayer;
    [SerializeField] private AudioClip HitSound;

    [Header("Wait time in sequence")]
    [SerializeField] private float WaitTime_Exit_State = 1;
    [SerializeField] private float WaitTime_Idle_State = 1;
    [SerializeField] private float WaitTime_Search_State = 1;
    [SerializeField] private float WaitTime_Idle2_State = 2;
    [SerializeField] private float WaitTime_Search2_State = 1;
    [SerializeField] private float[] WaitTime_Smash_Action = { 0.3f, 1.5f, 1.7f };

    [Header("AnimationsName")]
    [SerializeField] private string Animation_CameraShake; //Name of the state in the animator
    [SerializeField] private string Animation_Idle_Left, Animation_Idle_Right, Animation_Smash_Left, Animation_Smash_Right;

    private GameObject Player;
    private GameObject BossContainer;
    private Vector3 PlayerPos;
    private const float BossSpeed = 2;

    private AudioSource FXPlayerAudioSource;
    private Animator BossContainerAnimator;
    private Animator LockVirtualCameraAnimator;
    private DamageZone DamageZoneScript;
    private Popup PopupScript;

    private float PosX, PosY;
    private const float StartPosY = 6.6f;
    private const float ExitPosY  = 40;
    private const float StartPosX = 480; //The start value for the X position
    private bool ArmLeft;

    void Awake () {
        //Asign objects to variables
        BossContainer = transform.parent.gameObject;
        Player = GameObject.FindGameObjectWithTag("Player");
        FXPlayerAudioSource = FXPlayer.GetComponent<AudioSource>();
        BossContainerAnimator = BossContainer.GetComponent<Animator>();
        LockVirtualCameraAnimator = LockVirtualCamera.GetComponent<Animator>();
        DamageZoneScript = DamageZone.GetComponent<DamageZone>();
	}

    private void OnEnable() {
        //Put the Boss in the Start Position
        PosY = StartPosY;
        PosX = StartPosX;

        //Start the Boss sequence loop
        StartCoroutine("States", "Start");
    }

    void Update() {
        PlayerPos = Player.transform.position; //Find the value of the Player positon

        transform.position = Vector2.MoveTowards(transform.position, new Vector3(PosX, PosY, transform.position.z), BossSpeed); //Move the object to the player position
    }

    public IEnumerator States(string State) {
        switch (State) {
            case "Start":
                StartCoroutine("States", "Idle");
                break;

            case "Exit":
                //Go to the exit position and desactive itself
                PosY = ExitPosY;
                yield return new WaitForSeconds(WaitTime_Exit_State);
                gameObject.SetActive(false);
                break;

            case "Idle":
                BossContainerAnimator.Play(Animation_Idle_Left);
                //Wait some time
                yield return new WaitForSeconds(WaitTime_Idle_State);
                StartCoroutine("States", "Search");
                break;

            case "Search":
                //Go to the player position
                PosX = PlayerPos.x;
                yield return new WaitForSeconds(WaitTime_Search_State);
                StartCoroutine("States", "Idle2");
                break;

            case "Idle2":
                //Wait some time
                yield return new WaitForSeconds(WaitTime_Idle2_State);
                StartCoroutine("States", "Search2");
                break;

            case "Search2":
                //Go to player position
                PosX = PlayerPos.x;
                yield return new WaitForSeconds(WaitTime_Search2_State);
                StartCoroutine("States", "Smash");
                break;

            case "Smash":
                //Smash

                //Play the Smash sound
                FXPlayerAudioSource.clip = HitSound;
                FXPlayerAudioSource.Play();
                BossContainerAnimator.Play(Animation_Smash_Left);
                
                yield return new WaitForSeconds(WaitTime_Smash_Action[0]);

                //Shake the camera
                LockVirtualCameraAnimator.Play(Animation_CameraShake);

                //Enable the dash zone if the player is not in the damage zone
                if (!DamageZoneScript.IsInZone) {
                    DashZone.SetActive(true);
                }

                //Apply damages to player
                DamageZoneScript.ApplyDamages();
                yield return new WaitForSeconds(WaitTime_Smash_Action[1]);
                DashZone.SetActive(false);

                yield return new WaitForSeconds(WaitTime_Smash_Action[2]);
                //Restart the State Coroutine
                StartCoroutine("States", "Idle");
                break;
        }
    }
}
