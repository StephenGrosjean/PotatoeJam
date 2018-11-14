using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script to pickup the heal object
/// </summary>

public class HealPickup : MonoBehaviour {
    [SerializeField] private GameObject Poof;

    private LifeSystem LifeSystemScript;

    //Check if the player as collided with the object
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player") {
            //Get his life system script
            LifeSystemScript = collision.GetComponent<LifeSystem>();

            //Check if he is not full life
            if(LifeSystemScript.Life < 4) {
                //Rise his life
                LifeSystemScript.RiseLife();
            }

            //Instantiate the Poof Prefab and destroy itself
            Instantiate(Poof, transform.position, Quaternion.identity);
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
