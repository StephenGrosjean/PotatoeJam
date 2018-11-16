using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Utility to play a gif on an object
/// </summary>
public class GifPlay : MonoBehaviour {
    [SerializeField] private Sprite[] frames;
    [SerializeField] private float speed;
    [SerializeField] private bool loop;
    private SpriteRenderer spriteRendererComponent;

    void Start() {
        spriteRendererComponent = GetComponent<SpriteRenderer>();

        //Play the gif frame by frame
        StartCoroutine("Play", speed);
    }

    IEnumerator Play(float speed) {
        //For each sprite in the array, asign it to the sprite renderer
        for (int i = 0; i < frames.Length; i++) {
            yield return new WaitForSeconds(speed);
           spriteRendererComponent.sprite = frames[i];
        }
        if (!loop) {
            Destroy(gameObject);
        }
        else {
            StartCoroutine("Play", this.speed);
        }
    }
}
