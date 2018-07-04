using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

    public float xRotation = -5f;
    public float zRotation = -5f;
    public float angularVelocity = 100f;

    private float angularPosition;
	
	void Update () {
        float angularOffset = angularVelocity * Time.deltaTime;
        angularPosition += angularOffset;
        transform.rotation = Quaternion.Euler(xRotation, angularPosition, zRotation);
    }
}
