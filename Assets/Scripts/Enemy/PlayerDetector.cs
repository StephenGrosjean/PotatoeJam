using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Check if the player is inside the Trigger 
/// </summary>


public class PlayerDetector : MonoBehaviour {
    [SerializeField] private bool playerInRange;

    public bool PlayerInRange {
        get {
            return playerInRange;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player") {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            playerInRange = false;
        }
    }
}
