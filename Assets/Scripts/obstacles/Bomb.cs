using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float explosionRadius = 5f;
    public float explosionForce = 10f;
    public float explosionDelay = 3f;

    private bool hasExploded = false;

    void Start()
    {
        Invoke("Explode", explosionDelay);
    }

    void Explode()
    {
        if (!hasExploded)
        {
            hasExploded = true;
            // Perform explosion logic here
            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
            foreach (Collider col in colliders)
            {
                Rigidbody rb = col.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
                }
            }
            Destroy(gameObject);
        }
    }
}
