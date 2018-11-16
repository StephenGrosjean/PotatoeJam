using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Check if the player is inside the head collider on the enemy, if so, Kill the enemy
/// </summary>
public class EnemyHeadCollider : MonoBehaviour {

    [SerializeField] private bool headBump;
    public bool HeadBump
    {
        get { return headBump; }
        set { headBump = value; }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Player") {
            HeadBump = true;
            GetComponentInParent<Enemy>().KillHeadBump(); //Start the KillHeadBump Method on the Enemy Object
        }  
    }

}
