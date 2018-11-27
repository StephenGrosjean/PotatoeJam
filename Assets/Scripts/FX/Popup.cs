using System.Collections;
using UnityEngine;
/// <summary>
/// Script to enable the Tip popup
/// </summary>
public class Popup : MonoBehaviour {
    private enum Animation {Pop,PopLong};
    [SerializeField] private Animation animName;

    private Animator animatorComponent;
    private const float PopDuration = 6.5f;
    private const float PopLongDuration = 11.5f;
    private float waitTimeUntilDestruction;
    private string currentAnimation;

    private void OnEnable() {
        currentAnimation = System.Enum.GetName(typeof(Animation), animName);
        animatorComponent = GetComponent<Animator>();

        if (animName == Animation.Pop) {
            waitTimeUntilDestruction = PopDuration;
        }
        else {
            waitTimeUntilDestruction = PopLongDuration;
        }

        Pop();
    }

    private void Pop() {
       animatorComponent.Play(currentAnimation.ToString());
        StartCoroutine("Kill");
    }

    IEnumerator Kill() {
        yield return new WaitForSeconds(waitTimeUntilDestruction);
        Destroy(gameObject);
    }
}
