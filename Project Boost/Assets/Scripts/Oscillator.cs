using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour {
    private Vector3 startPosition;

    float currentTime = 0f;
    
    public int movementFactor = 3;
    public int angularSpeed = 2;

    public Vector3 movementDirection = new Vector3(0f, 0f, 0f);

    public float periodOffset = 0f;
    
    void Start ()
    {
        startPosition = transform.position;
    }
	
	void Update ()
    {
        currentTime = currentTime + Time.deltaTime;

        Vector3 offset = movementFactor * movementDirection * Mathf.Sin(angularSpeed * currentTime + periodOffset);

        transform.position = startPosition + offset;
    }
}
