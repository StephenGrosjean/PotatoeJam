using UnityEngine;
using Cinemachine;

/// <summary>
/// Game Manager script
/// </summary>

public class GameManager : MonoBehaviour {
    public bool BossArmsDead; //Get (yes)
    public bool BossLegsDead; //Get (yes)

    public int Currentcheckpoint;
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

    [Header("SmashPower")]
    [SerializeField] private GameObject uiSmash;

    private const float NormalMusicPitch = 1f;
    private const float BossMusicPitch = 1.1f;

    private const int CameraHighPrority = 10;
    private const int CameraLowPriority = 1;

    private const int VolumeScale = 15;

    private GameObject player;

    private Animator powerScreenAnimator;
    private AudioSource camAudioSource;
    private LifeSystem lifeSystemScript;
    private AnimatorNames animatorNamesScript;
    private Smash smashScript;
    private CinemachineVirtualCamera camCinemachineMainVirtualCamera, camCinemachineLock1VirtualCamera, camCinemachineLock2VirtualCamera;

    void Start () {
        Currentcheckpoint = PlayerPrefs.GetInt("CheckPoint");
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<LifeSystem>().ActiveCamera = camCinemachineMain;
        camAudioSource = cam.GetComponent<AudioSource>();
        lifeSystemScript = player.GetComponent<LifeSystem>();
        animatorNamesScript = player.GetComponent<AnimatorNames>();
        smashScript = player.GetComponent<Smash>();
        powerScreenAnimator = powerScreen.GetComponent<Animator>();
        camCinemachineMainVirtualCamera = camCinemachineMain.GetComponent<CinemachineVirtualCamera>();
        camCinemachineLock1VirtualCamera = camCinemachineLock1.GetComponent<CinemachineVirtualCamera>();
        camCinemachineLock2VirtualCamera = camCinemachineLock2.GetComponent<CinemachineVirtualCamera>();

        camAudioSource.volume = PlayerPrefs.GetFloat("Volume") / VolumeScale;

        if(PlayerPrefs.GetInt("CheckPoint") == 0) {
            player.transform.position = cp0.position;
        }
        else if(PlayerPrefs.GetInt("CheckPoint") == 1) {
            player.transform.position = cp1.position;
            UpgradePlayer(2);
            wallB11.SetActive(true);
            BossArmsDead = true;
            Destroy(boss1);
            camCinemachineMainVirtualCamera.Priority = CameraHighPrority;
            camCinemachineLock1VirtualCamera.Priority = CameraLowPriority;
        }
        else if (PlayerPrefs.GetInt("CheckPoint") == 2) {
            player.transform.position = cp2.position;
            wallB21.SetActive(true);
            BossArmsDead = true;
            BossLegsDead = true;
            Destroy(boss1);
            Destroy(boss2);
            camCinemachineMainVirtualCamera.Priority = CameraHighPrority;
            camCinemachineLock1VirtualCamera.Priority = CameraLowPriority;
            camCinemachineLock2VirtualCamera.Priority = CameraLowPriority;
        }
    }

    //Save the check point in the player prefs
    public void Save(int checkPoint) {
        PlayerPrefs.SetInt("CheckPoint", checkPoint);
        Debug.Log("GameSaved: " + PlayerPrefs.GetInt("CheckPoint"));
    }

    void Update () {
        //If the camera as passed the boss 1 room center
		if(cam.transform.position.x >= limitBoss1 && !BossArmsDead && !BossLegsDead) {
            
            camAudioSource.pitch = BossMusicPitch;
            lifeSystemScript.ActiveCamera = camCinemachineLock1;

            //Change Virtual Camera
            camCinemachineMainVirtualCamera.Priority = CameraLowPriority;
            camCinemachineLock1VirtualCamera.Priority = CameraHighPrority;

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
            Save(1); //Set the checkpoint 
            powerScreenAnimator.Play("PowerScreen");
            popUpFirstBoss.SetActive(true);

            UpgradePlayer(2);

            camAudioSource.pitch = NormalMusicPitch;
            lifeSystemScript.ActiveCamera = camCinemachineMain;

            //Change Virtual Camera
            camCinemachineMainVirtualCamera.Priority = CameraHighPrority;
            camCinemachineLock1VirtualCamera.Priority = CameraLowPriority;

            //Delete only the wall that prevent the player continue the game
            Destroy(wallB12);

            //Disable Hearts UI of the Boss
            foreach (GameObject heart in lifeBoss1) {
                heart.SetActive(false);
            }
        }

        //Check if camera as passed the boss 2 room center, the first boss is dead and the second not
        if(cam.transform.position.x >= limitBoss2 && BossArmsDead && !BossLegsDead) {
            camAudioSource.pitch = BossMusicPitch;
            lifeSystemScript.ActiveCamera = camCinemachineLock2;

            //Change Virtual Camera
            camCinemachineMainVirtualCamera.Priority = CameraLowPriority;
            camCinemachineLock2VirtualCamera.Priority = CameraHighPrority;

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
            Save(2); //Set the checkpoint 

            camAudioSource.pitch = NormalMusicPitch;
            lifeSystemScript.ActiveCamera = camCinemachineMain;

            //Change Virtual Camera
            camCinemachineMainVirtualCamera.Priority = CameraHighPrority;
            camCinemachineLock2VirtualCamera.Priority = CameraLowPriority;

            //Delete only the wall that prevent the player continue the game
            Destroy(wallB22);

            //Disable Hearts UI of the Boss
            foreach (GameObject heart in lifeBoss2) {
                heart.SetActive(false);
            }
        }

        //Enable pause menu
        if (Input.GetKeyDown(KeyCode.P)) {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }

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
                break;
        }
    }
}
