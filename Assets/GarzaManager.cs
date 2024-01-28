using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;


public class GarzaManager : MonoBehaviour
{
    public float rateMin = 0.3f; // Make sure to use 'f' for float literals
    public float rateMax = 1.5f;
    public GameObject visual;
    private Vector3 initialPos;

    // Start is called before the first frame update
    void Start()
    {
        // Call the Caer function at the beginning
        initialPos = transform.position;
        Caer();
    }

    void Caer()
    {
        // Move visual downwards 11 along the Y-axis (make it in 0.7 seconds)
        StartCoroutine(MoveVisualDown(11f, 0.7f));
        // Randomly choose a position between -5 and 5 on the X and Z axes
        Vector3 randomPosition = new Vector3(UnityEngine.Random.Range(-5f, 5f), transform.position.y, UnityEngine.Random.Range(-5f, 5f));

        // Set the object's position to the randomly chosen position
        transform.position = initialPos+randomPosition;



    }

    void waitToFallAgain() {
        // Wait between rateMin and rateMax to fall again
        yield return new WaitForSeconds(UnityEngine.Random.Range(rateMin, rateMax));

        // Call the Caer function to make it fall again
        Caer();
    }

    IEnumerator MoveVisualDown(float distance, float duration)
    {
        // Disable the visual object while moving down
        //visual.SetActive(false);

        // Calculate the target position
        Vector3 targetPosition = transform.position - new Vector3(0f, distance, 0f);

        // Move the visual object downward
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Impact: Enable the visual object and move it back up
        //visual.SetActive(true);
        yield return new WaitForSeconds(0.5f); // Adjust the delay as needed

        
    }

    // Update is called once per frame
    void Update()
    {
        // You can add any additional update logic if needed
    }
}