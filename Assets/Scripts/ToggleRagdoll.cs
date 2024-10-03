using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleRagdoll : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Animator _animator;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    void Start()
    {
        RagdollToggle(false);
        Invoke(nameof(SetRagdoll), 3);
        _animator = GetComponent<Animator>();
    }
    private void SetRagdoll() => RagdollToggle(true);
    public void RagdollToggle(bool enabled)
    {
        TryGetComponent(out Collider collider);
        if (collider)
            collider.enabled = !enabled;

        Animator anim = GetComponentInChildren<Animator>(true);
        if (anim)
            anim.enabled = !enabled;

        foreach (var item in GetComponentsInChildren<Rigidbody>(true))
            if (item != _rigidbody)
            {
                item.isKinematic = !enabled;
                item.velocity = _rigidbody.velocity;
            }
        foreach (var item in GetComponentsInChildren<Collider>(true))
            if (item != collider)
                item.enabled = enabled;

        _rigidbody.isKinematic = enabled;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            SetRagdoll();
        }
    }
}
