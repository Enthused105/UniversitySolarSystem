using System;
using UnityEngine;
using UnityEngine.UI;


public class DateLabel : MonoBehaviour
{
    private DateTime time;
    private Text text;

    private void Awake()
    {
        time = DateTime.Now;
        text = GetComponent<Text>();
    }

    private void FixedUpdate()
    {
        time = time.AddDays(Time.deltaTime * 3.65 * Math.Sqrt(Simulation.simulation.timeScale));
        this.text.text = time.ToString();
    }
}