using System.Collections;
using UnityEngine;

/// <summary>
/// Script that control the first boss at his first stage. 
/// </summary>

public class BossBrasSmash : MonoBehaviour {
    [SerializeField] private GameObject dashZone, damageZone;
    [SerializeField] private GameObject lockVirtualCamera;
    [SerializeField] private GameObject fxPlayer;
    [SerializeField] private AudioClip hitSound;

    [Header("Wait time in sequence")]
    [SerializeField] private float waitTimeExitState = 1;
    [SerializeField] private float waitTimeIdleState = 1;
    [SerializeField] private float waitTimeSearchState = 1;
    [SerializeField] private float waitTimeIdle2State = 2;
    [SerializeField] private float waitTimeSearch2State = 1;
    [SerializeField] private float[] waitTimeSmashAction = { 0.3f, 1.5f, 1.7f };

    [Header("AnimationsName")]
    [SerializeField] private string animationCameraShake; //Name of the state in the animator
    [SerializeField] private string animationIdleLeft, animationIdleRight, animationSmashLeft, animationSmashRight;

    private GameObject player;
    private GameObject bossContainer;
    private Vector3 playerPos;
    private const float BossSpeed = 2;

    private AudioSource fxPlayerAudioSource;
    private Animator bossContainerAnimator;
    private Animator lockVirtualCameraAnimator;
    private DamageZone damageZoneScript;
    private Popup popupScript;

    private float posX, posY;
    private const float StartPosY = 6.6f;
    private const float ExitPosY  = 40;
    private const float StartPosX = 480; //The start value for the X position
    private bool armLeft;

    void Awake () {
        //Asign objects to variables
        bossContainer = transform.parent.gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        fxPlayerAudioSource = fxPlayer.GetComponent<AudioSource>();
        bossContainerAnimator = bossContainer.GetComponent<Animator>();
        lockVirtualCameraAnimator = lockVirtualCamera.GetComponent<Animator>();
        damageZoneScript = damageZone.GetComponent<DamageZone>();
	}

    private void OnEnable() {
        //Put the Boss in the Start Position
        posY = StartPosY;
        posX = StartPosX;

        //Start the Boss sequence loop
        InvokeState("Start");
    }

    void Update() {
        playerPos = player.transform.position; //Find the value of the Player positon

        transform.position = Vector2.MoveTowards(transform.position, new Vector3(posX, posY, transform.position.z), BossSpeed); //Move the object to the player position
    }


    public void InvokeState(string state) {
        StartCoroutine("States", state);
    }

    IEnumerator States(string state) {
        switch (state) {
            case "Start":
                InvokeState("Idle");
                break;

            case "Exit":
                //Go to the exit position and desactive itself
                posY = ExitPosY;
                yield return new WaitForSeconds(waitTimeExitState);
                gameObject.SetActive(false);
                break;

            case "Idle":
                bossContainerAnimator.Play(animationIdleLeft);
                //Wait some time
                yield return new WaitForSeconds(waitTimeIdleState);
                InvokeState("Search");
                break;

            case "Search":
                //Go to the player position
                posX = playerPos.x;
                yield return new WaitForSeconds(waitTimeSearchState);
                InvokeState("Idle2");
                break;

            case "Idle2":
                //Wait some time
                yield return new WaitForSeconds(waitTimeIdle2State);
                InvokeState("Search2");
                break;

            case "Search2":
                //Go to player position
                posX = playerPos.x;
                yield return new WaitForSeconds(waitTimeSearch2State);
                InvokeState("Smash");
                break;

            case "Smash":
                //Smash

                //Play the Smash sound
                fxPlayerAudioSource.clip = hitSound;
                fxPlayerAudioSource.Play();
                bossContainerAnimator.Play(animationSmashLeft);
                
                yield return new WaitForSeconds(waitTimeSmashAction[0]);

                //Shake the camera
                lockVirtualCameraAnimator.Play(animationCameraShake);

                //Enable the dash zone if the player is not in the damage zone
                if (!damageZoneScript.IsInZone) {
                    dashZone.SetActive(true);
                }

                //Apply damages to player
                damageZoneScript.ApplyDamages();
                yield return new WaitForSeconds(waitTimeSmashAction[1]);
                dashZone.SetActive(false);

                yield return new WaitForSeconds(waitTimeSmashAction[2]);
                //Restart the State Coroutine
                InvokeState("Idle");
                break;
        }
    }
}
