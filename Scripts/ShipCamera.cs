using System;
using UnityEngine;


public class ShipCamera : CameraMode
{
    public ShipController ship;
    public float distance;
    public float height;

    public void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position,
            ship.transform.position - ship.transform.forward * distance + ship.transform.up * height, 0.1f);
        Vector3 lookAtPosition = ship.transform.position + Vector3.up * 10;
        transform.LookAt(lookAtPosition);
    }
}