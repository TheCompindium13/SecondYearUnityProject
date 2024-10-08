using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] waypoints; // Waypoints for wandering
    public Transform player; // Reference to the player
    public float chaseDistance = 10f; // Distance to start chasing
    public float idleTime = 2f; // Time spent in idle state

    private NavMeshAgent agent;
    private int currentWaypointIndex = 0;
    private float idleTimer = 0f;

    private enum State { Idle, Wandering, Chasing }
    private State currentState = State.Idle;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (waypoints.Length > 0)
        {
            SetNextWaypoint();
        }
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                HandleIdle();
                break;
            case State.Wandering:
                HandleWandering();
                break;
            case State.Chasing:
                HandleChasing();
                break;
        }

        CheckDistanceToPlayer();
    }

    private void HandleIdle()
    {
        idleTimer += Time.deltaTime;
        if (idleTimer >= idleTime)
        {
            currentState = State.Wandering;
            idleTimer = 0f;
        }
    }

    private void HandleWandering()
    {
        if (agent.remainingDistance < 0.5f)
        {
            SetNextWaypoint();
        }
    }

    private void HandleChasing()
    {
        agent.SetDestination(player.position);
    }

    private void CheckDistanceToPlayer()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance < chaseDistance)
        {
            currentState = State.Chasing;
        }
        else if (currentState == State.Chasing)
        {
            currentState = State.Idle;
            idleTimer = 0f;
        }
    }

    private void SetNextWaypoint()
    {
        agent.SetDestination(waypoints[currentWaypointIndex].position);
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }
}