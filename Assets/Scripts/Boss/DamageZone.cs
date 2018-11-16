using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script to check if the player have to recieve damages 
/// </summary>

public class DamageZone : MonoBehaviour {
    public bool IsInZone; //Get (yes)

    private LifeSystem lifeSystemScript;
    private GameObject player;
    private bool canGiveDmg = true;
    private const float DamageDamp = 0.5f;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        lifeSystemScript = player.GetComponent<LifeSystem>();
    }

    //Give him some damages if he is in zone
   public void ApplyDamages() {
        if (IsInZone && canGiveDmg) {
            canGiveDmg = false;
            StartCoroutine("DmgDamp");
            lifeSystemScript.LowerLife();
        }
    }

    //Check if the player is in the zone
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player") {
            IsInZone = true;
        }
    }
    
    //Check if the player is not in the zone anymore
    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player") {
            IsInZone = false;
        }
    }
    
    //Prevent the boss dealing more than 1 damage to the player at a time
    IEnumerator DmgDamp() {
        yield return new WaitForSeconds(DamageDamp);
        canGiveDmg = true;
       
    }
}
