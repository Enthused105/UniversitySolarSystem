using System;
using UnityEngine;

public class RingSpin : MonoBehaviour
{
    public float angularVelocity;
    private Quaternion rotationAxis;
    public Rigidbody rigidBody;

    public void FixedUpdate()
    {
        rotationAxis = GameObject.Find("Saturn").transform.rotation;
        transform.rotation = GameObject.Find("Saturn").transform.rotation;
            
    }

}