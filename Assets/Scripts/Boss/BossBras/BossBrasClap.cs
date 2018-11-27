using System.Collections;
using UnityEngine;
/// <summary>
/// Script to control the Clap Phase of the first boss
/// </summary>
public class BossBrasClap : MonoBehaviour {

    [SerializeField] private AudioClip  clapSound;
    [SerializeField] private GameObject fxPlayer;
    [SerializeField] private GameObject damageZone;
    [SerializeField] private GameObject fixedVirtualCamera;

    private float posY;
    private const float ExitPosY = 75;
    private const float BossSpeed = 2;
    private Vector3 playerPos;
    private GameObject bossContainer;
    private GameObject player;

    private Animator bossContainerAnimator;
    private Animator fixedVirtualCameraAnimator;
    private AudioSource fxPlayerAudioSource;
    private DamageZone damageZoneScript;

    [Header ("Wait time in sequence")]
    [SerializeField] private float waitTimeExitState = 1;
    [SerializeField] private float waitTimeIdleState = 1;
    [SerializeField] private float waitTimeSearchState = 1;
    [SerializeField] private float waitTimeIdle2State = 2;
    [SerializeField] private float waitTimeSearch2State = 1;
    [SerializeField] private float[] waitTimeClapAction = { 0.4f, 3};

    [Header("Animation Names")]
    [SerializeField] private string animationClap;
    [SerializeField] private string animationCameraShake;
    

    void Awake () {
        bossContainer = transform.parent.gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        bossContainerAnimator = bossContainer.GetComponent<Animator>();
        fixedVirtualCameraAnimator = fixedVirtualCamera.GetComponent<Animator>();
        fxPlayerAudioSource = fxPlayer.GetComponent<AudioSource>();
        damageZoneScript = damageZone.GetComponent<DamageZone>();
	}

    private void OnEnable() {
        InvokeState("Idle");
    }

    public void InvokeState(string state) {
        StartCoroutine("States", state);
    }

    // Update is called once per frame
    void Update () {
        playerPos = player.transform.position;
        transform.position = Vector2.MoveTowards(transform.position, new Vector3(transform.position.x, posY, transform.position.z), BossSpeed);
    }

   IEnumerator States(string state) {
        switch (state) {

            case "Exit":
                //Go to Exit position
                posY = ExitPosY;
                yield return new WaitForSeconds(waitTimeExitState);
                gameObject.SetActive(false);
                break;

            case "Idle":
                //Wait some time
  
                yield return new WaitForSeconds(waitTimeIdleState);
                InvokeState("Search");
                break;

            case "Search":
                //Goto player position
                posY = playerPos.y;
                yield return new WaitForSeconds(waitTimeSearchState);
                InvokeState("Idle2");
                break;

            case "Idle2":
                //Wait some time
                yield return new WaitForSeconds(waitTimeIdle2State);
                InvokeState("Search2");
                break;

            case "Search2":
                //Goto player position
                posY = playerPos.y;
                yield return new WaitForSeconds(waitTimeSearch2State);
                InvokeState("Smash");
                break;

            case "Smash":
                //SMASH! (Clap.)
                bossContainerAnimator.Play(animationClap);
                yield return new WaitForSeconds(waitTimeClapAction[0]);

                //Play clap sound
                fxPlayerAudioSource.clip = clapSound;
                fxPlayerAudioSource.Play();

                //Shake the camera
                fixedVirtualCameraAnimator.Play(animationCameraShake);

                //Apply Damages
                damageZoneScript.ApplyDamages();

                yield return new WaitForSeconds(waitTimeClapAction[1]);

                //Restart the sequence
                InvokeState("Idle");
                break;
        }
    }
}
