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
    // Movement speed of the player
    [SerializeField]
    private float movementSpeed = 5.0f;
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Start()
    {
        // Calculate the initial offset based on the player's position, distance, and height
        ResetOffset();
    }

    private void FixedUpdate()
    {
        // Calculate desired position based on player's position and offset
        Vector3 desiredPosition = player.position + offset;

        // Smoothly interpolate towards the desired position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Calculate the direction to the player
        Vector3 direction = player.position - transform.position;
        direction.y = 0; // Ignore vertical direction for horizontal rotation
        if (direction != Vector3.zero)
        {
            // Calculate the target rotation
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Smoothly rotate towards the player
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothSpeed);
        }

        // Get input for movement
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Create a movement vector based on camera direction
        Vector3 forward = player.transform.TransformDirection(Vector3.forward);
        Vector3 right = player.transform.TransformDirection(Vector3.right);

        // Calculate movement direction
        Vector3 moveDir = (forward * moveZ + right * moveX).normalized;

        // Scale the movement direction by the movement speed
        Vector3 velocity = moveDir * movementSpeed * Time.fixedDeltaTime;

        // Move the player
        player.position += velocity;

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
        transform.rotation = player.rotation;
    }
}