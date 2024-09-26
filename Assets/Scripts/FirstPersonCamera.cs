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

    void LateUpdate()
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
        transform.rotation = playerBody.rotation;


    }

    private void MovePlayer()
    {
        // Get input for movement
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Create a movement vector based on camera direction
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Calculate movement direction
        Vector3 moveDir = (forward * moveZ + right * moveX).normalized;

        // Scale the movement direction by the movement speed
        Vector3 velocity = moveDir * movementSpeed * Time.fixedDeltaTime;

        // Move the player
        playerBody.position += moveDir * movementSpeed * Time.deltaTime;

        float horizontalInput = Input.GetAxis("Mouse X");

        // Calculate rotation angle
        float rotationY = horizontalInput * mouseSensitivity;

        // Rotate the player around the camera
        playerBody.Rotate(0, rotationY, 0);

        // Update offset to maintain the distance while allowing for rotation
        Quaternion rotation = Quaternion.Euler(0, rotationY, 0);
        transform.rotation = playerBody.rotation;

        // Smoothly move the player
        playerBody.GetComponent<Rigidbody>().MovePosition(transform.position + velocity);

    }
}
