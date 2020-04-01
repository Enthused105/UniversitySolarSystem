using System;
using UnityEngine;


public class Rings : MonoBehaviour
{
    public GravityBody body;
    Rigidbody rigidbody;

    public void Awake()
    {
        rigidbody = body.GetComponent<Rigidbody>();
    }
    public void FixedUpdate()
    {
        transform.position = body.transform.position + (rigidbody.velocity * Time.deltaTime);
    }
}