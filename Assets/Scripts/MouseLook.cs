using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides functionality for InputSystem driven mouse look behaviour. Handles sensitivity, verticle clamping, and rotates the player.
/// </summary>
public class MouseLook : MonoBehaviour
{
    [Tooltip("Rigidbody to rotate when the mouse moves")]
    [SerializeField] private Rigidbody _playerBody;

    [Tooltip("Multiplier for mouse input")]
    [SerializeField] private float _sensitivity = 10f;

    [Tooltip("How far to allow the camera to look up and down")]
    [SerializeField] private float _clampAngle = 90f;

    private float _xRotation = 0f;

    private float _deltaXInput;
    private float _deltaYInput;

    private void Awake()
    {
        // Lock cursor to game view
        Cursor.lockState = CursorLockMode.Locked;
        
        // Initialize _xRotation;
        _xRotation = transform.parent.transform.eulerAngles.x;
    }

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        // Set _deltaXInput and _deltaYInput based on how far the mouse has moved this frame
        _deltaXInput = _deltaXInput - Input.mousePosition.x;
        _deltaYInput = _deltaYInput - Input.mousePosition.y;
        // Rotate the parent on the x axis
        _playerBody.MoveRotation(Quaternion.Euler(_playerBody.rotation.eulerAngles + (Vector3.up * _deltaXInput * Time.deltaTime)));

        // Calculate and clamp _xRotation
        _xRotation -= _deltaYInput * Time.deltaTime;
        _xRotation = Mathf.Clamp(_xRotation, -_clampAngle, _clampAngle);

        // Rotate the camera
        transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
    }
}
