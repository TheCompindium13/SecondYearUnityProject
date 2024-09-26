using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    // Reference to the player character
    [SerializeField]
    private Transform player;

    // Distance from the player
    [SerializeField]
    private float distance = 5.0f;

    // Height offset from the player's position
    [SerializeField]
    private float height = 2.0f;

    // Camera smoothness factor
    [SerializeField]
    private float smoothSpeed = 0.125f;

    // Speed of camera rotation
    public float rotationSpeed = 5.0f;

    private Vector3 offset;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Start()
    {
        // Calculate the initial offset based on the player's position, distance, and height
        ResetOffset();
    }

    void LateUpdate()
    {
        // Calculate desired position based on player's position and offset
        Vector3 desiredPosition = player.position + offset;

        // Smoothly interpolate towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Rotate the camera to look at the player
        transform.LookAt(player.position);

        // Handle camera rotation based on input
        HandleCameraRotation();
    }

    private void HandleCameraRotation()
    {
        // Get mouse input for camera rotation
        float horizontalInput = Input.GetAxis("Mouse X");

        // Calculate rotation angle
        float rotationY = horizontalInput * rotationSpeed;

        // Rotate the player around the camera
        player.Rotate(0, rotationY, 0);

        // Update offset to maintain the distance while allowing for rotation
        Quaternion rotation = Quaternion.Euler(0, rotationY, 0);
        offset = rotation * offset; // Update the offset based on rotation
    }

    public void ResetOffset()
    {
        // Reset the camera offset based on current player position
        offset = new Vector3(0, height, -distance);
    }
}