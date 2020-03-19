using System;
using UnityEngine;


public class ShipCamera : CameraMode
{
    public ShipController ship;
    public float distance;

    public void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position,
            ship.transform.position - ship.transform.forward * distance, 0.1f);
        transform.LookAt(ship.transform.position);
    }
}