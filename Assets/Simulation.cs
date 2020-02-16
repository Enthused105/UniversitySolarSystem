using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Video;
using Debug = UnityEngine.Debug;
using Vector3 = UnityEngine.Vector3;


public class Simulation : MonoBehaviour
{
    public static List<GravityBody> bodies = new List<GravityBody>();
    public static float G = 100000f;
    public static Simulation simulation;
    public static Dictionary<GravityBody, List<GravityBody>> orbitingMap = new Dictionary<GravityBody, List<GravityBody>>();
    public GravityBody centerBody;


    private void Awake()
    {
        //ensure singleton status, then set to static variable
        if (FindObjectsOfType(typeof(Simulation)).Length > 1)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            simulation = this;
        }

    }

    private void FixedUpdate()
    {
        
        handleInput();
        
        foreach(GravityBody body in bodies)
        {
            body.currentForces.Clear();
            
            body.transform.Rotate(body.getRotationalAxisVector(), Time.deltaTime*body.angularVelocity);
            
        }
        
        //loop through all bodies, then loop through all bodies again for each iteration
        for (int i = 0; i < bodies.Count; i++)
        {
            for (int j = 0; j < bodies.Count; j++)
            {
                //ignore case where comparing same object to itself
                if (i == j) continue;
                
                //it's possible that this has already been populated from the other direction
                if (!bodies[i].currentForces.ContainsKey(bodies[j]))
                    {

                        Vector3 vector = bodies[i].transform.position - bodies[j].transform.position;

                        //divide by R^3 to convert vector to unit vector.
                        Vector3 force = vector * (G * bodies[i].getMass() * bodies[j].getMass()) /
                                        (float) Math.Pow(vector.magnitude, 3);
                        bodies[i].currentForces[bodies[j]] = -force;
                        bodies[j].currentForces[bodies[i]] = force;
                        bodies[i].rigidBody.AddForce(-force);
                        bodies[j].rigidBody.AddForce(force);
                    }
                }
        }
    }

    private void handleInput()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

    }
    

    private void Start()
    {
        
        setOrbitalVelocity(centerBody);

    }

    private void setOrbitalVelocity(GravityBody body)
    {
        if (!orbitingMap.ContainsKey(body)) return;
        
        foreach (GravityBody orbitingBody in orbitingMap[body])
        {
            
            Vector3 vector = orbitingBody.transform.position - body.transform.position;
            Vector3 orbitNormal = Vector3.Cross(new Vector3(0,1,0), vector);
            
            float relativeVelocity = (float) Math.Sqrt((Simulation.G * (orbitingBody.getMass() + body.getMass()))/vector.magnitude);
            float inverseMassRatio = (1-(orbitingBody.getMass()/(orbitingBody.getMass()+body.getMass())));

            orbitingBody.rigidBody.velocity = (orbitNormal.normalized * (relativeVelocity * inverseMassRatio)) + body.initialVelocity;
            orbitingBody.initialVelocity = orbitingBody.rigidBody.velocity;

            body.rigidBody.velocity -= orbitNormal.normalized * (relativeVelocity * (1 - inverseMassRatio));
            body.initialVelocity = body.rigidBody.velocity;
            
            orbitingBody.transform.Rotate(orbitNormal, orbitingBody.axialTilt);
            orbitingBody.setRotationAxisVector(Vector3.up);
            
            setOrbitalVelocity(orbitingBody);

        }
    } 
            
}