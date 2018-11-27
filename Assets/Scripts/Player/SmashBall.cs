using UnityEngine;
/// <summary>
/// Script to check the smashball collisions
/// </summary>
public class SmashBall : MonoBehaviour {

    [SerializeField] private float force;
    [SerializeField] private int direction;

    private Rigidbody2D rigid;

    private void Start() {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Enemy") {
            collision.gameObject.GetComponent<Enemy>().Kill();

            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "Ground"){
            Destroy(gameObject);
            
        }
        else if(collision.gameObject.tag == "SpecialBrick") {
            collision.gameObject.GetComponent<DestroyableBrick>().DestroySequenceSmash(); //Get the destroyable brick script
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "DashZone") {
            collision.gameObject.GetComponent<DashZone>().TakeDamage(); //Get the dashzone script from the boss
        }
    }

    private void Update() {
        if (direction != 0) {
            rigid.velocity = new Vector2(direction * force, transform.position.y);
        }
    }
       

    public void SetDirection(int dir) {
        direction = dir;
    }
}
