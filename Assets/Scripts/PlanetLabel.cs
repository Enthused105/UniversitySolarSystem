using System;
using UnityEngine;
using UnityEngine.UI;

public class PlanetLabel:MonoBehaviour
{
    private Text label;
    public GravityBody followTarget;

    public void Awake()
    {
        label = GetComponent<Text>();
    }

    public void FixedUpdate()
    {
        label.transform.position = Camera.main.WorldToScreenPoint(followTarget.transform.position);
    }
}