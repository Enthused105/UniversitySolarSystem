using System;
using UnityEngine;


public class SpiralOut : CameraMode
{
    public Rigidbody lookAtPlanet;
    public Vector3 movementDirection;
    public Vector3 prevPosition;
    public float spiralSpeed;
    public float spiralAngle;
    private bool spiralingOut = true;
    private void FixedUpdate()
    {
        float distanceToPlanet = (transform.position - lookAtPlanet.transform.position).magnitude;
        if (distanceToPlanet > 3000 && spiralingOut)
        {
            spiralingOut = false;
        }
        else if (distanceToPlanet < 10 && !spiralingOut)
        {
            spiralingOut = true;
        }
        if (spiralingOut)
        {
            transform.RotateAround(lookAtPlanet.position, Vector3.up, spiralAngle * Time.deltaTime);
            transform.LookAt(lookAtPlanet.position);
            transform.Translate(Vector3.back * spiralSpeed * Time.deltaTime);
        }

        else 
        {
            transform.RotateAround(lookAtPlanet.position, Vector3.up, spiralAngle * Time.deltaTime);
            transform.LookAt(lookAtPlanet.position);
            transform.Translate(Vector3.back * -spiralSpeed * Time.deltaTime);
        }
    }
}