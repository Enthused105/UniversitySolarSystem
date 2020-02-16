using System;
using UnityEngine;
using UnityEngine.UI;

public class PlanetLabel:MonoBehaviour
{
    private Text label;

    public string name;
    public GravityBody followTarget;

    public void Awake()
    {
        label = GetComponent<Text>();
        label.text = name;
    }

    public void FixedUpdate()
    {
        label.transform.position = Camera.main.WorldToScreenPoint(followTarget.transform.position);
    }
}