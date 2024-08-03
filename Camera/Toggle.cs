using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSwitcher : MonoBehaviour
{
    public Camera[] cameras; // Array of cameras
    private int currentCameraIndex; // Index of the currently active camera

    private void Start()
    {
        // Enable the first camera
        currentCameraIndex = 0;
        EnableCamera(currentCameraIndex);
    }

    private void Update()
    {
        // Check for input to toggle between cameras
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            // Disable the current camera
            DisableCamera(currentCameraIndex);

            // Increment the camera index
            currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;

            // Enable the next camera
            EnableCamera(currentCameraIndex);
        }
    }

    private void EnableCamera(int index)
    {
        if (index >= 0 && index < cameras.Length)
        {
            Camera camera = cameras[index];
            camera.enabled = true;
        }
    }

    private void DisableCamera(int index)
    {
        if (index >= 0 && index < cameras.Length)
        {
            Camera camera = cameras[index];
            camera.enabled = false;
        }
    }
}
