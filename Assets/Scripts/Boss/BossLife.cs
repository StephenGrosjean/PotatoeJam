using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script for the Boss Life
/// </summary>

public class BossLife : MonoBehaviour {

    [SerializeField] private GameObject Blood;
    [SerializeField] private Image[] Hearts;
    [SerializeField] private Sprite GoodHeart, BadHeart;
    [SerializeField] private GameObject LockVirtualCamera;
    [SerializeField] private GameObject Poof;
    [SerializeField] private GameObject PoofOrigin;
    [SerializeField] private int Life = 3;
    [SerializeField] private Transform BloodSpawnPoint;

    public string BossName;
    private Animator LockVirtualCameraAnimator;
    private GameManager GM;
    private Animator BossAnimator;
    private BossJambes BossJambesScript;


    private void OnEnable() {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        LockVirtualCameraAnimator = LockVirtualCamera.GetComponent<Animator>();
        BossAnimator = GetComponent<Animator>();
        BossName = gameObject.name;

        if (BossName == "BossJambes") {
            BossJambesScript = GetComponent<BossJambes>();
        }

       
    }

    void UpdateHearts() {
        //Change sprite for the Hearts acording to the life value
        switch (Life) {
            case 3:
                Hearts[0].sprite = GoodHeart;
                Hearts[1].sprite = GoodHeart;
                Hearts[2].sprite = GoodHeart;
                break;

            case 2:
                Hearts[0].sprite = GoodHeart;
                Hearts[1].sprite = GoodHeart;
                Hearts[2].sprite = BadHeart;
                break;

            case 1:
                Hearts[0].sprite = GoodHeart;
                Hearts[1].sprite = BadHeart;
                Hearts[2].sprite = BadHeart;
                break;

            case 0:
                Hearts[0].sprite = BadHeart;
                Hearts[1].sprite = BadHeart;
                Hearts[2].sprite = BadHeart;

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
        BossJambesScript.enabled = false;
        BossAnimator.Play("Defeat");
        yield return new WaitForSeconds(1.367f);
        KillBoss();
    }

    //Do damage to the boss, shake the camera and update the Hearts
    public void TakeDamage() {
        Invoke("UpdateHearts", 0);
        Instantiate(Blood, BloodSpawnPoint.position, Quaternion.identity);
        LockVirtualCameraAnimator.Play("CameraShake");
        Life--;
    }

    //Kill the boss
    public void KillBoss() {
        Instantiate(Poof, PoofOrigin.transform.position, Quaternion.identity);
        if(BossName == "BossBras") {
            GM.BossArmsDead = true;
        }
        else {
            GM.BossLegsDead = true;
        }
        
        foreach(Image Heart in Hearts) {
            Heart.enabled = false;
        }
        Hearts[0].sprite = GoodHeart;
        Hearts[1].sprite = GoodHeart;
        Hearts[2].sprite = GoodHeart;

        Destroy(gameObject);
    }
}
