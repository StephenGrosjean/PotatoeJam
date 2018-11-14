using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script that control the enemy movement
/// </summary>

public class Enemy : MonoBehaviour {
    
    [Header ("Physics/Movement")]
    //Physic variables
    [SerializeField] private float Speed;
    [SerializeField] private float JumpForce;
    [SerializeField] private float HeadBumpForce;
    [SerializeField] private float CheckRadius;
    [SerializeField] private bool isGrounded;
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private LayerMask GroundLayer;
    private Rigidbody2D Rigid;

    [Header("Control")]
    //Controller variables
    [SerializeField] private GameObject PlayerDetector;
    [SerializeField] private GameObject LeftDetector, RightDetector;
    private Transform Player;
    private Transform Target;
    private Vector3 ObjectScale;
    private bool LeftDetect, RightDetect;

    [Header("FX")]
    //effects
    [SerializeField] private GameObject Poof;
    [SerializeField] private GameObject BloodParticles;
    //Player status
    private bool PlayerDashing;
    private bool PlayerInhaling;

    private const float JumpTimeStep = 0.2f;

    private PlayerDetector PlayerDetectorScript;
    private EnemySideDetector LeftSideDetectorScript;
    private EnemySideDetector RightSideDetectorScript;
    private DashDamp DashDampScript;
    private Inhale InhaleScript;
    private Rigidbody2D PlayerRigidBody;
    private EnemyHeadCollider EnemyHeadColliderScript;
    private LifeSystem PlayerLifeSystem;

    private void Start() {
        //Asign components to variables
        Rigid = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        PlayerDetectorScript = PlayerDetector.GetComponent<PlayerDetector>();
        RightSideDetectorScript = RightDetector.GetComponent<EnemySideDetector>();
        LeftSideDetectorScript = LeftDetector.GetComponent<EnemySideDetector>();
        DashDampScript = Player.GetComponent<DashDamp>();
        InhaleScript = Player.GetComponent<Inhale>();
        PlayerRigidBody = Player.GetComponent<Rigidbody2D>();
        EnemyHeadColliderScript = GetComponentInChildren<EnemyHeadCollider>();
        PlayerLifeSystem = Player.GetComponent<LifeSystem>();

        //Get the Scale of the Object;
        ObjectScale = transform.localScale;

        //Invoke the Jump method
        InvokeRepeating("Jump", 0, JumpTimeStep);
    }

    private void FixedUpdate() {
        //If the player is in range (From the PlayerDetector Script) 
        if (PlayerDetectorScript.playerInRange) {
            Target = Player; //Asign the Player as the target
        }
        else {
            Target = transform; //Asign itself as the target 
        }

        //Check if the enemy is grounded
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, CheckRadius, GroundLayer);

        //Asign detector values to variables
        RightDetect = RightSideDetectorScript.Detected;
        LeftDetect = LeftSideDetectorScript.Detected;
        PlayerDashing = DashDampScript.IsDashing;
        PlayerInhaling = InhaleScript.DoInhale;

        //Asign the Orientation to a vector
        Vector3 Orientation = transform.position - Player.position;
        float oriX = Orientation.x; //Get the X value of the orientation


        //Move the enemy towards the player only if it's not colliding with a wall
        if (!RightDetect || !LeftDetect) { 
            transform.position = Vector2.MoveTowards(transform.position, new Vector3(Target.position.x, transform.position.y, Target.position.z), Speed);
        }

        //FLip the object acording to the orientation
        if (oriX > 0) {
            gameObject.transform.localScale = new Vector3(-ObjectScale.x, ObjectScale.y, ObjectScale.z);
        }
        else if (oriX < 0) {
            gameObject.transform.localScale = new Vector3(ObjectScale.x, ObjectScale.y, ObjectScale.z);
        }
    }

    void Jump() {
        //If the player is grounded and is touching a wall, Make it jump
        if ((RightDetect || LeftDetect) && isGrounded) {
            Rigid.velocity = Vector2.up * JumpForce;
        }
    }

    public void Kill() {
        Instantiate(Poof, transform.position, Quaternion.identity);
        Instantiate(BloodParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void KillHeadBump() {
        Instantiate(Poof, transform.position, Quaternion.identity);
        PlayerRigidBody.velocity = Vector2.up * HeadBumpForce;
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Player") {
            //If the Player isn't dashing and isn't colliding with the head collider...
            if (!PlayerDashing && !EnemyHeadColliderScript.HeadBump) {
                if (!PlayerInhaling) {
                    //...and he is not using the Inhale power, lower is life
                    PlayerLifeSystem.LowerLife();
                }
                else {
                    //...and he is using the Inhale power, rise is life 
                    PlayerLifeSystem.RiseLife();

                }
            }

            //Kill the enemy.
            Kill();
        }
    }
}
