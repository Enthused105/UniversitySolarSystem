using System;
using UnityEngine;

public class GravityBodyLightSource : MonoBehaviour
{
    public GravityBody followBody;


    private void FixedUpdate()
    {
        transform.position = followBody.transform.position;
    }
}