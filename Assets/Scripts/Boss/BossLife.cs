using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script for the Boss Life
/// </summary>

public class BossLife : MonoBehaviour {
    [SerializeField] private float damageTimer;
    [SerializeField] private GameObject blood;
    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite goodHeart, badHeart;
    [SerializeField] private GameObject lockVirtualCamera;
    [SerializeField] private GameObject poof;
    [SerializeField] private GameObject poofOrigin;
    [SerializeField] private int life = 3;
    [SerializeField] private Transform bloodSpawnPoint;

    public string BossName;
    private Animator lockVirtualCameraAnimator;
    private GameManager GM;
    private Animator bossAnimator;
    private BossJambes bossJambesScript;

    private bool canTakeDamage = true;


    private void OnEnable() {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        lockVirtualCameraAnimator = lockVirtualCamera.GetComponent<Animator>();
        bossAnimator = GetComponent<Animator>();
        BossName = gameObject.name;

        if (BossName == "BossJambes") {
            bossJambesScript = GetComponent<BossJambes>();
        }

       
    }

    void UpdateHearts() {
        //Change sprite for the Hearts acording to the life value
        switch (life) {
            case 3:
                hearts[0].sprite = goodHeart;
                hearts[1].sprite = goodHeart;
                hearts[2].sprite = goodHeart;
                break;

            case 2:
                hearts[0].sprite = goodHeart;
                hearts[1].sprite = goodHeart;
                hearts[2].sprite = badHeart;
                break;

            case 1:
                hearts[0].sprite = goodHeart;
                hearts[1].sprite = badHeart;
                hearts[2].sprite = badHeart;
                break;

            case 0:
                hearts[0].sprite = badHeart;
                hearts[1].sprite = badHeart;
                hearts[2].sprite = badHeart;

                if(BossName == "BossBras") {
                    KillBoss();
                }
                else {
                    StartCoroutine("JambesDefeat");
                }
                
                break;
        }
    }


    IEnumerator JambesDefeat() {
        bossJambesScript.enabled = false;
        bossAnimator.Play("Defeat");
        yield return new WaitForSeconds(1.367f);
        KillBoss();
    }

    //Do damage to the boss, shake the camera and update the Hearts
    public void TakeDamage() {
        if (canTakeDamage) {
            StartCoroutine("TakeDamageTimer");
            Invoke("UpdateHearts", 0);
            Instantiate(blood, bloodSpawnPoint.position, Quaternion.identity);
            lockVirtualCameraAnimator.Play("CameraShake");
            life--;
        }

    }

    //Kill the boss
    public void KillBoss() {
        Instantiate(poof, poofOrigin.transform.position, Quaternion.identity);
        if(BossName == "BossBras") {
            GM.BossArmsDead = true;
        }
        else {
            GM.BossLegsDead = true;
        }
        
        foreach(Image heart in hearts) {
            heart.enabled = false;
        }
        hearts[0].sprite = goodHeart;
        hearts[1].sprite = goodHeart;
        hearts[2].sprite = goodHeart;

        Destroy(gameObject);
    }

    IEnumerator TakeDamageTimer() {
        canTakeDamage = false;
        yield return new WaitForSeconds(damageTimer);
        canTakeDamage = true;
    }
}
