using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleRotator : MonoBehaviour {
    
    public float angularVelocity = 100f;

    private float angularPosition;

    private float xRotation;
    private float zRotation;

    private void Start()
    {
        xRotation = 0f;
        zRotation = 0f;
    }

    void Update () {
        float angularOffset = angularVelocity * Time.deltaTime;
        angularPosition += angularOffset;
        transform.rotation = Quaternion.Euler(xRotation, angularPosition, zRotation);
    }
}
