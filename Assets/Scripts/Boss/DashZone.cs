using UnityEngine;
/// <summary>
/// Script to check if the player as dashed into the boss
/// </summary>
public class DashZone : MonoBehaviour {
    [SerializeField] private GameObject bossContainer;

    private GameObject player;
    private bool isDashing; 
    private DashDamp dashDampScript;
    private BossLife bossLifeScript;

    void OnEnable () {
        //Asign the objects to variables
        player = GameObject.FindGameObjectWithTag("Player");
        dashDampScript = player.GetComponent<DashDamp>();
        bossLifeScript = bossContainer.GetComponent<BossLife>();
	}

	void Update () {
        //Check if the player is dashing
        isDashing = dashDampScript.IsDashing;
	}

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Player") {
            if (isDashing) {
                //If the player is dashing and colliding with the boss DashZone, make damages to the Boss
                TakeDamage();
            }
        }
    }

    public void TakeDamage() {
        bossLifeScript.TakeDamage();
    }
}
