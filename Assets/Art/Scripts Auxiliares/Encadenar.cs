using System.Collections;
using System.Collections.Generic;
//using System.Runtime.Remoting.Metadata.W3cXsd2001;
using UnityEngine;

public class Encadenar : MonoBehaviour
{
    public GameObject target;
    float radius = 5f; // Changed "integer" to "float" for radius
    float strength = 50f; // Changed "SoapInteger" to "float" for strength

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the distance between this object and the target
        float distance = Vector3.Distance(transform.position, target.transform.position);

        // Check if the distance is greater than the specified radius
        if (distance > radius)
        {
            // Calculate the force vector towards the target and scale it by strength
            Vector3 forceDirection = (target.transform.position - transform.position).normalized;
            Vector3 force = forceDirection * strength;

            // Apply the force to the Rigidbody component of this object
            GetComponent<Rigidbody>().AddForce(force);
        }
    }
}