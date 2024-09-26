using System.Collections.Generic;
using UnityEngine;

public class RopeBuilder : MonoBehaviour
{
    // Variables to hold rope properties
    private List<GameObject> m_segments;
    [SerializeField]
    private GameObject m_ropePrefab;
    [SerializeField]
    private float m_Length;
    [SerializeField]
    private int m_segmentNumber;

    // Start called when the script instance is being loaded
    private void Start()
    {
        // Initialize rope segments list
        m_segments = new List<GameObject>();

        // Set segment length and number of segments
        if (m_Length == 0)
        {
            m_Length = 1;
        }
        if (m_segmentNumber == 0)
        {
            m_segmentNumber = 1;
        }

        // Build the rope
        BuildRope();
    }

    // Build the rope
    private void BuildRope()
    {
        for (int i = 0; i < m_segmentNumber; i++)
        {
            // Create a new segment
            GameObject m_segment = Instantiate(m_ropePrefab);

            // Calculate the position of the segment
            Vector3 m_position = CalculateSegmentPosition(i);
            m_segment.transform.position = m_position;

            // Make the first segment kinematic
            if (i == 0)
            {
                m_segment.GetComponent<Rigidbody>().isKinematic = true;
            }

            // Add the segment to the list
            m_segments.Add(m_segment);

            // Connect to the previous segment if it's not the first one
            if (i > 0)
            {
                ConnectSegments(m_segments[i - 1], m_segment);
            }
        }
    }

    // Calculate the position of each rope segment
    private Vector3 CalculateSegmentPosition(int index)
    {
        return new Vector3(0, index * m_Length, 0);
    }

    // Connect two segments with a joint
    private void ConnectSegments(GameObject previousSegment, GameObject currentSegment)
    {
        FixedJoint joint = currentSegment.AddComponent<FixedJoint>();
        joint.connectedBody = previousSegment.GetComponent<Rigidbody>();
    }

    // Optional: Update rope dynamically (e.g., on user input)
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            AddSegment();
        }
        if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            RemoveSegment();
        }
    }

    // Add a segment to the rope
    private void AddSegment()
    {
        int index = m_segments.Count;

        // Create a new segment
        GameObject m_segment = Instantiate(m_ropePrefab);

        // Calculate the position of the segment
        Vector3 m_position = CalculateSegmentPosition(index);
        m_segment.transform.position = m_position;

        // Make the new segment dynamic
        m_segment.GetComponent<Rigidbody>().isKinematic = false;

        // Add the new segment to the list
        m_segments.Add(m_segment);

        // Connect to the previous segment
        if (index > 0)
        {
            ConnectSegments(m_segments[index - 1], m_segment);
        }
    }

    // Remove the last segment of the rope
    private void RemoveSegment()
    {
        // Check if there are segments to remove
        if (m_segments.Count > 0)
        {
            // Destroy the last segment
            GameObject m_lastSegment = m_segments[m_segments.Count - 1];
            Destroy(m_lastSegment);
            m_segments.RemoveAt(m_segments.Count - 1);

            // Reconnect the last remaining segment to the one before it if necessary
            if (m_segments.Count > 0)
            {
                // You might want to add logic to reconnect the remaining segments here
                // This could involve removing the joint from the last segment and reconnecting it to the new last segment
            }
        }
    }
}