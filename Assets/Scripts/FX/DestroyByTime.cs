using UnityEngine;
/// <summary>
/// Destroy an object by a given time
/// </summary>
public class DestroyByTime : MonoBehaviour {

    [SerializeField] private float time;

    private void Start() {
        Destroy(gameObject, time);
    }
}
