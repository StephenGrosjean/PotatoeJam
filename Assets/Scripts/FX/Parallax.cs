using UnityEngine;
using UnityEngine.Tilemaps;
/// <summary>
/// Paralax script
/// </summary>
public class Parallax : MonoBehaviour {
    [SerializeField] private GameObject player;
    [SerializeField] private float speed;

    private Rigidbody2D playerRigidbody;
    private float playerVelocity;
    private Tilemap tilemapComponent;

	void Start () {
        playerRigidbody = player.GetComponent<Rigidbody2D>();
        tilemapComponent = GetComponent<Tilemap>();
	}
	
	void Update () {
        playerVelocity = playerRigidbody.velocity.x; //Get player velocity
        float offset = tilemapComponent.tileAnchor.x; //Get tile anchor x

        if (playerVelocity > 0) {
            tilemapComponent.tileAnchor = new Vector3(offset - Time.deltaTime * speed, 0, 0); //set the new offset 
        }

        if (playerVelocity < 0) {
            tilemapComponent.tileAnchor = new Vector3(offset + Time.deltaTime * speed, 0, 0); //set the new offset
        }
    }
}
