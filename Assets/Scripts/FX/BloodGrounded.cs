using UnityEngine;
/// <summary>
/// Check if the particle is grounded
/// </summary>
public class BloodGrounded : MonoBehaviour {
    [SerializeField] private float checkRadius;
    [SerializeField] private Transform groundCheckLeft;
    [SerializeField] private Transform groundCheckRight;
    [SerializeField] private Transform groundCheckUp;
    [SerializeField] private LayerMask groundLayer;

    private bool isGroundedLeft;
    private bool isGroundedRight;
    private bool isGroundedUp;

    void Start() {
        isGroundedLeft = Physics2D.OverlapCircle(groundCheckLeft.position, checkRadius, groundLayer);
        isGroundedRight = Physics2D.OverlapCircle(groundCheckRight.position, checkRadius, groundLayer);
        isGroundedUp = Physics2D.OverlapCircle(groundCheckUp.position, checkRadius, groundLayer);


        if (!isGroundedLeft || !isGroundedRight || isGroundedUp) {
            Destroy(gameObject);
        }
    }
}
