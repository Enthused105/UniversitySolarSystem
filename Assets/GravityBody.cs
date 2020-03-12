using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBody : MonoBehaviour
{
    private float mass;
    private Vector3 velocity;

    public Rigidbody rigidBody;
    private Transform bodyTransform;
    public Vector3 initialVelocity;

    public GravityBody orbitTarget;

    public Dictionary<GravityBody, Vector3> currentForces = new Dictionary<GravityBody, Vector3>();

    public float angularVelocity;
    public float axialTilt;

    private Vector3 orbitNormal;

    public bool geostationaryOrbit;
    
    private Vector3 rotationalAxisVector;
    
    public Vector3 semiMajor;
    public bool circularOrbit;
    private float perihelion;

    private float lastMultiplier = 1;

    public float LastMultiplier
    {
        get => lastMultiplier;
        set => lastMultiplier = value;
    }

    public float Perihelion
    {
        get => perihelion;
        set => perihelion = value;
    }

    private float apohelion;

    public float Apohelion
    {
        get => apohelion;
        set => apohelion = value;
    }

    public float inclination;

    public float forceMultiplier;

    private float initialDisplacementY;

    public float InitialDisplacementY
    {
        get => initialDisplacementY;
        set => initialDisplacementY = value;
    }

    //private float radius;
    

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        bodyTransform = GetComponent<Transform>();

        mass = rigidBody.mass;
        
        Simulation.bodies.Add(this);

        if (orbitTarget != null)
        {
            if (!Simulation.orbitingMap.ContainsKey(orbitTarget))
            {
                Simulation.orbitingMap.Add(orbitTarget, new List<GravityBody>());
            }

            Simulation.orbitingMap[orbitTarget].Add(this);
        }

        if (forceMultiplier > 0)
        {
            Simulation.forceMultMap.Add(this, forceMultiplier);
        }
    }

    private void FixedUpdate()
    {
        
    }

    private void Start()
    {
        if (orbitTarget != null)
        {
            

        }
        else
        {
            rigidBody.velocity = initialVelocity;
        }
    }


    public float getMass()
    {
        return mass;
    }

    public void setRotationAxisVector(Vector3 vector)
    {
        rotationalAxisVector = vector;
    }

    public Vector3 getRotationalAxisVector()
    {
        return rotationalAxisVector;
    }
    
}