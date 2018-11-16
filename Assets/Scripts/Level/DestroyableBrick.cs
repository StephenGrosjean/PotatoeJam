using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableBrick : MonoBehaviour {
    [SerializeField] private GameObject poof;
    [SerializeField] private bool destroySmash;
    [SerializeField] private GameObject[] otherBricks;

    private GameObject player;
    private DashDamp dashDampScript;
    private bool isPlayerDashing;

    private Rigidbody2D rigid;
    private Vector3 normal;


    private List<GameObject> otherBricksList = new List<GameObject>();
    private SpriteRenderer spriteRendererComponent;
    private BoxCollider2D col;

    void Start() {
        rigid = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        dashDampScript = player.GetComponent<DashDamp>();
        spriteRendererComponent = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
    }

    private void Update() {
        isPlayerDashing = dashDampScript.IsDashing;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            if (!destroySmash) {
                if (isPlayerDashing) {
                    normal = collision.contacts[0].normal;
                    StartCoroutine("DestroySequenceDash");
                }
            }
        }
    }

    public void DestroySequenceSmash() {
        if (destroySmash) {
            StartCoroutine("DestroyTNT");
        }
    }

    public void DestroySequenceTnt() {
        Instantiate(poof, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    IEnumerator DestroySequenceDash() {
        rigid.bodyType = RigidbodyType2D.Dynamic;
        rigid.velocity = normal*50;
        rigid.gravityScale = 10;
        yield return new WaitForSeconds(0.5f);
        Instantiate(poof, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    IEnumerator DestroyTnt() {

        Instantiate(poof, transform.position, Quaternion.identity);
        spriteRendererComponent.enabled = false;
        col.isTrigger = true;

        for (int i = 0; i < otherBricks.Length; i ++) {
            if (otherBricks[i] != null) {
                otherBricksList.Add(otherBricks[i]);
            }
        }

        foreach(GameObject brick in otherBricksList) {
            brick.GetComponent<DestroyableBrick>().DestroySequenceTnt();
            yield return new WaitForSeconds(0.1f);
        }

        Destroy(gameObject);
    }
}
