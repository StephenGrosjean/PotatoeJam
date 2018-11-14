using UnityEngine;

public class BloodGrounded : MonoBehaviour {
    [SerializeField] private float CheckRadius;
    [SerializeField] private Transform GroundCheckLeft;
    [SerializeField] private Transform GroundCheckRight;
    [SerializeField] private Transform GroundCheckUp;
    [SerializeField] private LayerMask GroundLayer;

    private bool isGroundedLeft;
    private bool isGroundedRight;
    private bool isGroundedUp;

    void Start() {
        isGroundedLeft = Physics2D.OverlapCircle(GroundCheckLeft.position, CheckRadius, GroundLayer);
        isGroundedRight = Physics2D.OverlapCircle(GroundCheckRight.position, CheckRadius, GroundLayer);
        isGroundedUp = Physics2D.OverlapCircle(GroundCheckUp.position, CheckRadius, GroundLayer);


        if (!isGroundedLeft || !isGroundedRight || isGroundedUp) {
            Destroy(gameObject);
        }
    }
}
