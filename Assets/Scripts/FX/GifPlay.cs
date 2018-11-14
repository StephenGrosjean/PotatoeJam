using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Utility to play a gif on an object
/// </summary>
public class GifPlay : MonoBehaviour {
    [SerializeField] private Sprite[] Frames;
    [SerializeField] private float Speed;
    [SerializeField] private bool Loop;
    private SpriteRenderer SpriteRendererComponent;

    void Start() {
        SpriteRendererComponent = GetComponent<SpriteRenderer>();

        //Play the gif frame by frame
        StartCoroutine("Play", Speed);
    }

    IEnumerator Play(float speed) {
        //For each sprite in the array, asign it to the sprite renderer
        for (int i = 0; i < Frames.Length; i++) {
            yield return new WaitForSeconds(speed);
           SpriteRendererComponent.sprite = Frames[i];
        }
        if (!Loop) {
            Destroy(gameObject);
        }
        else {
            StartCoroutine("Play", Speed);
        }
    }
}
