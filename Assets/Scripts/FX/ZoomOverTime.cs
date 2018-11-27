using UnityEngine;
/// <summary>
/// Scale object over time
/// </summary>
public class ZoomOverTime : MonoBehaviour {
    [SerializeField] private float minimumScale;
    [SerializeField] private float speed;
    private int direction = 1;
    private float maxScale;

    private void Start() {
        maxScale = transform.localScale.x;
    }

    void Update() {
        transform.localScale = new Vector3(transform.localScale.x - Time.deltaTime * direction, transform.localScale.y - Time.deltaTime*direction, transform.localScale.z);
        if (transform.localScale.x <= minimumScale) {
            direction *= -1;
        }
        if (transform.localScale.x >= maxScale) {
            direction *= -1;
        }
    }
}
