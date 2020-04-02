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
    public static readonly float G_BASE = 0.530f;
    public static float G = G_BASE;
    public static Simulation simulation;

    public static Dictionary<GravityBody, List<GravityBody>> orbitingMap =
        new Dictionary<GravityBody, List<GravityBody>>();

    public static Dictionary<GravityBody, float> forceMultMap = new Dictionary<GravityBody, float>();
    public GravityBody centerBody;

    public float timeScale = 1;

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
    public void OnDestroy()
    {
        orbitingMap.Clear();
        forceMultMap.Clear();
        bodies.Clear();

    }

    private void Start()
    {
        setOrbitalVelocity(centerBody);
        
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            //UnityEngine.SceneManagement.SceneManager.LoadScene(1);
            //Cursor.lockState = CursorLockMode.None;
        }

        handleInput();

        foreach (GravityBody body in bodies)
        {
            body.currentForces.Clear();

            body.transform.Rotate(body.getRotationalAxisVector(), Time.deltaTime * body.angularVelocity);
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
                    float forceMultiplier = 1f;

                    //Checking if orbitingmap of bodies[i] contains bodies[j]
                    if (orbitingMap.ContainsKey(bodies[i]))
                    {
                        if (orbitingMap[bodies[i]].Contains(bodies[j]))
                        {
                            if (forceMultMap.ContainsKey(bodies[i]))
                                forceMultiplier = forceMultMap[bodies[i]];
                        }
                    }
                    //Checking if orbitingmap of bodies[j] contains bodies[i]
                    else if (orbitingMap.ContainsKey(bodies[j]))
                    {
                        if (orbitingMap[bodies[j]].Contains(bodies[i]))
                        {
                            if (forceMultMap.ContainsKey(bodies[j]))
                                forceMultiplier = forceMultMap[bodies[j]];
                        }
                    }


                    Vector3 vector = bodies[i].transform.position - bodies[j].transform.position;

                    //divide by R^3 to convert vector to unit vector.
                    Vector3 force = vector * (G * forceMultiplier * bodies[i].getMass() * bodies[j].getMass()) /
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
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            Application.Quit();
        }
    }

    public void setTimeScale(float f)
    {
        timeScale = f;

        G = G_BASE * f;
        
        foreach (GravityBody body in bodies)
        {
            float factor = (float) Math.Sqrt(f/body.LastMultiplier);

            body.angularVelocity = body.angularVelocity * factor;
            body.rigidBody.velocity = body.rigidBody.velocity * factor;
            body.LastMultiplier = f;
        }
    }




    private void setOrbitalVelocity(GravityBody body)
    {
        if (!orbitingMap.ContainsKey(body)) return;

        foreach (GravityBody orbitingBody in orbitingMap[body])
        {
            Vector3 vector = orbitingBody.transform.position - body.transform.position;

            float forceMultiplier = 1f;

            if (forceMultMap.ContainsKey(body))
            {
                forceMultiplier = forceMultMap[body];
            }

            float initialDisplacementY =
                (float) (Math.Tan(orbitingBody.inclination * Math.PI / 180) * vector.magnitude) + body.InitialDisplacementY;
            orbitingBody.InitialDisplacementY = initialDisplacementY;
            orbitingBody.transform.Translate(Vector3.up * initialDisplacementY);
            
            vector = orbitingBody.transform.position - body.transform.position;
            Vector3 orbitNormal = Vector3.Cross(new Vector3(0, 1, 0), vector);

            orbitingBody.circularOrbit = orbitingBody.circularOrbit || orbitingBody.semiMajor == Vector3.zero;

            if (orbitingBody.circularOrbit)
            {
                orbitingBody.semiMajor = vector;
            }

            orbitingBody.Perihelion = vector.magnitude;
            orbitingBody.Apohelion = (orbitingBody.semiMajor.magnitude * 2) - vector.magnitude;

            float relativeVelocity =
                (float) Math.Sqrt(
                    (Simulation.G * 2 * forceMultiplier * (orbitingBody.getMass() + body.getMass()) * orbitingBody.Apohelion) /
                    (orbitingBody.Perihelion * (orbitingBody.Perihelion + orbitingBody.Apohelion)));
            float inverseMassRatio = (1 - (orbitingBody.getMass() / (orbitingBody.getMass() + body.getMass())));


            orbitingBody.rigidBody.velocity = (orbitNormal.normalized * (relativeVelocity * inverseMassRatio)) +
                                              body.initialVelocity;
            orbitingBody.initialVelocity = orbitingBody.rigidBody.velocity;

            body.rigidBody.velocity -= orbitNormal.normalized * (relativeVelocity * (1 - inverseMassRatio));
            body.initialVelocity = body.rigidBody.velocity;

            orbitingBody.transform.Rotate(orbitNormal, orbitingBody.axialTilt);
            orbitingBody.setRotationAxisVector(Vector3.up);

            setOrbitalVelocity(orbitingBody);
        }


    }

   
}