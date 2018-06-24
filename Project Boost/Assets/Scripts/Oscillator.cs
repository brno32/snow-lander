using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour {
    private Vector3 startPosition;

    float currentTime = 0f;
    
    public int movementFactor = 3;
    public int angularSpeed = 2;

    // Use this for initialization
    void Start ()
    {
        startPosition = transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        currentTime = currentTime + Time.deltaTime;

        Vector3 offset = movementFactor * Vector3.right * Mathf.Sin(angularSpeed * currentTime);

        transform.position = startPosition + offset;
    }
}
