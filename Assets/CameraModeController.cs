using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraModeController : MonoBehaviour
{
    List<CameraMode> cameraModes = new List<CameraMode>();
    public int currentMode = 0;
    
    public void Awake()
    {

        cameraModes = GetComponents<CameraMode>().ToList();
        
        foreach(CameraMode mode in cameraModes)
        {
            mode.enabled = false;
        }

        cameraModes[currentMode].enabled = true;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentMode++;
            if (currentMode >= cameraModes.Count)
            {
                currentMode = 0;
            }
            
            for (int i = 0; i < cameraModes.Count; i++)
            {
                if (i == currentMode)
                {
                    cameraModes[i].enabled = true;
                }
                else
                {
                    cameraModes[i].enabled = false;
                }
            }
        }
    }
}