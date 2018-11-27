using System.Collections;
using UnityEngine;
/// <summary>
/// Rotate over time
/// </summary>
public class RotateOverTime : MonoBehaviour {
    [SerializeField] private float speed;
    [SerializeField] private float changeDirectionTime;
    [SerializeField] private bool lockDirection;
    private int direction = 1;

    private void Start() {
        if (!lockDirection) {
            StartCoroutine("ChangeDirection");
        }
    }

    void Update () {
        transform.Rotate(new Vector3(0, 0, speed*Time.deltaTime*direction));
	}

    IEnumerator ChangeDirection() {
        direction = 1;
        yield return new WaitForSeconds(changeDirectionTime);
        direction = -1;
        yield return new WaitForSeconds(changeDirectionTime);
        StartCoroutine("ChangeDirection");

    }
}
