using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
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
    private Rigidbody _rigidbody;  // Reference to the Rigidbody component

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
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
                _rigidbody.isKinematic = true;
            }

            // Add the segment to the list
            m_segments.Add(m_segment);

            // Connect to the previous segment if it's not the first one
            if (i > 0)
            {
                _rigidbody.isKinematic = false;
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
        SpringJoint joint = currentSegment.GetComponent<SpringJoint>();
        joint.connectedBody = previousSegment.GetComponent<Rigidbody>();
    }

    // Optional: Update rope dynamically (e.g., on user input)
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Plus))
        {
            AddSegment();
        }
        if (Input.GetKeyDown(KeyCode.Minus) )
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
            // Get the last segment and its index
            GameObject m_lastSegment = m_segments[m_segments.Count - 1];
            int lastIndex = m_segments.Count - 1;

            // If there are at least two segments
            if (lastIndex > 0)
            {
                // Get the previous segment
                GameObject m_previousSegment = m_segments[lastIndex - 1];
                SpringJoint lastJoint = m_lastSegment.GetComponent<SpringJoint>();

                // Remove the joint from the last segment
                if (lastJoint != null)
                {
                    Destroy(lastJoint);
                }

                // Optionally, add a new joint to reconnect the previous segment to the last remaining segment
                SpringJoint newJoint = m_previousSegment.GetComponent<SpringJoint>();
                newJoint.connectedBody = m_previousSegment.GetComponent<Rigidbody>();
                newJoint.spring = 1000f; // Adjust the spring value as needed
                newJoint.damper = 100f;  // Adjust the damper value as needed
                newJoint.autoConfigureConnectedAnchor = false; // Configure anchors manually if needed
                newJoint.anchor = Vector3.zero; // Set anchor for previous segment
                newJoint.connectedAnchor = new Vector3(0, m_Length, 0); // Set anchor for current segment
            }

            // Destroy the last segment and remove it from the list
            Destroy(m_lastSegment);
            m_segments.RemoveAt(lastIndex);
        }
    }
}