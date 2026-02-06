using System;
using UnityEngine;

public class HeadBounceCollisionController : MonoBehaviour
{
    private Rigidbody _rigidBody;

    public event Action<HeadBounceTarget> OnHeadBounceCollision;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void OnCollisionStay(Collision collision)
    {
        HeadBounceTarget target = collision.gameObject.GetComponent<HeadBounceTarget>();

        if (target == null)
        {
            return;
        }

        if (collision.GetContact(0).normal.y <= 0f) 
        {
            return;
        }

        if (Mathf.Abs(collision.GetContact(0).point.y - transform.position.y) > 0.2f)
        {
            return;
        }

        if (_rigidBody.linearVelocity.y > 0)
        {
            return;
        }

        OnHeadBounceCollision?.Invoke(target);
    }
}
