using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovingCamera : MonoBehaviour
{
    public float moveSpeed = 50f;
    public float verticalSpeed = 50f;
    public float rotationSpeed = 0.1f; // Added rotation speed parameter

    private Vector2 movementInput;
    private float verticalInput;
    private bool isRotating = false; // Flag to track if rotation is enabled
    private float rotationX = 0f; // Current rotation around x-axis
    private float rotationY = 0f; // Current rotation around y-axis

    private void OnEnable()
    {
        // Enable the input actions
        InputSystem.EnableDevice(Keyboard.current);
        InputSystem.EnableDevice(Mouse.current); // Enable mouse input
    }

    private void OnDisable()
    {
        // Disable the input actions
        InputSystem.DisableDevice(Keyboard.current);
        InputSystem.DisableDevice(Mouse.current); // Disable mouse input
    }

    private void Update()
    {
        // Read the movement input values
        movementInput = Keyboard.current.wKey.ReadValue() * Vector2.up +
            Keyboard.current.sKey.ReadValue() * Vector2.down +
            Keyboard.current.aKey.ReadValue() * Vector2.left +
            Keyboard.current.dKey.ReadValue() * Vector2.right;

        // Read the vertical input value
        verticalInput = Keyboard.current.eKey.ReadValue() - Keyboard.current.qKey.ReadValue();

        // Move the camera
        Vector3 movement = new Vector3(movementInput.x, 0f, movementInput.y) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);

        // Rotate the camera when right mouse button is clicked
        if (Mouse.current.rightButton.isPressed)
        {
            if (!isRotating)
            {
                isRotating = true;
                rotationX = transform.eulerAngles.x;
                rotationY = transform.eulerAngles.y;
            }

            float mouseY = Mouse.current.delta.y.ReadValue() * rotationSpeed;
            float mouseX = Mouse.current.delta.x.ReadValue() * rotationSpeed;
            rotationX -= mouseY;
            rotationY -= -mouseX;

            transform.rotation = Quaternion.Euler(rotationX, rotationY, transform.eulerAngles.z);
        }
        else
        {
            isRotating = false;
        }

        // Move the camera up or down
        Vector3 verticalMovement = Vector3.up * verticalInput * verticalSpeed * Time.deltaTime;
        transform.Translate(verticalMovement);
    }
}


