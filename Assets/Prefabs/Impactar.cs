using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Impactar : MonoBehaviour
{
    public float str = 1000;

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has a Rigidbody
        Rigidbody otherRigidbody = collision.gameObject.GetComponent<Rigidbody>();

        if (otherRigidbody != null)
        {
            // Calculate the direction from the impact point to the other object
            Vector3 impactDirection = collision.contacts[0].point - transform.position;
            impactDirection.Normalize();

            // Apply force to the other object in the calculated direction
            otherRigidbody.AddForce(impactDirection * str);
        }
    }
}