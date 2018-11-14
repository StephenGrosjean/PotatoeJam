using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour {

    [SerializeField] private float Time;

    private void Start() {
        Destroy(gameObject, Time);
    }
}
