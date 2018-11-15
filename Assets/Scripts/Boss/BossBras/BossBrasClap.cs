using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBrasClap : MonoBehaviour {

    [SerializeField] private AudioClip  ClapSound;
    [SerializeField] private GameObject FXPlayer;
    [SerializeField] private GameObject DamageZone;
    [SerializeField] private GameObject FixedVirtualCamera;

    private float PosY;
    private const float ExitPosY = 75;
    private const float BossSpeed = 2;
    private Vector3 PlayerPos;
    private GameObject BossContainer;
    private GameObject Player;

    private Animator BossContainerAnimator;
    private Animator FixedVirtualCameraAnimator;
    private AudioSource FXPlayerAudioSource;
    private DamageZone DamageZoneScript;

    [Header ("Wait time in sequence")]
    [SerializeField] private float WaitTime_Exit_State = 1;
    [SerializeField] private float WaitTime_Idle_State = 1;
    [SerializeField] private float WaitTime_Search_State = 1;
    [SerializeField] private float WaitTime_Idle2_State = 2;
    [SerializeField] private float WaitTime_Search2_State = 1;
    [SerializeField] private float[] WaitTime_Clap_Action = { 0.4f, 3};

    [Header("Animation Names")]
    [SerializeField] private string Animation_Clap;
    [SerializeField] private string Animation_CameraShake;
    

    void Awake () {
        BossContainer = transform.parent.gameObject;
        Player = GameObject.FindGameObjectWithTag("Player");
        BossContainerAnimator = BossContainer.GetComponent<Animator>();
        FixedVirtualCameraAnimator = FixedVirtualCamera.GetComponent<Animator>();
        FXPlayerAudioSource = FXPlayer.GetComponent<AudioSource>();
        DamageZoneScript = DamageZone.GetComponent<DamageZone>();
	}

    private void OnEnable() {
        InvokeState("Idle");
    }

    public void InvokeState(string state) {
        StartCoroutine("States", state);
    }

    // Update is called once per frame
    void Update () {
        PlayerPos = Player.transform.position;
        transform.position = Vector2.MoveTowards(transform.position, new Vector3(transform.position.x, PosY, transform.position.z), BossSpeed);
    }

   IEnumerator States(string State) {
        switch (State) {

            case "Exit":
                //Go to Exit position
                PosY = ExitPosY;
                yield return new WaitForSeconds(WaitTime_Exit_State);
                gameObject.SetActive(false);
                break;

            case "Idle":
                //Wait some time
  
                yield return new WaitForSeconds(WaitTime_Idle_State);
                InvokeState("Search");
                break;

            case "Search":
                //Goto player position
                PosY = PlayerPos.y;
                yield return new WaitForSeconds(WaitTime_Search_State);
                InvokeState("Idle2");
                break;

            case "Idle2":
                //Wait some time
                yield return new WaitForSeconds(WaitTime_Idle2_State);
                InvokeState("Search2");
                break;

            case "Search2":
                //Goto player position
                PosY = PlayerPos.y;
                yield return new WaitForSeconds(WaitTime_Search2_State);
                InvokeState("Smash");
                break;

            case "Smash":
                //SMASH! (Clap.)
                BossContainerAnimator.Play(Animation_Clap);
                yield return new WaitForSeconds(WaitTime_Clap_Action[0]);

                //Play clap sound
                FXPlayerAudioSource.clip = ClapSound;
                FXPlayerAudioSource.Play();

                //Shake the camera
                FixedVirtualCameraAnimator.Play(Animation_CameraShake);

                //Apply Damages
                DamageZoneScript.ApplyDamages();

                yield return new WaitForSeconds(WaitTime_Clap_Action[1]);

                //Restart the sequence
                InvokeState("Idle");
                break;
        }
    }
}
