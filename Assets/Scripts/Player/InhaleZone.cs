
using UnityEngine;

public class InhaleZone : MonoBehaviour {
    [SerializeField] private string MaskAttract;
    [SerializeField] private string MaskNone;

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Enemy") {
            collision.gameObject.layer = LayerMask.NameToLayer(MaskAttract);
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Enemy") {
            collision.gameObject.layer = LayerMask.NameToLayer(MaskNone);
        }
    }
}
