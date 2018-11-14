using System.Collections;
using UnityEngine;
/// <summary>
/// Script to make the player move
/// </summary>

public class PlayerMovement : MonoBehaviour{
    [SerializeField] private float Speed;
    [SerializeField] private float JumpForce;
    [SerializeField] private float MoveInput;
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private float CheckRadius;
    [SerializeField] private LayerMask GroundLayer;
    [SerializeField] private int ExtraJumpValue;
    [SerializeField] private GameObject Inputs;
    [SerializeField] private string LeftKey, RightKey, JumpKey, DashKey, InhaleKey, SmashKey;

    private bool IsXboxControls;
    private bool Fliped;
    private bool isGrounded;
    private int ExtraJumps;
    private bool AsPressedJump = false;
    private const float GetKeyWaitTime = 0.2f;

    private Rigidbody2D rb;
    private AnimatorNames AnimatorNamesScript;
    private Dash DashScript;
    private Inhale InhaleScript;
    private Smash SmashScript;
    private InputManager InputsManager;

    void Start () {
        if (PlayerPrefs.GetString("ControlLayout") == "Xbox") {
            IsXboxControls = true;
        }
        else {
            IsXboxControls = false;
        }

        Inputs = GameObject.FindGameObjectWithTag("InputManager");
        rb = GetComponent<Rigidbody2D>();
        AnimatorNamesScript = GetComponent<AnimatorNames>();
        DashScript = GetComponent<Dash>();
        InhaleScript = GetComponent<Inhale>();
        SmashScript = GetComponent<Smash>();
        InputsManager = Inputs.GetComponent<InputManager>();
        

        StartCoroutine("GetKey");
    }

    //Draw grounded sphere Gizmos
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.blue;
        //Gizmos.DrawSphere(GroundCheck.position, CheckRadius);
        Gizmos.DrawCube(GroundCheck.position, new Vector2(CheckRadius,2f));
    }

    private void FixedUpdate() {
        //Set the horizontal velocity
        rb.velocity = new Vector2(MoveInput * Speed, rb.velocity.y);
    }

    private void Update() {
        //Check if the player is grounded
        isGrounded = Physics2D.OverlapBox(GroundCheck.position, new Vector2(CheckRadius, 2f), 0, GroundLayer);

        //Check if the player as pressed the jump key
        if (JumpKey != "") {
            if (Input.GetKeyDown(JumpKey)) {
                AsPressedJump = true;
            }
            else {
                AsPressedJump = false;
            }
        }
        
        //Check if he is grounded, if so reasign the max value for the extra jumps
        if (isGrounded) {
            ExtraJumps = ExtraJumpValue;
        }

        //If the player as extra jumps he can jump in the air while decreasing his number of extra jumps
        if (AsPressedJump && ExtraJumps > 0) {
            rb.velocity = Vector2.up * JumpForce;
            ExtraJumps--;
        }

        // isGrounded = Physics2D.OverlapCircle(GroundCheck.position, CheckRadius, GroundLayer);

        if (LeftKey != "") {
            if (Input.GetKey(LeftKey)) {
                MoveInput = -1;
            }
            else if (Input.GetKey(RightKey)) {
                MoveInput = 1;
            }
            if (!Input.GetKey(LeftKey) && !Input.GetKey(RightKey)) {
                MoveInput = 0;
            }
        }

        //Flip the player acording to his movement
        if (Fliped && MoveInput > 0) {
            Flip();
        }
        else if (!Fliped && MoveInput < 0) {
            Flip();
        }

        //Play Animations
        if (MoveInput != 0) {
            AnimatorNamesScript.PlayAnimations("Walk");

        }
        else if (MoveInput == 0) {
            AnimatorNamesScript.PlayAnimations("Idle");
        }
    }

    void Flip() {
        Fliped = !Fliped;
        Vector3 Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }

    //Get all inputs from the Json
    IEnumerator GetKey() {
        yield return new WaitForSeconds(GetKeyWaitTime);

        LeftKey = InputsManager.Inputs.Left;
        RightKey = InputsManager.Inputs.Right;
        JumpKey = InputsManager.Inputs.Jump;

        DashKey = InputsManager.Inputs.Dash;
             DashScript.DashKey = DashKey;

        InhaleKey = InputsManager.Inputs.Inhale;
            InhaleScript.InhaleKey = InhaleKey;

        SmashKey = InputsManager.Inputs.Smash;
            SmashScript.SmashKey = SmashKey;
    }
}
