using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Check if the enemy collide with the ground on it's side
/// </summary>

public class EnemySideDetector : MonoBehaviour {
    public bool Detected; //Get (yes)

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Ground") {
            Detected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.gameObject.tag == "Ground") {
            Detected = false;
        }
    }
}
