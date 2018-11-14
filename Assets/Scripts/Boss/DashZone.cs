using UnityEngine;
/// <summary>
/// Script to check if the player as dashed into the boss
/// </summary>
public class DashZone : MonoBehaviour {
    [SerializeField] private GameObject BossContainer;

    private GameObject Player;
    private bool isDashing; 
    private DashDamp DashDampScript;
    private BossLife BossLifeScript;

    void OnEnable () {
        //Asign the objects to variables
        Player = GameObject.FindGameObjectWithTag("Player");
        DashDampScript = Player.GetComponent<DashDamp>();
        BossLifeScript = BossContainer.GetComponent<BossLife>();
	}

	void Update () {
        //Check if the player is dashing
        isDashing = DashDampScript.IsDashing;
	}

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Player") {
            if (isDashing) {
                //If the player is dashing and colliding with the boss DashZone, make damages to the Boss
                BossLifeScript.TakeDamage();
            }
        }
    }
}
