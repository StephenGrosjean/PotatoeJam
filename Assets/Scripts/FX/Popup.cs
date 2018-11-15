using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script to enable the Tip popup
/// </summary>
public class Popup : MonoBehaviour {
    private enum Animation {Pop,PopLong};

    private Animator AnimatorComponent;
    private const float PopDuration = 6.5f;
    private const float PopLongDuration = 11.5f;
    private float WaitTimeUntilDestruction;

    [SerializeField] private Animation AnimName;
    private string currentAnimation;

    private void OnEnable() {
        currentAnimation = System.Enum.GetName(typeof(Animation), AnimName);
        AnimatorComponent = GetComponent<Animator>();
        if (AnimName == Animation.Pop) {
            WaitTimeUntilDestruction = PopDuration;
        }
        else {
            WaitTimeUntilDestruction = PopLongDuration;
        }

        Pop();
    }

    private void Pop() {
        Debug.Log(currentAnimation);
       AnimatorComponent.Play(currentAnimation.ToString());
        StartCoroutine("Kill");
    }

    IEnumerator Kill() {
        yield return new WaitForSeconds(WaitTimeUntilDestruction);
        Destroy(gameObject);
    }
}
