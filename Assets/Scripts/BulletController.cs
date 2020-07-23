using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public Rigidbody bulletRigidbody;
    public BulletSO bulletStats;

    private float lifeTime = 10f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hit");
        var gameObjectTransform = collision.gameObject.transform;
        Transform targetTransform = gameObjectTransform;

        while(gameObjectTransform != null)
        {
            targetTransform = gameObjectTransform;
            gameObjectTransform = gameObjectTransform.parent;
        }

        var targetGameObject = targetTransform.gameObject;
        var zombieController = targetGameObject.GetComponent<ZombieController>();

        if (zombieController != null)
        {
            zombieController.SetHit();
        }
        else
        {
            Debug.Log("Non zombie-object hit");
        }

        var hitGO = collision.gameObject;
        var rb = hitGO.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForceAtPosition(bulletRigidbody.velocity * bulletStats.forceMultiplyer, collision.contacts[0].point);
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
