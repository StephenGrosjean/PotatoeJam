using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script to make some bricks "Special"
/// </summary>
public class DestroyableBrick : MonoBehaviour {
    private bool isInDestroyProcess;
    public bool IsInDestroyProcess {
        get { return isInDestroyProcess; }
        set { isInDestroyProcess = value; }
    }

    [SerializeField] private GameObject poof;
    [SerializeField] private bool destroySmash;
    [SerializeField] private GameObject[] otherBricks;

    private const float destroyDamp = 0.1f;

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
            IsInDestroyProcess = true;
            StartCoroutine("DestroyTNT");
        }
    }

    public void DestroySequenceTNT() {
        Instantiate(poof, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    //Destroy bricks dash
    IEnumerator DestroySequenceDash() {
        rigid.bodyType = RigidbodyType2D.Dynamic;
        rigid.velocity = normal*50;
        rigid.gravityScale = 10;
        yield return new WaitForSeconds(0.5f);
        Instantiate(poof, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    //Destroy bricks Smash
    IEnumerator DestroyTNT() {
        Instantiate(poof, transform.position, Quaternion.identity);
        spriteRendererComponent.enabled = false;
        col.isTrigger = true;

        //add bricks to array
        for (int i = 0; i < otherBricks.Length; i ++) {
            if (otherBricks[i] != null) {
                otherBricksList.Add(otherBricks[i]);
            }
        }

        //Destroy all bricks
        foreach(GameObject brick in otherBricksList) {
            if (!brick.GetComponent<DestroyableBrick>().IsInDestroyProcess) {
                brick.GetComponent<DestroyableBrick>().DestroySequenceTNT();
            }
            yield return new WaitForSeconds(destroyDamp);
        }

        Destroy(gameObject);
    }
}
