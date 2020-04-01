using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraModeController : MonoBehaviour
{
    List<CameraMode> cameraModes = new List<CameraMode>();
    public int currentMode = 0;
    public static CameraModeController mainCameraModeController;
    private List<LockedCamera> lockedCameras;
    private LockedCamera lockedCamera;
    public int currentLockedCamera = 0;
    public Vector3 velocity;
    public Rigidbody rigidBody;
    
    private List<LockedCameraReverse> reverseCameras;
    private LockedCameraReverse reverseCamera;
    public int currentReverseCamera = 0;
    
    public ShipCamera shipCamera;
    
    public void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        if (mainCameraModeController == null)
        {
            mainCameraModeController = this;
        }
        else
        {
            Debug.Log("Only one CameraModeController is allowed.");
            Destroy(this);
            return;
        }

        shipCamera = GetComponent<ShipCamera>();
        lockedCameras = GetComponents<LockedCamera>().ToList();
        reverseCameras = GetComponents<LockedCameraReverse>().ToList();
        cameraModes = GetComponents<CameraMode>().ToList();

        List<CameraMode> newCameraModes = new List<CameraMode>();

        foreach(CameraMode mode in cameraModes)
        {
            if (!(mode is LockedCamera || mode is LockedCameraReverse))
            {
                newCameraModes.Add(mode);
            }
            mode.enabled = false;
        }

        if (lockedCameras.Count > 0) newCameraModes.Add(lockedCameras[0]);
        
        if (reverseCameras.Count >0) newCameraModes.Add(reverseCameras[0]);
        

        cameraModes[currentMode].enabled = true;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(currentMode);
            transform.position = Vector3.zero;
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
        } else if (Input.GetMouseButtonDown(0))
        {
            if (cameraModes[currentMode] is LockedCamera)
            {
                currentLockedCamera++;
                if (currentLockedCamera >= lockedCameras.Count)
                {
                    currentLockedCamera = 0;
                }
                cameraModes[currentMode].enabled = false;
                cameraModes[currentMode] = lockedCameras[currentLockedCamera];
                cameraModes[currentMode].enabled = true;
            } else if (cameraModes[currentMode] is LockedCameraReverse)
            {
                currentReverseCamera++;
                if (currentReverseCamera >= reverseCameras.Count)
                {
                    currentReverseCamera = 0;
                }
                cameraModes[currentMode].enabled = false;
                cameraModes[currentMode] = reverseCameras[currentReverseCamera];
                cameraModes[currentMode].enabled = true;
                
            }
        } else if (Input.GetMouseButtonDown(1))
        {
            
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && Simulation.simulation.timeScale < 4000)
        {
            Simulation.simulation.setTimeScale(Simulation.simulation.timeScale*2);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Simulation.simulation.setTimeScale(Simulation.simulation.timeScale / 2);

        }
    }
}