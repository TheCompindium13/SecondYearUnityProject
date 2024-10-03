using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleRagdoll : MonoBehaviour
{
    private Rigidbody _rigidbody;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    void Start()
    {
        RagdollToggle(false);
    }
    private void SetRagdolltoOn() => RagdollToggle(true);
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
        if (collision.gameObject.CompareTag("Cannonball") )
        {
            SetRagdolltoOn();
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            SetRagdolltoOn();
        }
    }
}
