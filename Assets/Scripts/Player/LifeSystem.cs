using System.Collections;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Script that manage the player life
/// </summary>
public class LifeSystem : MonoBehaviour {
    [SerializeField] private GameObject activeCamera;
    public GameObject ActiveCamera
    {
        get { return activeCamera; }
        set { activeCamera = value; }
    }

    private int life = 4;

    public int Life
    {
        get { return life; }
        set { life = value; }
    }

    [SerializeField] private AudioClip lifeUp;
    [SerializeField] private GameObject fxPlayer;
    [SerializeField] private GameObject fxHeal;
    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite goodHeart, badHeart;
    [SerializeField] private GameObject poof;
    [SerializeField] private GameObject damageScreen;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject inhaleSlider;
    [SerializeField] private int maxLife = 4;
    public bool Invincible;
    private Animator camAnimator;
    private bool canTakeDamage;

    private const float damageTimerWaitTime = 0.5f;

    private Inhale inhaleScript;
    private Image inhaleSliderImage;
    private Animator damageScreenAnimator;
    private AudioSource fxPlayerAudioSource;
    private Darkend deathScreenScript;

    private void Start() {
        canTakeDamage = true;

        camAnimator = ActiveCamera.GetComponent<Animator>();
        inhaleScript = GetComponent<Inhale>();
        inhaleSliderImage = inhaleSlider.GetComponentInChildren<Image>();
        damageScreenAnimator = damageScreen.GetComponent<Animator>();
        camAnimator = ActiveCamera.GetComponent<Animator>();
        fxPlayerAudioSource = fxPlayer.GetComponent<AudioSource>();
        deathScreenScript = deathScreen.GetComponent<Darkend>();

    }

    void Update () {
        //Look if life doesn't get above the MaxLife value
        if (Life > maxLife) {
            Life = maxLife;
        }

        //Fill the Inhale Slider and disable it
        if(Life == maxLife) {
            inhaleSliderImage.fillAmount = 1;
            inhaleScript.DoInhale = false;
            inhaleScript.enabled = false;
        }
        else {
            inhaleScript.enabled = true;
        }

        switch (Life) {
            case 4:
                hearts[0].sprite = goodHeart;
                hearts[1].sprite = goodHeart;
                hearts[2].sprite = goodHeart;
                hearts[3].sprite = goodHeart;
                break;

            case 3:
                hearts[0].sprite = goodHeart;
                hearts[1].sprite = goodHeart;
                hearts[2].sprite = goodHeart;
                hearts[3].sprite = badHeart;
                break;

            case 2:
                hearts[0].sprite = goodHeart;
                hearts[1].sprite = goodHeart;
                hearts[2].sprite = badHeart;
                hearts[3].sprite = badHeart;
                break;

            case 1:
                hearts[0].sprite = goodHeart;
                hearts[1].sprite = badHeart;
                hearts[2].sprite = badHeart;
                hearts[3].sprite = badHeart;
                break;

            case 0:
               
                hearts[0].sprite = badHeart;
                hearts[1].sprite = badHeart;
                hearts[2].sprite = badHeart;
                hearts[3].sprite = badHeart;
                KillPlayer();
                break;
        }
	}

    public void LowerLife() {
        if (canTakeDamage) {
            damageScreenAnimator.Play("Damage");
            camAnimator.Play("CameraShake");
            if (!Invincible) {
                Life--;
            }
            
        }
    }

    public void RiseLife() {
        //instantiate the Wing Gif and put it as child
        GameObject wing = Instantiate(fxHeal, new Vector3(transform.position.x, transform.position.y+17, transform.position.z-0.1f), Quaternion.identity);
        wing.transform.SetParent(transform);

        //Play audio clip
        fxPlayerAudioSource.clip = lifeUp;
        fxPlayerAudioSource.Play();
        Life++;
    }

    void KillPlayer() {
        Instantiate(poof, transform.position, Quaternion.identity);
        deathScreenScript.StartCoroutine("DeathProtocol");
        Destroy(gameObject);
    }

    //Allow the Player to take one damage at a time
    IEnumerator DamageTimer() {
        canTakeDamage = false;
        yield return new WaitForSeconds(damageTimerWaitTime);
        canTakeDamage = true;
    }

}
