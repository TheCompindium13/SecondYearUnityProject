using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMovementBehaviour : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [Tooltip("The object the enemy will be seeking towards.")]
    [SerializeField]
    private GameObject _target;
    [Tooltip("How fast the player will move.")]
    [SerializeField]
    private float _moveSpeed;

    [Tooltip("Jump force for the player.")]
    [SerializeField]
    private float _jumpForce;

    [Tooltip("Reference to the active camera.")]
    [SerializeField]
    private Camera _camera;

    private bool _isGrounded;

    private float _maxVelocity = 10;

    private Animator _animator;
    private NavMeshAgent _navAgent;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _navAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();

    }
    void Start()
    {
        _navAgent = GetComponent<NavMeshAgent>();
        _rigidbody = GetComponent<Rigidbody>();

        _animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update


    private void FixedUpdate()
    {
        //If a target hasn't been set return
        if (!_target)
            return;
        _navAgent.SetDestination(_target.transform.position);

        // Get input values for movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        // Calculate movement direction
        Vector3 moveDir = _target.transform.position - transform.position;

        // Scale the movement direction by the movement speed
        Vector3 velocity = moveDir * _moveSpeed * Time.fixedDeltaTime;

        // Smoothly move the player
        _rigidbody.MovePosition(transform.position + velocity);
        _rigidbody.transform.LookAt(_target.transform, Vector3.up);
        //If the velocity goes over the max magnitude, clamp it
        //If the velocity goes over the max magnitude, clamp it
        //If the velocity goes over the max magnitude, clamp it
        if (_navAgent.velocity.magnitude > _maxVelocity)
            _navAgent.velocity = _navAgent.velocity.normalized * _maxVelocity;
        if (_rigidbody.velocity.magnitude > _maxVelocity)
            _rigidbody.velocity = _rigidbody.velocity.normalized * _maxVelocity;
        _animator.SetFloat("Speed", moveDir.magnitude); // Forward/backward movement
        _animator.SetFloat("Direction", horizontalInput); // Left/right movement
    }

        private void UpdateAnimatorParameters(float verticalInput, float horizontalInput)
    {
        // Update Speed and Direction based on input

    }

}