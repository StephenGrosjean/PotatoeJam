using System.Collections;
using UnityEngine;
/// <summary>
/// Script to make the player move
/// </summary>

public class PlayerMovement : MonoBehaviour{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float moveInput;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private int extraJumpValue;
    [SerializeField] private GameObject inputs;
    [SerializeField] private string leftKey, rightKey, jumpKey, dashKey, inhaleKey, smashKey;

    private bool isXboxControls;
    private bool fliped;
    private bool isGrounded;
    private int extraJumps;
    private bool asPressedJump = false;
    private const float GetKeyWaitTime = 0.2f;

    private Rigidbody2D rb;
    private AnimatorNames animatorNamesScript;
    private Dash dashScript;
    private Inhale inhaleScript;
    private Smash smashScript;
    private InputManager inputsManager;

    void Start () {
        if (PlayerPrefs.GetString("ControlLayout") == "Xbox") {
            isXboxControls = true;
        }
        else {
            isXboxControls = false;
        }

        inputs = GameObject.FindGameObjectWithTag("InputManager");
        rb = GetComponent<Rigidbody2D>();
        animatorNamesScript = GetComponent<AnimatorNames>();
        dashScript = GetComponent<Dash>();
        inhaleScript = GetComponent<Inhale>();
        smashScript = GetComponent<Smash>();
        inputsManager = inputs.GetComponent<InputManager>();
        

        StartCoroutine("GetKey");
    }

    //Draw grounded sphere Gizmos
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.blue;
        //Gizmos.DrawSphere(GroundCheck.position, CheckRadius);
        Gizmos.DrawCube(groundCheck.position, new Vector2(checkRadius,2f));
    }

    private void FixedUpdate() {
        //Set the horizontal velocity
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }

    private void Update() {
        //Check if the player is grounded
        isGrounded = Physics2D.OverlapBox(groundCheck.position, new Vector2(checkRadius, 2f), 0, groundLayer);

        //Check if the player as pressed the jump key
        if (jumpKey != "") {
            if (Input.GetKeyDown(jumpKey)) {
                asPressedJump = true;
            }
            else {
                asPressedJump = false;
            }
        }
        
        //Check if he is grounded, if so reasign the max value for the extra jumps
        if (isGrounded) {
            extraJumps = extraJumpValue;
        }

        //If the player as extra jumps he can jump in the air while decreasing his number of extra jumps
        if (asPressedJump && extraJumps > 0) {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }

        // isGrounded = Physics2D.OverlapCircle(GroundCheck.position, CheckRadius, GroundLayer);

        if (leftKey != "") {
            if (Input.GetKey(leftKey)) {
                moveInput = -1;
            }
            else if (Input.GetKey(rightKey)) {
                moveInput = 1;
            }
            if (!Input.GetKey(leftKey) && !Input.GetKey(rightKey)) {
                moveInput = 0;
            }
        }

        //Flip the player acording to his movement
        if (fliped && moveInput > 0) {
            Flip();
        }
        else if (!fliped && moveInput < 0) {
            Flip();
        }

        //Play Animations
        if (moveInput != 0) {
            animatorNamesScript.PlayAnimations("Walk");

        }
        else if (moveInput == 0) {
            animatorNamesScript.PlayAnimations("Idle");
        }
    }

    void Flip() {
        fliped = !fliped;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    //Get all inputs from the Json
    IEnumerator GetKey() {
        yield return new WaitForSeconds(GetKeyWaitTime);

        leftKey = inputsManager.Inputs.Left;
        rightKey = inputsManager.Inputs.Right;
        jumpKey = inputsManager.Inputs.Jump;

        dashKey = inputsManager.Inputs.Dash;
             dashScript.DashKey = dashKey;

        inhaleKey = inputsManager.Inputs.Inhale;
            inhaleScript.InhaleKey = inhaleKey;

        smashKey = inputsManager.Inputs.Smash;
            smashScript.SmashKey = smashKey;
    }
}
