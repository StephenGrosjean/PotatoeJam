using UnityEngine;

/// <summary>
/// Script that control the enemy movement
/// </summary>

public class Enemy : MonoBehaviour {
    
    [Header ("Physics/Movement")]
    //Physic variables
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float headBumpForce;
    [SerializeField] private float checkRadius;
    [SerializeField] private bool isGrounded;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody2D rigid;

    [Header("Control")]
    //Controller variables
    [SerializeField] private GameObject playerDetector;
    [SerializeField] private GameObject leftDetector, rightDetector;
    private Transform player;
    private Transform target;
    private Vector3 objectScale;
    private bool leftDetect, rightDetect;

    [Header("FX")]
    //effects
    [SerializeField] private GameObject poof;
    [SerializeField] private GameObject bloodParticles;
    //Player status
    private bool playerDashing;
    private bool playerInhaling;

    private const float jumpTimeStep = 0.2f;

    private PlayerDetector playerDetectorScript;
    private EnemySideDetector leftSideDetectorScript;
    private EnemySideDetector rightSideDetectorScript;
    private DashDamp dashDampScript;
    private Inhale inhaleScript;
    private Rigidbody2D playerRigidBody;
    private EnemyHeadCollider enemyHeadColliderScript;
    private LifeSystem playerLifeSystem;

    private void Start() {
        //Assign components to variables
        rigid = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerDetectorScript = playerDetector.GetComponent<PlayerDetector>();
        rightSideDetectorScript = rightDetector.GetComponent<EnemySideDetector>();
        leftSideDetectorScript = leftDetector.GetComponent<EnemySideDetector>();
        dashDampScript = player.GetComponent<DashDamp>();
        inhaleScript = player.GetComponent<Inhale>();
        playerRigidBody = player.GetComponent<Rigidbody2D>();
        enemyHeadColliderScript = GetComponentInChildren<EnemyHeadCollider>();
        playerLifeSystem = player.GetComponent<LifeSystem>();

        //Get the Scale of the Object;
        objectScale = transform.localScale;

        //Invoke the Jump method
        InvokeRepeating("Jump", 0, jumpTimeStep);
    }

    private void FixedUpdate() {
        //If the player is in range (From the PlayerDetector Script) 
        if (playerDetectorScript.PlayerInRange) {
            target = player; //Asign the Player as the target
        }
        else {
            target = transform; //Asign itself as the target 
        }

        //Check if the enemy is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        //Asign detector values to variables
        rightDetect = rightSideDetectorScript.Detected;
        leftDetect = leftSideDetectorScript.Detected;
        playerDashing = dashDampScript.IsDashing;
        playerInhaling = inhaleScript.DoInhale;

        //Asign the Orientation to a vector
        Vector3 orientation = transform.position - player.position;
        float oriX = orientation.x; //Get the X value of the orientation


        //Move the enemy towards the player only if it's not colliding with a wall
        if (!rightDetect || !leftDetect) { 
            transform.position = Vector2.MoveTowards(transform.position, new Vector3(target.position.x, transform.position.y, target.position.z), speed);
        }

        //FLip the object acording to the orientation
        if (oriX > 0) {
            gameObject.transform.localScale = new Vector3(-objectScale.x, objectScale.y, objectScale.z);
        }
        else if (oriX < 0) {
            gameObject.transform.localScale = new Vector3(objectScale.x, objectScale.y, objectScale.z);
        }
    }

    void Jump() {
        //If the player is grounded and is touching a wall, Make it jump
        if ((rightDetect || leftDetect) && isGrounded) {
            rigid.velocity = Vector2.up * jumpForce;
        }
    }

    public void Kill() {
        Instantiate(poof, transform.position, Quaternion.identity);
        Instantiate(bloodParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void KillHeadBump() {
        Instantiate(poof, transform.position, Quaternion.identity);
        playerRigidBody.velocity = Vector2.up * headBumpForce;
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Player") {
            //If the Player isn't dashing and isn't colliding with the head collider...
            if (!playerDashing && !enemyHeadColliderScript.HeadBump) {
                if (!playerInhaling) {
                    //...and he is not using the Inhale power, lower is life
                    playerLifeSystem.LowerLife();
                }
                else {
                    //...and he is using the Inhale power, rise is life 
                    playerLifeSystem.RiseLife();

                }
            }

            //Kill the enemy.
            Kill();
        }
    }
}
