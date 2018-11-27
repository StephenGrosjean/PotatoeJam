using UnityEngine;
/// <summary>
/// Script to detect if the Player is inside the trigger zone
/// </summary>
public class LevelTrigger : MonoBehaviour {

    [SerializeField] private bool activateObject;
    [SerializeField] private GameObject[] objectsToActivate;

    [SerializeField] private bool triggered;
    public bool Triggered
    {
        get { return triggered; }
        set { triggered = value; }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player") {
            Triggered = true;
            if (activateObject) {
                ActivateObjects();
            }
        }
    }

    void ActivateObjects() {

        foreach(GameObject obj in objectsToActivate) {
            Debug.Log("EEE");
            obj.SetActive(true);
        }
        //Destroy(gameObject);
    }
}
