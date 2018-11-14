using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplat : MonoBehaviour {

    [SerializeField] private GameObject BloodFlaque;
    [SerializeField] private int Offset = 8;
    private GameObject BloodContainer;


    private ParticleSystem BloodEmitter;

    List<ParticleCollisionEvent> collisionEvents;

    private void Start() {
        BloodEmitter = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
        BloodContainer = GameObject.Find("BloodContainer");
    }

    private void OnParticleCollision(GameObject other) {
        ParticlePhysicsExtensions.GetCollisionEvents(BloodEmitter, other, collisionEvents);

        for(int i = 0; i < collisionEvents.Count; i++) {
            SpawnBloodAt(collisionEvents[i]);
        }

    }


    void SpawnBloodAt(ParticleCollisionEvent particleCollisionEvent) {
        float RDMN = Random.Range(0f, 100f);
        Vector3 IntersectPos = particleCollisionEvent.intersection;
        Vector3 BloodPos = new Vector3(IntersectPos.x, IntersectPos.y-Offset, IntersectPos.z);
        Vector3 Normal = particleCollisionEvent.normal;
        if(Normal.y > 0.9f) {
            GameObject Blood = Instantiate(BloodFlaque, BloodPos, Quaternion.identity);
            Blood.transform.SetParent(BloodContainer.transform);
        }
        
    }
}
