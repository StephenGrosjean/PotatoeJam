using UnityEngine;
/// <summary>
/// Script to make it rain $
/// </summary>
public class Rain : MonoBehaviour {
    [SerializeField] private float rainSpeed;
    [SerializeField] private float tilingX;
    private Renderer meshRenderer;

    private float time = 0;
    
    void Start () {
        meshRenderer = GetComponent<Renderer>();
	}
	
	void Update () {

        time += Time.deltaTime;

        if(meshRenderer.material.mainTextureOffset.x <= -tilingX) {
            meshRenderer.material.mainTextureOffset = Vector2.zero;
            time = 0;
        }
        else {
            meshRenderer.material.mainTextureOffset = new Vector2(time * - rainSpeed, 0); //offset the material texture 
        }
    }
}
