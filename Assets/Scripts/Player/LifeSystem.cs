using System.Collections;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Script that manage the player life
/// </summary>
public class LifeSystem : MonoBehaviour {
    public GameObject ActiveCamera; //GET (yes)
    public int Life = 4; //GET (yes)

    [SerializeField] private AudioClip LifeUp;
    [SerializeField] private GameObject FxPlayer;
    [SerializeField] private GameObject FxHeal;
    [SerializeField] private Image[] Hearts;
    [SerializeField] private Sprite GoodHeart, BadHeart;
    [SerializeField] private GameObject Poof;
    [SerializeField] private GameObject DamageScreen;
    [SerializeField] private GameObject DeathScreen;
    [SerializeField] private GameObject InhaleSlider;
    [SerializeField] private int MaxLife = 4;
    public bool Invincible;
    private Animator CamAnimator;
    private bool CanTakeDamage;

    private const float DamageTimerWaitTime = 0.5f;

    private Inhale InhaleScript;
    private Image InhaleSliderImage;
    private Animator DamageScreenAnimator;
    private AudioSource FXPlayerAudioSource;
    private Darkend DeathScreenScript;

    private void Start() {
        CanTakeDamage = true;

        CamAnimator = ActiveCamera.GetComponent<Animator>();
        InhaleScript = GetComponent<Inhale>();
        InhaleSliderImage = InhaleSlider.GetComponentInChildren<Image>();
        DamageScreenAnimator = DamageScreen.GetComponent<Animator>();
        CamAnimator = ActiveCamera.GetComponent<Animator>();
        FXPlayerAudioSource = FxPlayer.GetComponent<AudioSource>();
        DeathScreenScript = DeathScreen.GetComponent<Darkend>();

    }

    void Update () {
        //Look if life doesn't get above the MaxLife value
        if (Life > MaxLife) {
            Life = MaxLife;
        }

        //Fill the Inhale Slider and disable it
        if(Life == MaxLife) {
            InhaleSliderImage.fillAmount = 1;
            InhaleScript.DoInhale = false;
            InhaleScript.enabled = false;
        }
        else {
            InhaleScript.enabled = true;
        }

        switch (Life) {
            case 4:
                Hearts[0].sprite = GoodHeart;
                Hearts[1].sprite = GoodHeart;
                Hearts[2].sprite = GoodHeart;
                Hearts[3].sprite = GoodHeart;
                break;

            case 3:
                Hearts[0].sprite = GoodHeart;
                Hearts[1].sprite = GoodHeart;
                Hearts[2].sprite = GoodHeart;
                Hearts[3].sprite = BadHeart;
                break;

            case 2:
                Hearts[0].sprite = GoodHeart;
                Hearts[1].sprite = GoodHeart;
                Hearts[2].sprite = BadHeart;
                Hearts[3].sprite = BadHeart;
                break;

            case 1:
                Hearts[0].sprite = GoodHeart;
                Hearts[1].sprite = BadHeart;
                Hearts[2].sprite = BadHeart;
                Hearts[3].sprite = BadHeart;
                break;

            case 0:
               
                Hearts[0].sprite = BadHeart;
                Hearts[1].sprite = BadHeart;
                Hearts[2].sprite = BadHeart;
                Hearts[3].sprite = BadHeart;
                KillPlayer();
                break;
        }
	}

    public void LowerLife() {
        if (CanTakeDamage) {
            DamageScreenAnimator.Play("Damage");
            CamAnimator.Play("CameraShake");
            if (!Invincible) {
                Life--;
            }
            
        }
    }

    public void RiseLife() {
        //instantiate the Wing Gif and put it as child
        GameObject Wing = Instantiate(FxHeal, new Vector3(transform.position.x, transform.position.y+17, transform.position.z-0.1f), Quaternion.identity);
        Wing.transform.SetParent(transform);

        //Play audio clip
        FXPlayerAudioSource.clip = LifeUp;
        FXPlayerAudioSource.Play();
        Life++;
    }

    void KillPlayer() {
        Instantiate(Poof, transform.position, Quaternion.identity);
        DeathScreenScript.StartCoroutine("DeathProtocol");
        Destroy(gameObject);
    }

    //Allow the Player to take one damage at a time
    IEnumerator DamageTimer() {
        CanTakeDamage = false;
        yield return new WaitForSeconds(DamageTimerWaitTime);
        CanTakeDamage = true;
    }

}
