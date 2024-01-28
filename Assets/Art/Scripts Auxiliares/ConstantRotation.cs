using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ConstantRotation : MonoBehaviour
{
    private Quaternion initialRotation;

    // Start is called before the first frame update
    void Start()
    {
        // Capture the initial rotation of the object
        initialRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        // Update the rotation of the object in each frame to be equal to the initial rotation
        transform.rotation = initialRotation;

        // Optionally, you can add constant rotation here, for example:
        // transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
    }
}

