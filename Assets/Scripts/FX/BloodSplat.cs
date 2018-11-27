using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script to check if the particle hit the ground or a wall and spawn a particle 
/// </summary>

public class BloodSplat : MonoBehaviour {

    [SerializeField] private GameObject bloodFlaque;
    [SerializeField] private int offset = 8;
    private GameObject bloodContainer;


    private ParticleSystem bloodEmitter;

    List<ParticleCollisionEvent> collisionEvents;

    private void Start() {
        bloodEmitter = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
        bloodContainer = GameObject.Find("BloodContainer");
    }

    private void OnParticleCollision(GameObject other) {
        ParticlePhysicsExtensions.GetCollisionEvents(bloodEmitter, other, collisionEvents);

        for(int i = 0; i < collisionEvents.Count; i++) {
            SpawnBloodAt(collisionEvents[i]);
        }

    }


    void SpawnBloodAt(ParticleCollisionEvent particleCollisionEvent) {
        float rdmn = Random.Range(0f, 100f);
        Vector3 intersectPos = particleCollisionEvent.intersection;
        Vector3 bloodPos = new Vector3(intersectPos.x, intersectPos.y-offset, intersectPos.z);
        Vector3 normal = particleCollisionEvent.normal;

        //Check if the normal is at a right angle (approximatly) 
        if(normal.y > 0.9f) {
            GameObject blood = Instantiate(bloodFlaque, bloodPos, Quaternion.identity);
            blood.transform.SetParent(bloodContainer.transform);
        }
        
    }
}
