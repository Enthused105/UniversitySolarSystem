using System;
using UnityEngine;

public class SpaceshipLightSource : MonoBehaviour
{
    public Rigidbody followBody;
    public float distance;
    public float height;


    private void FixedUpdate()
    {
        transform.position = followBody.transform.position + ( Vector3.up * height ) + (Vector3.forward * distance);
    }
}