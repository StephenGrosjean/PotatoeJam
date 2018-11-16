using UnityEngine;

public class DestroyByTime : MonoBehaviour {

    [SerializeField] private float time;

    private void Start() {
        Destroy(gameObject, time);
    }
}
