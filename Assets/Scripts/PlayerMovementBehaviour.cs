using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerMovementBehaviour : MonoBehaviour
{
    [Tooltip("How fast the player will move.")]
    [SerializeField]
    private float _moveSpeed;

    [Tooltip("Jump force for the player.")]
    [SerializeField]
    private float _jumpForce;

    [Tooltip("Reference to the active camera.")]
    [SerializeField]
    private Camera _camera;

    private bool _isGrounded;
    private Rigidbody _rigidbody;
    private Animator _animator;

    private void Awake()
    {
    }
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update


    private void FixedUpdate()
    {
        // Get input values for movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Create a movement vector based on camera direction
        Vector3 forward = _camera.transform.TransformDirection(Vector3.forward);
        forward.y = 0; // Ignore vertical component
        Vector3 right = _camera.transform.TransformDirection(Vector3.right);
        right.y = 0; // Ignore vertical component

        // Calculate movement direction
        Vector3 moveDir = (forward * verticalInput + right * horizontalInput).normalized;

        // Scale the movement direction by the movement speed
        Vector3 velocity = moveDir * _moveSpeed * Time.fixedDeltaTime;

        // Smoothly move the player
        _rigidbody.MovePosition(transform.position + velocity);

        // Set Animator parameters using switch statement

        if (Input.GetKey(KeyCode.LeftShift))
        {
            verticalInput = Input.GetAxis("Vertical");
        }
        else 
        {
            verticalInput = Mathf.Clamp(verticalInput, 0, .5f);

        }
        UpdateAnimatorParameters(verticalInput, horizontalInput);

        HandleTurning();
        HandleJumping();
    }

    private void UpdateAnimatorParameters(float verticalInput, float horizontalInput)
    {
        // Update Speed and Direction based on input
        _animator.SetFloat("Speed", verticalInput); // Forward/backward movement
        _animator.SetFloat("Direction", horizontalInput); // Left/right movement
    }

    private void HandleTurning()
    {
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

    private void HandleJumping()
    {
        // Handle jumping
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            Jump();
        }
    }

    public void Jump()
    {
        // Apply an upward force to the Rigidbody for jumping
        _animator.SetBool("Jump", true); // Set jumping animation state
        _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        _isGrounded = false; // Set grounded state to false
    }

    private void RotatePlayerAndCamera(float angle)
    {
        Quaternion rotation = Quaternion.Euler(0, angle, 0);
        _rigidbody.MoveRotation(_rigidbody.rotation * rotation);

        // Rotate the camera (assuming it's a child of the player)
        _camera.transform.rotation *= rotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the player is on the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true; // Player is grounded
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        // Check if the player has left the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = false; // Player is no longer grounded
            _animator.SetBool("Jump", false); // Update grounded state in animator
        }
    }
}
