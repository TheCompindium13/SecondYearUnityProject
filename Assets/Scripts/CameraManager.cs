using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera primaryCamera; // Reference to your primary camera

    void Start()
    {
        // Set the primary camera as the active camera at the start
        if (primaryCamera != null)
        {
            Camera.main.gameObject.SetActive(false); // Disable the current main camera
            primaryCamera.gameObject.SetActive(true); // Enable your primary camera
        }
    }

    void Update()
    {
        // Optionally, you can check if a new camera is added and reset to the primary camera
        if (Camera.allCameras.Length > 0)
        {
            foreach (Camera cam in Camera.allCameras)
            {
                if (cam != primaryCamera)
                {
                    cam.gameObject.SetActive(false); // Disable any other cameras
                }
            }
            primaryCamera.gameObject.SetActive(true); // Ensure primary camera is active
        }
    }
}
