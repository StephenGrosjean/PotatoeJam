using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            
        }else if(collision.gameObject.tag == "SpecialBrick") {
            collision.gameObject.GetComponent<DestroyableBrick>().DestroySequenceSmash();
            Destroy(gameObject);
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
