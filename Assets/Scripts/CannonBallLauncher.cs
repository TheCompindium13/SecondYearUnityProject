using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallLauncher : MonoBehaviour
{
    [SerializeField]
    private GameObject _cannonballPrefab;
    [SerializeField]
    private float _speed;

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0))
        {
            return;
        }

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100))
        {
            Vector3 direction = (ray.direction * 100 - ray.origin).normalized;
            GameObject ball = Instantiate(_cannonballPrefab, transform.position, Quaternion.identity);
            if (ball.TryGetComponent(out Rigidbody rb))
            {
                rb.AddForce(direction * _speed, ForceMode.Impulse);
                Destroy(ball, 10);
            }
            else
            {
                Destroy(ball);
            }
        }
    }
}
