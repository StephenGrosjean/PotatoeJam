using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script to enable the Tip popup
/// </summary>
public class Popup : MonoBehaviour {

    public Animator AnimatorComponent;
    private const float WaitTimeUntilDestruction = 6.2f;

    private void OnEnable() {
        AnimatorComponent = GetComponent<Animator>();
        Pop();
    }

    private void Pop() {
       AnimatorComponent.Play("Pop");
        StartCoroutine("Kill");
    }

    IEnumerator Kill() {
        yield return new WaitForSeconds(WaitTimeUntilDestruction);
        Destroy(gameObject);
    }
}
