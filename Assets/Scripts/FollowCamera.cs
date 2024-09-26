using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField]
    private Transform target;       // The target the camera will follow
    [SerializeField]
    private Vector3 offset;         // Offset position from the target
    [SerializeField]
    private float smoothSpeed = 0.125f; // Smoothing speed

    void LateUpdate()
    {
        if (target != null)
        {
            // Desired position based on target's position and offset
            Vector3 desiredPosition = target.position + offset;
            // Smoothly interpolate between current position and desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            // Update camera position
            transform.position = smoothedPosition;
        }
    }
}
