using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiggeronTheInside : MonoBehaviour
{
    public Camera mainCamera; // Reference to the main camera
    public Transform cameraPosition; // Camera position inside the TARDIS
    public float interiorScale = 2f; // Scale for the interior effect
    public float fieldOfView = 60f; // Camera field of view
    public float doorOpenAngle = 90f; // Angle to open doors
    public float doorSpeed = 2f; // Speed to open/close doors
    private bool doorsOpen = false;

    private void Start()
    {
        // Set the camera position inside the TARDIS
        if (mainCamera != null && cameraPosition != null)
        {
            mainCamera.transform.position = cameraPosition.position;
            mainCamera.transform.rotation = cameraPosition.rotation;
            mainCamera.fieldOfView = fieldOfView;
        }

        // Scale the TARDIS to create the illusion of being bigger on the inside
        transform.localScale = new Vector3(interiorScale, interiorScale, interiorScale);
    }

    private void Update()
    {
        // Update camera position
        if (mainCamera != null && cameraPosition != null)
        {
            mainCamera.transform.position = cameraPosition.position;
            mainCamera.transform.rotation = cameraPosition.rotation;
        }

        // Handle door opening/closing with the space key
        if (Input.GetKeyDown(KeyCode.Space))
        {
            doorsOpen = !doorsOpen;
        }

        // Animate doors
        if (doorsOpen)
        {
            transform.Rotate(0, doorOpenAngle * doorSpeed * Time.deltaTime, 0);
        }
        else
        {
            transform.Rotate(0, -doorOpenAngle * doorSpeed * Time.deltaTime, 0);
        }
    }
}
