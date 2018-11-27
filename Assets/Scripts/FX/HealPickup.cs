using UnityEngine;
/// <summary>
/// Script to pickup the heal object
/// </summary>

public class HealPickup : MonoBehaviour {
    [SerializeField] private GameObject poof;
    [SerializeField] private int maxLife = 4;
    private LifeSystem lifeSystemScript;


    //Check if the player as collided with the object
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player") {
            //Get his life system script
            lifeSystemScript = collision.GetComponent<LifeSystem>();

            //Check if he is not full life
            if(lifeSystemScript.Life < maxLife) {
                //Rise his life
                lifeSystemScript.RiseLife();
            }

            //Instantiate the Poof Prefab and destroy itself
            Instantiate(poof, transform.position, Quaternion.identity);
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
