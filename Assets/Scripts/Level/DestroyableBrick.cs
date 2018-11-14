using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableBrick : MonoBehaviour {
    [SerializeField] private GameObject Poof;
    [SerializeField] private bool DestroySmash;
    [SerializeField] private GameObject[] OtherBricks;

    private GameObject Player;
    private DashDamp DashDampScript;
    private bool isPlayerDashing;

    private Rigidbody2D Rigid;
    private Vector3 Normal;


    private List<GameObject> OtherBricksList = new List<GameObject>();
    private SpriteRenderer SpriteRendererComponent;
    private BoxCollider2D Col;

    void Start() {
        Rigid = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player");
        DashDampScript = Player.GetComponent<DashDamp>();
        SpriteRendererComponent = GetComponent<SpriteRenderer>();
        Col = GetComponent<BoxCollider2D>();
    }

    private void Update() {
        isPlayerDashing = DashDampScript.IsDashing;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            if (!DestroySmash) {
                if (isPlayerDashing) {
                    Normal = collision.contacts[0].normal;
                    StartCoroutine("DestroySequenceDash");
                }
            }
        }
    }

    public void DestroySequenceSmash() {
        if (DestroySmash) {
            StartCoroutine("DestroyTNT");
        }
    }

    public void DestroySequenceTNT() {
        Instantiate(Poof, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    IEnumerator DestroySequenceDash() {
        Rigid.bodyType = RigidbodyType2D.Dynamic;
        Rigid.velocity = Normal*50;
        Rigid.gravityScale = 10;
        yield return new WaitForSeconds(0.5f);
        Instantiate(Poof, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    IEnumerator DestroyTNT() {

        Instantiate(Poof, transform.position, Quaternion.identity);
        SpriteRendererComponent.enabled = false;
        Col.isTrigger = true;

        for (int i = 0; i < OtherBricks.Length; i ++) {
            if (OtherBricks[i] != null) {
                OtherBricksList.Add(OtherBricks[i]);
            }
        }

        foreach(GameObject brick in OtherBricksList) {
            brick.GetComponent<DestroyableBrick>().DestroySequenceTNT();
            yield return new WaitForSeconds(0.1f);
        }

        Destroy(gameObject);
    }
}
