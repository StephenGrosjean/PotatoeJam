using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOverTime : MonoBehaviour {
    [SerializeField] private float Speed;
    [SerializeField] private float ChangeDirectionTime;
    [SerializeField] private bool LockDirection;
    private int direction = 1;

    private void Start() {
        if (!LockDirection) {
            StartCoroutine("ChangeDirection");
        }
    }

    void Update () {
        transform.Rotate(new Vector3(0, 0, Speed*Time.deltaTime*direction));
	}

    IEnumerator ChangeDirection() {
        direction = 1;
        yield return new WaitForSeconds(ChangeDirectionTime);
        direction = -1;
        yield return new WaitForSeconds(ChangeDirectionTime);
        StartCoroutine("ChangeDirection");

    }
}
