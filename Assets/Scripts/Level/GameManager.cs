﻿using UnityEngine;
using Cinemachine;
using UnityEngine.Audio;

/// <summary>
/// Game Manager script
/// </summary>

public class GameManager : MonoBehaviour {
    [SerializeField] private bool bossArmsDead;
    public bool BossArmsDead
    {
        get { return bossArmsDead; }
        set { bossArmsDead = value; }
    }

    [SerializeField] private bool bossLegsDead;
    public bool BossLegsDead
    {
        get { return bossLegsDead; }
        set { bossLegsDead = value; }
    }

    [SerializeField] private GameObject background, trees;
    [SerializeField] private int Currentcheckpoint;
    [SerializeField] private GameObject powerScreen;
    [SerializeField] private GameObject popUpFirstBoss, popUpSecondBoss;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private float limitBoss1, limitBoss2;
    [SerializeField] private Transform cp0, cp1, cp2;
    [SerializeField] private GameObject boss1, boss2;
    [SerializeField] private GameObject cam;
    [SerializeField] private GameObject camCinemachineMain, camCinemachineLock1, camCinemachineLock2;
    [SerializeField] private GameObject wallB11, wallB12, wallB21, wallB22;
    [SerializeField] private GameObject[] lifeBoss1;
    [SerializeField] private GameObject[] lifeBoss2;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Transform legsUpgradePosition;

    [Header("SmashPower")]
    [SerializeField] private GameObject uiSmash;

    [Header("Legs Sequence")]
    [SerializeField] private GameObject body;
    [SerializeField] private GameObject legs;
    [SerializeField] private Transform groundCheckNewPos;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private GameObject powerPanel;



    private bool isXboxControls;

    private const float normalMusicPitch = 1f;
    private const float bossMusicPitch = 1.1f;

    private const int cameraHighPrority = 10;
    private const int cameraLowPriority = 1;

    private const int volumeScale = 15;

    private GameObject player;

    private Animator powerScreenAnimator;
    private AudioSource camAudioSource;
    private LifeSystem lifeSystemScript;
    private AnimatorNames animatorNamesScript;
    private Smash smashScript;
    private CinemachineVirtualCamera camCinemachineMainVirtualCamera, camCinemachineLock1VirtualCamera, camCinemachineLock2VirtualCamera;
    private GameObject inputManager;
    private InputManager inputManagerScript;
    private EndGame endGameScript;
    private Parallax backgroundParallax, treesParallax;


    void Start () {
        Currentcheckpoint = PlayerPrefs.GetInt("CheckPoint");
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<LifeSystem>().ActiveCamera = camCinemachineMain;
        camAudioSource = cam.GetComponent<AudioSource>();
        lifeSystemScript = player.GetComponent<LifeSystem>();
        animatorNamesScript = player.GetComponent<AnimatorNames>();
        smashScript = player.GetComponent<Smash>();
        powerScreenAnimator = powerScreen.GetComponent<Animator>();
        inputManager = GameObject.Find("InputManager");
        inputManagerScript = inputManager.GetComponent<InputManager>();
        camCinemachineMainVirtualCamera = camCinemachineMain.GetComponent<CinemachineVirtualCamera>();
        camCinemachineLock1VirtualCamera = camCinemachineLock1.GetComponent<CinemachineVirtualCamera>();
        camCinemachineLock2VirtualCamera = camCinemachineLock2.GetComponent<CinemachineVirtualCamera>();
        endGameScript = GetComponent<EndGame>();
        backgroundParallax = background.GetComponent<Parallax>();
        treesParallax = trees.GetComponent<Parallax>();

        mixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("Volume") / volumeScale);

        if(PlayerPrefs.GetInt("CheckPoint") == 0) {
            player.transform.position = cp0.position;
        }
        else if(PlayerPrefs.GetInt("CheckPoint") == 1) {
            player.transform.position = cp1.position;
            UpgradePlayer(2);
            wallB11.SetActive(true);
            BossArmsDead = true;
            Destroy(boss1);
            camCinemachineMainVirtualCamera.Priority = cameraHighPrority;
            camCinemachineLock1VirtualCamera.Priority = cameraLowPriority;
        }
        else if (PlayerPrefs.GetInt("CheckPoint") == 2) {
            UpgradePlayer(3);
            player.transform.position = cp2.position;
            wallB21.SetActive(true);
            BossArmsDead = true;
            BossLegsDead = true;
            endGameScript.enabled = true;
            Destroy(boss1);
            Destroy(boss2);
            camCinemachineMainVirtualCamera.Priority = cameraHighPrority;
            camCinemachineLock1VirtualCamera.Priority = cameraLowPriority;
            camCinemachineLock2VirtualCamera.Priority = cameraLowPriority;
            EnableLegs();
        }
    }

    //Save the check point in the player prefs
    public void Save(int checkPoint) {
        PlayerPrefs.SetInt("CheckPoint", checkPoint);
        Debug.Log("GameSaved: " + PlayerPrefs.GetInt("CheckPoint"));
    }

    void Update () {
        isXboxControls = inputManagerScript.IsXboxControls;

        //If the camera as passed the boss 1 room center
        if (cam.transform.position.x >= limitBoss1 && !BossArmsDead && !BossLegsDead) {
            treesParallax.enabled = false;
            backgroundParallax.enabled = false;
            mixer.SetFloat("MusicPitch", bossMusicPitch);
            lifeSystemScript.ActiveCamera = camCinemachineLock1;

            //Change Virtual Camera
            camCinemachineMainVirtualCamera.Priority = cameraLowPriority;
            camCinemachineLock1VirtualCamera.Priority = cameraHighPrority;

            //Enable the boss and the room walls
            boss1.SetActive(true);
            wallB11.SetActive(true);
            wallB12.SetActive(true);

            //Enable Heart UI of the boss
            foreach (GameObject heart in lifeBoss1) {
                heart.SetActive(true);
            }
        }
        //Check if the Boss 1 is dead
        else if(BossArmsDead && PlayerPrefs.GetInt("CheckPoint") == 0) {
            treesParallax.enabled = true;
            backgroundParallax.enabled = true;
            Save(1); //Set the checkpoint 
            powerScreenAnimator.Play("PowerScreen");
            popUpFirstBoss.SetActive(true);

            UpgradePlayer(2);
            mixer.SetFloat("MusicPitch", normalMusicPitch);
            lifeSystemScript.ActiveCamera = camCinemachineMain;

            //Change Virtual Camera
            camCinemachineMainVirtualCamera.Priority = cameraHighPrority;
            camCinemachineLock1VirtualCamera.Priority = cameraLowPriority;

            //Delete only the wall that prevent the player continue the game
            Destroy(wallB12);

            //Disable Hearts UI of the Boss
            foreach (GameObject heart in lifeBoss1) {
                heart.SetActive(false);
            }
        }

        //Check if camera as passed the boss 2 room center, the first boss is dead and the second not
        if(cam.transform.position.x >= limitBoss2 && BossArmsDead && !BossLegsDead) {
            treesParallax.enabled = false;
            backgroundParallax.enabled = false;
            mixer.SetFloat("MusicPitch", bossMusicPitch);
            lifeSystemScript.ActiveCamera = camCinemachineLock2;

            //Change Virtual Camera
            camCinemachineMainVirtualCamera.Priority = cameraLowPriority;
            camCinemachineLock2VirtualCamera.Priority = cameraHighPrority;

            //Enable the boss and the room walls
            if(boss2 != null) {
                boss2.SetActive(true);
            }
            wallB21.SetActive(true);
            wallB22.SetActive(true);

            //Enable Heart UI of the boss
            foreach (GameObject heart in lifeBoss2) {
                heart.SetActive(true);
            }
        }
        //Check if the checkpoint is 1, the first boss and the second are dead
        else if (BossLegsDead && PlayerPrefs.GetInt("CheckPoint") == 1 && BossArmsDead) {
            treesParallax.enabled = true;
            backgroundParallax.enabled = true;
            Save(2); //Set the checkpoint 
            UpgradePlayer(3);
            EnableLegs();
            endGameScript.enabled = true;

            mixer.SetFloat("MusicPitch", normalMusicPitch);
            lifeSystemScript.ActiveCamera = camCinemachineMain;

            //Change Virtual Camera
            camCinemachineMainVirtualCamera.Priority = cameraHighPrority;
            camCinemachineLock2VirtualCamera.Priority = cameraLowPriority;

            //Delete only the wall that prevent the player continue the game
            Destroy(wallB22);

            //Disable Hearts UI of the Boss
            foreach (GameObject heart in lifeBoss2) {
                heart.SetActive(false);
            }
        }

        //Enable pause menu
        if (isXboxControls) {
            if (Input.GetButtonDown("X360_Pause")) {
                if (pauseMenu.activeSelf) {
                    pauseMenu.SetActive(false);
                    Time.timeScale = 1;
                }
                else {
                    pauseMenu.SetActive(true);
                    Time.timeScale = 0;
                }
            }
        }
        else {
            if (Input.GetKeyDown(KeyCode.P)) {
                if (pauseMenu.activeSelf) {
                    pauseMenu.SetActive(false);
                    Time.timeScale = 1;
                }
                else {
                    pauseMenu.SetActive(true);
                    Time.timeScale = 0;
                }
            }
        }

        Cursor.visible = (Time.timeScale == 0);

        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
	}

    void UpgradePlayer(int stage) {
        switch (stage) {
            case 2:
                animatorNamesScript.Stages = 2; //Get the arm Upgrade
                uiSmash.SetActive(true); //Enable the smash bar
                smashScript.enabled = true; //Enable the smash power
                break;
            case 3:
                animatorNamesScript.Stages = 3; //Get the arm Upgrade
                break;
        }
    }

    void EnableLegs() {
        player.transform.position = legsUpgradePosition.position;
        legs.SetActive(true);
        body.SetActive(false);
        player.GetComponent<BoxCollider2D>().enabled = true;
        player.GetComponent<BoxCollider2D>().enabled = true;
        groundCheck.transform.position = groundCheckNewPos.position;
        powerPanel.SetActive(false);
        player.GetComponent<Dash>().enabled = false;
        player.GetComponent<Inhale>().enabled = false;
        player.GetComponent<DashDamp>().enabled = false;
        player.GetComponent<Smash>().enabled = false;
        player.GetComponent<PlayerMovement>().JumpForce *= 2;
    }
}
