using UnityEngine;
using Cinemachine;

/// <summary>
/// Game Manager script
/// </summary>

public class GameManager : MonoBehaviour {
    public bool BossArmsDead; //Get (yes)
    public bool BossLegsDead; //Get (yes)

    public int currentcheckpoint;
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private float LimitBoss1, LimitBoss2;
    [SerializeField] private Transform CP0, CP1, CP2;
    [SerializeField] private GameObject Boss1, Boss2;
    [SerializeField] private GameObject Cam;
    [SerializeField] private GameObject CamCinemachineMain, CamCinemachineLock1, CamCinemachineLock2;
    [SerializeField] private GameObject WallB1_1, WallB1_2, WallB2_1, WallB2_2;
    [SerializeField] private GameObject[] LifeBoss1;
    [SerializeField] private GameObject[] LifeBoss2;
    [Header("SmashPower")]
    [SerializeField] private GameObject UISmash;

    private const float NormalMusicPitch = 1f;
    private const float BossMusicPitch = 1.1f;

    private const int CameraHighPrority = 10;
    private const int CameraLowPriority = 1;

    private const int VolumeScale = 15;

    private GameObject Player;
    
    private AudioSource CamAudioSource;
    private LifeSystem LifeSystemScript;
    private AnimatorNames AnimatorNamesScript;
    private Smash SmashScript;
    private CinemachineVirtualCamera CamCinemachineMain_VirtualCamera, CamCinemachineLock1_VirtualCamera, CamCinemachineLock2_VirtualCamera;

    void Start () {
        currentcheckpoint = PlayerPrefs.GetInt("CheckPoint");
        Player = GameObject.FindGameObjectWithTag("Player");
        Player.GetComponent<LifeSystem>().ActiveCamera = CamCinemachineMain;
        CamAudioSource = Cam.GetComponent<AudioSource>();
        LifeSystemScript = Player.GetComponent<LifeSystem>();
        AnimatorNamesScript = Player.GetComponent<AnimatorNames>();
        SmashScript = Player.GetComponent<Smash>();
        CamCinemachineMain_VirtualCamera = CamCinemachineMain.GetComponent<CinemachineVirtualCamera>();
        CamCinemachineLock1_VirtualCamera = CamCinemachineLock1.GetComponent<CinemachineVirtualCamera>();
        CamCinemachineLock2_VirtualCamera = CamCinemachineLock2.GetComponent<CinemachineVirtualCamera>();

        CamAudioSource.volume = PlayerPrefs.GetFloat("Volume") / VolumeScale;

        if(PlayerPrefs.GetInt("CheckPoint") == 0) {
            Player.transform.position = CP0.position;
        }
        else if(PlayerPrefs.GetInt("CheckPoint") == 1) {
            Player.transform.position = CP1.position;
            UpgradePlayer(2);
            WallB1_1.SetActive(true);
            BossArmsDead = true;
            Destroy(Boss1);
            CamCinemachineMain_VirtualCamera.Priority = CameraHighPrority;
            CamCinemachineLock1_VirtualCamera.Priority = CameraLowPriority;
        }
        else if (PlayerPrefs.GetInt("CheckPoint") == 2) {
            Player.transform.position = CP2.position;
            WallB2_1.SetActive(true);
            BossArmsDead = true;
            BossLegsDead = true;
            Destroy(Boss1);
            Destroy(Boss2);
            CamCinemachineMain_VirtualCamera.Priority = CameraHighPrority;
            CamCinemachineLock1_VirtualCamera.Priority = CameraLowPriority;
            CamCinemachineLock2_VirtualCamera.Priority = CameraLowPriority;
        }
    }

    //Save the check point in the player prefs
    public void Save(int CheckPoint) {
        PlayerPrefs.SetInt("CheckPoint", CheckPoint);
        Debug.Log("GameSaved: " + PlayerPrefs.GetInt("CheckPoint"));
    }

    void Update () {
        //If the camera as passed the boss 1 room center
		if(Cam.transform.position.x >= LimitBoss1 && !BossArmsDead && !BossLegsDead) {
            
            CamAudioSource.pitch = BossMusicPitch;
            LifeSystemScript.ActiveCamera = CamCinemachineLock1;

            //Change Virtual Camera
            CamCinemachineMain_VirtualCamera.Priority = CameraLowPriority;
            CamCinemachineLock1_VirtualCamera.Priority = CameraHighPrority;

            //Enable the boss and the room walls
            Boss1.SetActive(true);
            WallB1_1.SetActive(true);
            WallB1_2.SetActive(true);

            //Enable Heart UI of the boss
            foreach (GameObject Heart in LifeBoss1) {
                Heart.SetActive(true);
            }
        }
        //Check if the Boss 1 is dead
        else if(BossArmsDead && PlayerPrefs.GetInt("CheckPoint") == 0) {
            Save(1); //Set the checkpoint 

            UpgradePlayer(2);

            CamAudioSource.pitch = NormalMusicPitch;
            LifeSystemScript.ActiveCamera = CamCinemachineMain;

            //Change Virtual Camera
            CamCinemachineMain_VirtualCamera.Priority = CameraHighPrority;
            CamCinemachineLock1_VirtualCamera.Priority = CameraLowPriority;

            //Delete only the wall that prevent the player continue the game
            Destroy(WallB1_2);

            //Disable Hearts UI of the Boss
            foreach (GameObject Heart in LifeBoss1) {
                Heart.SetActive(false);
            }
        }

        //Check if camera as passed the boss 2 room center, the first boss is dead and the second not
        if(Cam.transform.position.x >= LimitBoss2 && BossArmsDead && !BossLegsDead) {
            CamAudioSource.pitch = BossMusicPitch;
            LifeSystemScript.ActiveCamera = CamCinemachineLock2;

            //Change Virtual Camera
            CamCinemachineMain_VirtualCamera.Priority = CameraLowPriority;
            CamCinemachineLock2_VirtualCamera.Priority = CameraHighPrority;

            //Enable the boss and the room walls
            if(Boss2 != null) {
                Boss2.SetActive(true);
            }
            WallB2_1.SetActive(true);
            WallB2_2.SetActive(true);

            //Enable Heart UI of the boss
            foreach (GameObject Heart in LifeBoss2) {
                Heart.SetActive(true);
            }
        }
        //Check if the checkpoint is 1, the first boss and the second are dead
        else if (BossLegsDead && PlayerPrefs.GetInt("CheckPoint") == 1 && BossArmsDead) {
            Save(2); //Set the checkpoint 

            CamAudioSource.pitch = NormalMusicPitch;
            LifeSystemScript.ActiveCamera = CamCinemachineMain;

            //Change Virtual Camera
            CamCinemachineMain_VirtualCamera.Priority = CameraHighPrority;
            CamCinemachineLock2_VirtualCamera.Priority = CameraLowPriority;

            //Delete only the wall that prevent the player continue the game
            Destroy(WallB2_2);

            //Disable Hearts UI of the Boss
            foreach (GameObject Heart in LifeBoss2) {
                Heart.SetActive(false);
            }
        }

        //Enable pause menu
        if (Input.GetKeyDown(KeyCode.P)) {
            PauseMenu.SetActive(true);
            Time.timeScale = 0;
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
	}

    void UpgradePlayer(int Stage) {
        switch (Stage) {
            case 2:
                AnimatorNamesScript.Stages = 2; //Get the arm Upgrade
                UISmash.SetActive(true); //Enable the smash bar
                SmashScript.enabled = true; //Enable the smash power
                break;
            case 3:
                break;
        }
    }
}
