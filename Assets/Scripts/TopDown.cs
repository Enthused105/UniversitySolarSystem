using System;
using UnityEngine;

public class TopDown : CameraMode{
    
    public float distance;
    public Rigidbody followPlanet;


    private void FixedUpdate()
    {
        transform.position = followPlanet.position + Vector3.up * distance;
        transform.LookAt(followPlanet.position);
    }
}