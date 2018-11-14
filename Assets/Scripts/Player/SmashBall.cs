using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashBall : MonoBehaviour {

    [SerializeField] private float Force;
    [SerializeField] private int Direction;

    private Rigidbody2D Rigid;

    private void Start() {
        Rigid = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Enemy") {
            collision.gameObject.GetComponent<Enemy>().Kill();
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "Ground"){
            Debug.Log("GT");
            Destroy(gameObject);
            
        }
    }

    private void Update() {
        if (Direction != 0) {
            Rigid.velocity = new Vector2(Direction * Force, transform.position.y);
        }
    }
       

    public void SetDirection(int dir) {
        Direction = dir;
    }
}
