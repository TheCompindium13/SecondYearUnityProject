using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        if(m_Length == 0)
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
        // Loop through the number of segments
        for (int i = 0; i < m_segmentNumber; i++)
        {
            // Create a new segment
            GameObject m_segment = Instantiate(m_ropePrefab);
            
            // Calculate the position of the segment
            Vector3 m_position = CalculateSegmentPosition(i);
            // Set segment's position
            m_segment.transform.position = m_position;
            // Add segment to the list
            m_segments.Add(m_segment);
        }

    }

    //Calculate the position of each rope segment
    private Vector3 CalculateSegmentPosition(int index)
    {
        //Calculate the position based on the index and segment length
        return new Vector3(0, index * m_Length, 0);
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
    // Check for input to modify the rope (add/remove segments)

    //Add a segment to the rope
    private void AddSegment()
    {
        for (int i = 0; i < m_segmentNumber;)
        {
            // Create a new segment
            GameObject m_segment = Instantiate(m_ropePrefab);

            // Calculate the position of the segment
            Vector3 m_position = CalculateSegmentPosition(i);
            // Set segment's position
            m_segment.transform.position = m_position;
            // Add segment to the list
            m_segments.Add(m_segment);
        }

    }

    //Remove the last segment of the rope
    private void RemoveSegment()
    {
        // Check if there are segments to remove
        if (m_segments.Count <= 0)
        {
            // Destroy the last segment
            GameObject m_lastSegment = m_segments.Last();
            Destroy(m_lastSegment);
            // Remove it from the list
            m_segments.RemoveAt(m_segments.Count - 1);
        }

    }


}
