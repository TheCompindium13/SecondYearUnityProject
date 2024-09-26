using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    // Sensitivity of the mouse movement
    [SerializeField]
    private float mouseSensitivity = 2.0f;

    // Movement speed of the player
    [SerializeField]
    private float movementSpeed = 5.0f;

    // Clamp values for looking up and down
    [SerializeField]
    private float upDownLookLimit = 80.0f;

    // Reference to the player body (for turning)
    [SerializeField]
    private Transform playerBody;

    [SerializeField]
    private Camera m_camera;

    [SerializeField]
    private float smoothSpeed = 0.125f; // Smoothing speed

    // Store the current vertical rotation
    private float verticalRotation = 0.0f;

    void Start()
    {
        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;

        // Set the initial rotation of the camera to match the player's rotation
        transform.rotation = playerBody.rotation;
    }

    void Update()
    {
        if (playerBody != null)
        {
            // Desired position based on player's position
            Vector3 desiredPosition = playerBody.position;
            // Smoothly interpolate between current position and desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            // Update camera position
            transform.position = smoothedPosition;

            // Get mouse input for looking around
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            // Calculate the vertical rotation and clamp it
            verticalRotation -= mouseY;
            verticalRotation = Mathf.Clamp(verticalRotation, -upDownLookLimit, upDownLookLimit);

            // Apply rotation to the camera and player body
            HandleCameraRotation();

            // Handle player movement
            MovePlayer();
        }
    }
    private void HandleCameraRotation()
    {
        // Get mouse input for camera rotation
        float horizontalInput = Input.GetAxis("Mouse X");

        // Calculate rotation angle
        float rotationY = horizontalInput * mouseSensitivity;

        // Rotate the player around the camera
        playerBody.Rotate(0, rotationY, 0);

        // Update offset to maintain the distance while allowing for rotation
        Quaternion rotation = Quaternion.Euler(0, rotationY, 0);
         // Update the offset based on rotation
    }

    private void MovePlayer()
    {
        // Get input for movement
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Calculate the direction to move based on player rotation
        Vector3 move = playerBody.right * moveX + playerBody.forward * moveZ;
        move.y = 0; // Prevent vertical movement

        // Move the player
        playerBody.position += move * movementSpeed * Time.deltaTime;
    }
}
