using UnityEngine;
/// <summary>
/// Script to check if the enemy is in the inhale zone
/// </summary>
public class InhaleZone : MonoBehaviour {
    [SerializeField] private string maskAttract;
    [SerializeField] private string maskNone;

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Enemy") {
            collision.gameObject.layer = LayerMask.NameToLayer(maskAttract);
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Enemy") {
            collision.gameObject.layer = LayerMask.NameToLayer(maskNone);
        }
    }
}
