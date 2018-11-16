using System.Collections;
using UnityEngine;

public class BossJambes : MonoBehaviour
{
    [SerializeField] private Transform leftPos;
    [SerializeField] private Transform rightPos;
    [SerializeField] private GameObject rightDamageZone, leftDamageZone;
    [SerializeField] private GameObject hitCircle;
    [SerializeField] private GameObject dashZone;
    [SerializeField] private float speed;
    [SerializeField] private float waitIdle, waitWalk, waitBeforeHit, waitHit, waitAfterHit;
    [SerializeField] private float kickForce;

    private bool sideLeft;
    private Transform target;
    private GameObject player;

    private Animator animatorComponent;
    private DamageZone rightDamageZoneScript, leftDamageZoneScript;
    private LifeSystem lifeSystemScript;
    private Rigidbody2D playerRigidbody;
    private PlayerMovement playerMovementScript;
    private DashZone dashZoneScript;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        animatorComponent = GetComponent<Animator>();
        rightDamageZoneScript = rightDamageZone.GetComponent<DamageZone>();
        leftDamageZoneScript = leftDamageZone.GetComponent<DamageZone>();
        lifeSystemScript = player.GetComponent<LifeSystem>();
        playerRigidbody = player.GetComponent<Rigidbody2D>();
        playerMovementScript = player.GetComponent<PlayerMovement>();
        dashZoneScript = dashZone.GetComponent<DashZone>();

        StartCoroutine("States", "Start");
    }

    void Update()
    {
        if (target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                new Vector3(target.position.x, transform.position.y, target.position.z), speed);
        }

        if (sideLeft)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (!sideLeft)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void OnDisable()
    {
        hitCircle.SetActive(false);
    }

    void ApplyKickDamages()
    {
        if (sideLeft)
        {
            if (leftDamageZoneScript.IsInZone)
            {
                lifeSystemScript.LowerLife();
                playerRigidbody.AddForce(Vector2.up * kickForce, ForceMode2D.Impulse);
            }
        }
        else
        {
            if (rightDamageZoneScript.IsInZone)
            {
                lifeSystemScript.LowerLife();
                playerRigidbody.AddForce(Vector2.up * kickForce, ForceMode2D.Impulse);
            }
        }
    }


    IEnumerator States(string state)
    {
        switch (state)
        {
            case "Start":
                Debug.Log("Start");
                yield return new WaitForSeconds(1);
                StartCoroutine("States", "Idle");
                break;

            case "Idle":
                Debug.Log("Idle");
                animatorComponent.Play("Idle");
                yield return new WaitForSeconds(waitIdle);
                hitCircle.SetActive(false);
                dashZone.SetActive(false);

                if (sideLeft)
                {
                    StartCoroutine("States", "WalkRight");
                }
                else
                {
                    StartCoroutine("States", "WalkLeft");
                }

                break;

            case "WalkLeft":
                Debug.Log("WalkLeft");
                target = leftPos;
                animatorComponent.Play("Walk");
                yield return new WaitForSeconds(waitWalk);
                sideLeft = true;
                StartCoroutine("States", "Wait");
                break;

            case "WalkRight":
                Debug.Log("WalkRight");
                target = rightPos;
                animatorComponent.Play("Walk");
                yield return new WaitForSeconds(waitWalk);
                sideLeft = false;
                StartCoroutine("States", "Wait");
                break;

            case "Wait":
                animatorComponent.Play("Idle");
                yield return new WaitForSeconds(1);
                StartCoroutine("States", "Hit");
                break;

            case "Hit":
                Debug.Log("Hit");
                yield return new WaitForSeconds(waitBeforeHit);
                animatorComponent.Play("Kick");

                yield return new WaitForSeconds(waitHit);
                ApplyKickDamages();
                dashZone.SetActive(true);
                hitCircle.SetActive(true);

                yield return new WaitForSeconds(waitAfterHit);
                StartCoroutine("States", "Idle");


                break;
        }
    }
}