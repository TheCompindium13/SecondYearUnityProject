using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MultipleTargetCamera : MonoBehaviour
{
    [SerializeField]
    private List<Transform> targets;  // List of targets to be tracked by the camera
    [SerializeField]
    private Vector3 offset;  // Offset for the camera position

    [SerializeField]
    private float smoothTime = .5f;  // Smooth time for camera movement
    [SerializeField]
    private float minZoom = 40f;  // Minimum zoom level
    [SerializeField]
    private float maxZoom = 10f;  // Maximum zoom level
    [SerializeField]
    private float zoomLimiter = 50f;  // Zoom limit
    private Vector3 velocity;  // Velocity of the camera
    private Camera cam;  // Reference to the camera component

    private void Start()
    {
        cam = GetComponent<Camera>();  // Get the camera component
    }

    private void LateUpdate()
    {
        if (targets.Count == 0)
        {
            return;  // If there are no targets, return
        }
        Move();  // Move the camera
        Zoom();  // Zoom the camera
    }

    void Zoom()
    {
        // Calculate the new zoom level based on the greatest distance between targets
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter);
        // Lerp the camera's field of view to the new zoom level
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
    }

    float GetGreatestDistance()
    {
        // Create a new bounds object at the position of the first target
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            // Expand the bounds to encapsulate each target's position
            bounds.Encapsulate(targets[i].position);
        }
        // Return the size of the bounds along the x-axis
        return bounds.size.x;
    }

    void Move()
    {
        // Get the center point of all targets
        Vector3 centerPoint = GetCenterPoint();
        // Calculate the new position of the camera
        Vector3 newPosition = centerPoint + offset;
        // Smoothly move the camera to the new position
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
        {
            // If there is only one target, return its position
            return targets[0].position;
        }
        // Create a new bounds object at the position of the first target
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            // Expand the bounds to encapsulate each target's position
            bounds.Encapsulate(targets[i].position);
        }
        // Return the center of the bounds
        return bounds.center;
    }
}