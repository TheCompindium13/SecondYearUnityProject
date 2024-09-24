using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementBehaviour : MonoBehaviour
{
    [Tooltip("How fast the player will move.")]
    [SerializeField]
    private float _moveSpeed;

    [Tooltip("The current active camera. Used to get mouse position for rotation.")]
    [SerializeField]
    private Camera _camera;

    private Rigidbody _rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        // Store reference to the attached Rigidbody
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Get input values for movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Create a movement vector based on camera direction
        Vector3 forward = _camera.transform.TransformDirection(Vector3.forward);
        Vector3 right = _camera.transform.TransformDirection(Vector3.right);

        // Calculate movement direction
        Vector3 moveDir = (forward * verticalInput + right * horizontalInput).normalized;

        // Scale the movement direction by the movement speed
        Vector3 velocity = moveDir * _moveSpeed * Time.deltaTime;

        // Smoothly move the player
        _rigidbody.MovePosition(transform.position + velocity);

        // Handle turning with Q and E
        if (Input.GetKey(KeyCode.Q))
        {
            RotatePlayerAndCamera(-5); // Turn left
        }
        if (Input.GetKey(KeyCode.E))
        {
            RotatePlayerAndCamera(5); // Turn right
        }

        
    }

    private void RotatePlayerAndCamera(float angle)
    {
        Quaternion rotation = Quaternion.Euler(0, angle, 0);
        _rigidbody.MoveRotation(_rigidbody.rotation * rotation);

        // Rotate the camera (assuming it's a child of the player)
        _camera.transform.rotation *= rotation;
    }
}
