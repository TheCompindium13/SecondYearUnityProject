using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Headtracking : MonoBehaviour
{
    public Transform player; // The player's transform to track
    public Transform head;   // The head transform of the ragdoll
    public float trackingSpeed = 5f; // Speed of head tracking
    public float raycastDistance = 10f; // Distance to check for line of sight

    void Update()
    {

        if (player != null && head != null)
        {
            // Calculate the direction to the player
            Vector3 direction = player.position - head.position;
            Debug.DrawRay(head.position, direction.normalized * raycastDistance, Color.red);

            direction.y = 0; // Keep the head horizontal

            // Perform the raycast to check for line of sight
            RaycastHit hit;
            if (Physics.Raycast(head.position, direction.normalized, out hit, raycastDistance))
            {
                // If the ray hits the player, track the player
                if (hit.transform == player)
                {
                    // Calculate the rotation
                    Quaternion lookRotation = Quaternion.LookRotation(direction);

                    // Smoothly rotate the head towards the target
                    head.rotation = Quaternion.Slerp(head.rotation, lookRotation, trackingSpeed * Time.fixedDeltaTime);

                }
            }
        }
    }
}
