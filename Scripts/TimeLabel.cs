using System.Globalization;
using UnityEngine;
using UnityEngine.UI;


public class TimeLabel : MonoBehaviour
{
    private Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private void FixedUpdate()
    {
        text.text = "Time Scale: "+ Simulation.simulation.timeScale.ToString("g2");
    }
}
