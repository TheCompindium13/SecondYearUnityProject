using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Reference to the player character
    [SerializeField]
    private Transform player;

    // Third-person camera settings
    [SerializeField]
    private ThirdPersonCamera thirdPersonCamera;

    // First-person camera settings
    [SerializeField]
    private FirstPersonCamera firstPersonCamera;

    // Toggle state
    private bool isThirdPerson = true;
    private bool isFirstPerson = false;
    void Start()
    {
        // Ensure only the third-person camera is active at the start
        SetCameraMode(isThirdPerson);
    }

    void Update()
    {
        // Check for middle mouse button input to toggle camera mode
        if (Input.GetMouseButtonDown(2)) // Middle mouse button
        {
            isThirdPerson = !isThirdPerson;
            SetCameraMode(isThirdPerson);
        }
    }

    private void SetCameraMode(bool thirdPerson)
    {
        // Enable or disable the respective camera scripts
        thirdPersonCamera.enabled = thirdPerson;
        firstPersonCamera.enabled = !thirdPerson;

        // Reset the third-person camera offset if it's activated
        if (thirdPerson)
        {
            thirdPersonCamera.ResetOffset();
        }
    }
}