using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script to detect if the Player is inside the trigger zone
/// </summary>
public class LevelTrigger : MonoBehaviour {

    public bool Triggered; //GET (yes)

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player") {
            Triggered = true;
        }
    }
}
